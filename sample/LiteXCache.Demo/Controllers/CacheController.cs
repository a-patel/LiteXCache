#region Imports
using LiteX.Cache.Core;
using LiteXCache.Demo.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
#endregion

namespace LiteXCache.Demo.Controllers
{
    /// <summary>
    /// Cache controller
    /// </summary>
    [Route("api/[controller]")]
    public class CacheController : Controller
    {
        #region Fields

        private readonly ILiteXCacheManager _cacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager"></param>
        public CacheController(ILiteXCacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get Cache Provider Type
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get-cache-provider-type")]
        public IActionResult GetCacheProviderType()
        {
            return Ok(_cacheManager.CacheProviderType.ToString());
        }

        /// <summary>
        /// Get a cached item. If it's not in the cache yet, then load and cache it
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("cache-all")]
        public async Task<IActionResult> CacheCustomers()
        {
            IList<Customer> customers;

            //cacheable key
            var key = "customers";

            customers = await _cacheManager.GetAsync(key, () =>
            {
                var result = new List<Customer>();
                result = GetCustomers().ToList();
                return result;
            });

            //// sync
            //customers = _cacheManager.Get(key, () =>
            //{
            //    var result = new List<Customer>();
            //    result = GetCustomers().ToList();
            //    return result;
            //});

            return Ok(customers);
        }

        /// <summary>
        /// Get a cached item. If it's not in the cache yet, then load and cache it
        /// </summary>
        /// <param name="cacheTime">Cache time in minutes (0 - do not cache)</param>
        /// <returns></returns>
        [HttpGet]
        [Route("cache-all-specific-time/{cacheTime}")]
        public async Task<IActionResult> CacheCustomers(int cacheTime)
        {
            IList<Customer> customers;

            //cacheable key
            var cacheKey = "customers";

            customers = await _cacheManager.GetAsync(cacheKey, cacheTime, () =>
            {
                var result = new List<Customer>();
                result = GetCustomers().ToList();
                return result;
            });

            //// sync
            //customers = _cacheManager.Get(cacheKey, cacheTime, () =>
            //{
            //    var result = new List<Customer>();
            //    result = GetCustomers().ToList();
            //    return result;
            //});

            return Ok(customers);
        }

        /// <summary>
        /// Get a cached item. If it's not in the cache yet, then load and cache it manually
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("cache-single-customer/{customerId}")]
        public async Task<IActionResult> CacheCustomer(int customerId)
        {
            Customer customer = null;
            var cacheKey = $"customer-{customerId}";

            customer = await _cacheManager.GetAsync<Customer>(cacheKey);

            //// sync
            //customer = _cacheManager.Get<Customer>(cacheKey);

            if (customer == default(Customer))
            {
                //no value in the cache yet
                //let's load customer and cache the result
                customer = GetCustomerById(customerId);

                await _cacheManager.SetAsync(cacheKey, customer, 60);

                //// sync
                //_cacheManager.Set(cacheKey, customer, 60);
            }

            return Ok(customer);
        }

        /// <summary>
        /// Remove cached item(s).
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("remove-all-cached")]
        public async Task<IActionResult> RemoveCachedCustomers()
        {
            //cacheable key
            var cacheKey = "customers";
            
            await _cacheManager.RemoveAsync(cacheKey);

            //// sync
            //_cacheManager.Remove(cacheKey);


            // OR (may not work in web-farm scenario for some providers)
            var cacheKeyPattern = "customers-";
            // remove by pattern
            await _cacheManager.RemoveByPatternAsync(cacheKeyPattern);

            //// sync
            //_cacheManager.RemoveByPattern(cacheKeyPattern);

            return Ok();
        }

        /// <summary>
        /// Clear-Flush all cached item(s).
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("clear-cached")]
        public async Task<IActionResult> ClearCachedItems()
        {
            await _cacheManager.ClearAsync();

            //// sync
            //_cacheManager.Clear();

            return Ok();
        }

        #endregion

        #region Utilities

        private IList<Customer> GetCustomers(int total = 1000)
        {
            IList<Customer> customers = new List<Customer>();

            for (int i = 1; i < (total + 1); i++)
            {
                customers.Add(new Customer() { Id = i, Username = $"customer_{i}", Email = $"customer_{i}@example.com" });
            }

            System.Threading.Thread.Sleep(5 * 1000);

            return customers;
        }

        private Customer GetCustomerById(int id)
        {
            Customer customer = null;

            customer = GetCustomers().ToList().FirstOrDefault(x => x.Id == id);

            System.Threading.Thread.Sleep(3 * 1000);

            return customer;
        }

        #endregion
    }
}



#region Reference
/*
https://github.com/dotnetcore/EasyCaching
     
*/
#endregion

