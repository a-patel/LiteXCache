# LiteX InMemory Cache
> LiteX.Cache is a InMemory caching based on on LiteX.Cache.Core and Microsoft.Extensions.Caching.Memory. Small library for manage cache with InMemory. A quick setup for InMemory Caching.

> Wrapper library is just written for the purpose to bring a new level of ease to the developers who deal with InMemory Cache integration with your system.

When you use this library , it means that you will handle the memory of current server . As usual , we named it as local caching.

This package is simple yet powerful and very high performance cache mechanism and incorporating both synchronous and asynchronous usage with some advanced usages of caching which can help us to handle caching more easier!

Provide Cache service for any type of application (.NET Core, .NET Standard).

Very simple yet advanced configuration. The main goal of the LiteXCache package is to make developer's life easier to handle even very complex caching scenarios.


## How to use ?


### Install Nuget packages

Run the nuget command for installing the client as,
```
Install-Package LiteX.Cache.Core
Install-Package LiteX.Cache
```

### Configuration

##### Startup Configuration
```cs
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddLiteXCache();
    }
}
```

### Use in Controller or Business layer

```cs
/// <summary>
/// Customer controller
/// </summary>
[Route("api/[controller]")]
public class CustomerController : Controller
{
    #region Fields

    private readonly ILiteXCacheManager _cacheManager;

    #endregion

    #region Ctor

    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="cacheManager"></param>
    public CustomerController(ILiteXCacheManager cacheManager)
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
    public IActionResult CacheCustomers()
    {
        IList<Customer> customers;

        //cacheable key
        var key = "customers";

        customers = _cacheManager.Get(key, () =>
        {
            var result = new List<Customer>();
            result = GetCustomers().ToList();
            return result;
        });


        ////Async
        //customers = await _cacheManager.GetAsync(key, () =>
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
    public IActionResult CacheCustomers(int cacheTime)
    {
        IList<Customer> customers;

        //cacheable key
        var cacheKey = "customers";

        customers = _cacheManager.Get(cacheKey, cacheTime, () =>
        {
            var result = new List<Customer>();
            result = GetCustomers().ToList();
            return result;
        });


        ////Async
        //customers = await _cacheManager.GetAsync(cacheKey, cacheTime, () =>
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
    public IActionResult CacheCustomer(int customerId)
    {
        Customer customer = null;
        var cacheKey = $"customer-{customerId}";

        customer = _cacheManager.Get<Customer>(cacheKey);

        ////Async
        //customer = await _cacheManager.GetAsync<Customer>(cacheKey);

        if (customer == default(Customer))
        {
            //no value in the cache yet
            //let's load customer and cache the result
            customer = GetCustomerById(customerId);
            _cacheManager.Set(cacheKey, customer, 60);

            ////Async
            //await _cacheManager.SetAsync(cacheKey, customer, 60);
        }
        return Ok(customer);
    }

    /// <summary>
    /// Remove cached item(s).
    /// </summary>
    /// <returns></returns>
    [HttpDelete]
    [Route("remove-all-cached")]
    public IActionResult RemoveCachedCustomers()
    {
        //cacheable key
        var cacheKey = "customers";

        _cacheManager.Remove(cacheKey);

        ////Async
        //await _cacheManager.RemoveAsync(cacheKey);


        // OR
        var cacheKeyPattern = "customers-";
        // remove by pattern
        _cacheManager.RemoveByPattern(cacheKeyPattern);

        ////Async
        //await _cacheManager.RemoveByPatternAsync(cacheKeyPattern);


        return Ok();
    }

    /// <summary>
    /// Clear-Flush all cached item(s).
    /// </summary>
    /// <returns></returns>
    [HttpDelete]
    [Route("clear-cached")]
    public IActionResult ClearCachedItems()
    {
        _cacheManager.Clear();

        ////Async
        //await _cacheManager.ClearAsync();

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

        return customers;
    }

    private Customer GetCustomerById(int id)
    {
        Customer customer = null;

        customer = GetCustomers().ToList().FirstOrDefault(x => x.Id == id);

        return customer;
    }

    #endregion
}
```


## Coming soon

* Logging
