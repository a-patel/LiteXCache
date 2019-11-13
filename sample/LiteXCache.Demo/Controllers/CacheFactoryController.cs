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
    /// Cache factory controller
    /// </summary>
    [Route("api/[controller]")]
    public class CacheFactoryController : Controller
    {
        #region Fields

        // when using single provider
        // private readonly ILiteXCacheManager _provider;

        // when using multiple provider
        private readonly ILiteXCacheProviderFactory _factory;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="factory"></param>
        // <param name="provider"></param>
        public CacheFactoryController(ILiteXCacheProviderFactory factory)
        {
            _factory = factory;
            //_provider = provider;
            //_provider = _factory.GetCacheProvider("redis");
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get the provider from factory with its name
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("demo-usage")]
        public async Task<IActionResult> DemoUsage()
        {
            // get the provider from factory with its name
            var provider = _factory.GetCacheProvider("inmemory");
            //var provider = _factory.GetCacheProvider("redis");
            //var provider = _factory.GetCacheProvider("memcached");
            //var provider = _factory.GetCacheProvider("sqlite");
            //var provider = _factory.GetCacheProvider("other");


            //TODO: do your work, same like default provider

            IList<Customer> customers;

            //cacheable key
            var key = "customers";

            customers = await provider.GetAsync(key, () =>
            {
                var result = new List<Customer>();
                result = GetCustomers().ToList();
                return result;
            });

            return Ok(customers);
        }

        /// <summary>
        /// Get Cache Provider Type
        /// </summary>
        /// <param name="cacheProviderType">Cache provider type</param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-cache-provider-type")]
        public IActionResult GetCacheProviderType(CacheProviderType cacheProviderType)
        {
            switch (cacheProviderType)
            {
                case CacheProviderType.InMemory:
                    var providerInMemory = _factory.GetCacheProvider("inmemory");
                    return Ok(providerInMemory.CacheProviderType.ToString());

                case CacheProviderType.Redis:
                    var providerRedis = _factory.GetCacheProvider("redis");
                    return Ok(providerRedis.CacheProviderType.ToString());

                case CacheProviderType.Memcached:
                    var providerMemcached = _factory.GetCacheProvider("memcached");
                    return Ok(providerMemcached.CacheProviderType.ToString());

                case CacheProviderType.SQLite:
                    var providerSQLite = _factory.GetCacheProvider("sqlite");
                    return Ok(providerSQLite.CacheProviderType.ToString());

                case CacheProviderType.Other:
                    var providerOther = _factory.GetCacheProvider("other");
                    return Ok(providerOther.CacheProviderType.ToString());

                default:
                    return BadRequest("Provider not supported");
            }
        }

        /// <summary>
        /// Get a cached item. If it's not in the cache yet, then load and cache it
        /// </summary>
        /// <param name="cacheProviderType">Cache provider type</param>
        /// <returns></returns>
        [HttpGet]
        [Route("cache-all")]
        public async Task<IActionResult> CacheCustomers(CacheProviderType cacheProviderType)
        {
            IList<Customer> customers;

            //cacheable key
            var key = "customers";

            switch (cacheProviderType)
            {
                case CacheProviderType.InMemory:
                    var providerInMemory = _factory.GetCacheProvider("inmemory");
                    customers = await providerInMemory.GetAsync(key, () =>
                    {
                        var result = new List<Customer>();
                        result = GetCustomers().ToList();
                        return result;
                    });
                    break;

                case CacheProviderType.Redis:
                    var providerRedis = _factory.GetCacheProvider("redis");
                    customers = await providerRedis.GetAsync(key, () =>
                    {
                        var result = new List<Customer>();
                        result = GetCustomers().ToList();
                        return result;
                    });
                    break;

                case CacheProviderType.Memcached:
                    var providerMemcached = _factory.GetCacheProvider("memcached");
                    customers = await providerMemcached.GetAsync(key, () =>
                    {
                        var result = new List<Customer>();
                        result = GetCustomers().ToList();
                        return result;
                    });
                    break;

                case CacheProviderType.SQLite:
                    var providerSQLite = _factory.GetCacheProvider("sqlite");
                    customers = await providerSQLite.GetAsync(key, () =>
                    {
                        var result = new List<Customer>();
                        result = GetCustomers().ToList();
                        return result;
                    });
                    break;

                case CacheProviderType.Other:
                    var providerOther = _factory.GetCacheProvider("other");
                    customers = await providerOther.GetAsync(key, () =>
                    {
                        var result = new List<Customer>();
                        result = GetCustomers().ToList();
                        return result;
                    });
                    break;

                default:
                    return BadRequest("Provider not supported");
            }

            return Ok(customers);
        }

        /// <summary>
        /// Get a cached item. If it's not in the cache yet, then load and cache it
        /// </summary>
        /// <param name="cacheTime">Cache time in minutes (0 - do not cache)</param>
        /// <param name="cacheProviderType">Cache provider type</param>
        /// <returns></returns>
        [HttpGet]
        [Route("cache-all-specific-time/{cacheTime}")]
        public async Task<IActionResult> CacheCustomers(int cacheTime, CacheProviderType cacheProviderType)
        {
            IList<Customer> customers;

            //cacheable key
            var cacheKey = "customers";

            switch (cacheProviderType)
            {
                case CacheProviderType.InMemory:
                    var providerInMemory = _factory.GetCacheProvider("inmemory");
                    customers = await providerInMemory.GetAsync(cacheKey, cacheTime, () =>
                    {
                        var result = new List<Customer>();
                        result = GetCustomers().ToList();
                        return result;
                    });
                    break;

                case CacheProviderType.Redis:
                    var providerRedis = _factory.GetCacheProvider("redis");
                    customers = await providerRedis.GetAsync(cacheKey, cacheTime, () =>
                    {
                        var result = new List<Customer>();
                        result = GetCustomers().ToList();
                        return result;
                    });
                    break;

                case CacheProviderType.Memcached:
                    var providerMemcached = _factory.GetCacheProvider("memcached");
                    customers = await providerMemcached.GetAsync(cacheKey, cacheTime, () =>
                    {
                        var result = new List<Customer>();
                        result = GetCustomers().ToList();
                        return result;
                    });
                    break;

                case CacheProviderType.SQLite:
                    var providerSQLite = _factory.GetCacheProvider("sqlite");
                    customers = await providerSQLite.GetAsync(cacheKey, cacheTime, () =>
                    {
                        var result = new List<Customer>();
                        result = GetCustomers().ToList();
                        return result;
                    });
                    break;

                case CacheProviderType.Other:
                    var providerOther = _factory.GetCacheProvider("other");
                    customers = await providerOther.GetAsync(cacheKey, cacheTime, () =>
                    {
                        var result = new List<Customer>();
                        result = GetCustomers().ToList();
                        return result;
                    });
                    break;

                default:
                    return BadRequest("Provider not supported");
            }

            return Ok(customers);
        }

        /// <summary>
        /// Get a cached item. If it's not in the cache yet, then load and cache it manually
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="cacheProviderType">Cache provider type</param>
        /// <returns></returns>
        [HttpGet]
        [Route("cache-single-customer/{customerId}")]
        public async Task<IActionResult> CacheCustomer(int customerId, CacheProviderType cacheProviderType)
        {
            Customer customer = null;
            var cacheKey = $"customer-{customerId}";

            switch (cacheProviderType)
            {
                case CacheProviderType.InMemory:
                    var providerInMemory = _factory.GetCacheProvider("inmemory");
                    customer = await providerInMemory.GetAsync<Customer>(cacheKey);

                    if (customer == default(Customer))
                    {
                        //no value in the cache yet
                        //let's load customer and cache the result
                        customer = GetCustomerById(customerId);

                        await providerInMemory.SetAsync(cacheKey, customer, 60);
                    }
                    break;

                case CacheProviderType.Redis:
                    var providerRedis = _factory.GetCacheProvider("redis");
                    customer = await providerRedis.GetAsync<Customer>(cacheKey);

                    if (customer == default(Customer))
                    {
                        //no value in the cache yet
                        //let's load customer and cache the result
                        customer = GetCustomerById(customerId);

                        await providerRedis.SetAsync(cacheKey, customer, 60);
                    }
                    break;

                case CacheProviderType.Memcached:
                    var providerMemcached = _factory.GetCacheProvider("memcached");
                    customer = await providerMemcached.GetAsync<Customer>(cacheKey);

                    if (customer == default(Customer))
                    {
                        //no value in the cache yet
                        //let's load customer and cache the result
                        customer = GetCustomerById(customerId);

                        await providerMemcached.SetAsync(cacheKey, customer, 60);
                    }
                    break;

                case CacheProviderType.SQLite:
                    var providerSQLite = _factory.GetCacheProvider("sqlite");
                    customer = await providerSQLite.GetAsync<Customer>(cacheKey);

                    if (customer == default(Customer))
                    {
                        //no value in the cache yet
                        //let's load customer and cache the result
                        customer = GetCustomerById(customerId);

                        await providerSQLite.SetAsync(cacheKey, customer, 60);
                    }
                    break;

                case CacheProviderType.Other:
                    var providerOther = _factory.GetCacheProvider("other");
                    customer = await providerOther.GetAsync<Customer>(cacheKey);

                    if (customer == default(Customer))
                    {
                        //no value in the cache yet
                        //let's load customer and cache the result
                        customer = GetCustomerById(customerId);

                        await providerOther.SetAsync(cacheKey, customer, 60);
                    }
                    break;

                default:
                    return BadRequest("Provider not supported");
            }

            return Ok(customer);
        }

        /// <summary>
        /// Remove cached item(s).
        /// </summary>
        /// <param name="cacheProviderType">Cache provider type</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("remove-all-cached")]
        public async Task<IActionResult> RemoveCachedCustomers(CacheProviderType cacheProviderType)
        {
            //cacheable key
            var cacheKey = "customers";

            // OR (may not work in web-farm scenario for some providers)
            var cacheKeyPattern = "customers-";

            switch (cacheProviderType)
            {
                case CacheProviderType.InMemory:
                    var providerInMemory = _factory.GetCacheProvider("inmemory");
                    await providerInMemory.RemoveAsync(cacheKey);
                    // remove by pattern
                    await providerInMemory.RemoveByPatternAsync(cacheKeyPattern);
                    break;

                case CacheProviderType.Redis:
                    var providerRedis = _factory.GetCacheProvider("redis");
                    await providerRedis.RemoveAsync(cacheKey);
                    // remove by pattern
                    await providerRedis.RemoveByPatternAsync(cacheKeyPattern);
                    break;

                case CacheProviderType.Memcached:
                    var providerMemcached = _factory.GetCacheProvider("memcached");
                    await providerMemcached.RemoveAsync(cacheKey);
                    // remove by pattern
                    await providerMemcached.RemoveByPatternAsync(cacheKeyPattern);
                    break;

                case CacheProviderType.SQLite:
                    var providerSQLite = _factory.GetCacheProvider("sqlite");
                    await providerSQLite.RemoveAsync(cacheKey);
                    // remove by pattern
                    await providerSQLite.RemoveByPatternAsync(cacheKeyPattern);
                    break;

                case CacheProviderType.Other:
                    var providerOther = _factory.GetCacheProvider("other");
                    await providerOther.RemoveAsync(cacheKey);
                    // remove by pattern
                    await providerOther.RemoveByPatternAsync(cacheKeyPattern);
                    break;

                default:
                    return BadRequest("Provider not supported");
            }

            return Ok();
        }

        /// <summary>
        /// Clear-Flush all cached item(s).
        /// </summary>
        /// <param name="cacheProviderType">Cache provider type</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("clear-cached")]
        public async Task<IActionResult> ClearCachedItems(CacheProviderType cacheProviderType)
        {
            switch (cacheProviderType)
            {
                case CacheProviderType.InMemory:
                    var providerInMemory = _factory.GetCacheProvider("inmemory");
                    await providerInMemory.ClearAsync();
                    break;

                case CacheProviderType.Redis:
                    var providerRedis = _factory.GetCacheProvider("redis");
                    await providerRedis.ClearAsync();
                    break;

                case CacheProviderType.Memcached:
                    var providerMemcached = _factory.GetCacheProvider("memcached");
                    await providerMemcached.ClearAsync();
                    break;

                case CacheProviderType.SQLite:
                    var providerSQLite = _factory.GetCacheProvider("sqlite");
                    await providerSQLite.ClearAsync();
                    break;

                case CacheProviderType.Other:
                    var providerOther = _factory.GetCacheProvider("other");
                    await providerOther.ClearAsync();
                    break;

                default:
                    return BadRequest("Provider not supported");
            }

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
