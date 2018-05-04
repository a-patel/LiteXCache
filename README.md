# LiteXCache
LiteXCache is a caching library that contains basic usages and some advanced usages of caching which can help us to handle caching more easier!
Provide Cache service for any type of application (Asp.Net Core, .Net Standard 2.x). Having a default implementation to wrap the MemoryCache and Redis Cache. 


## Basic Usage


### Install Nuget packages

Run the nuget command for installing the client as,
```
Install-Package LiteX.Cache.Core
Install-Package LiteX.Cache
Install-Package LiteX.Cache.Redis
```

### Configuration

**AppSettings**
```js
{
  "RedisConfig": {
    "RedisCachingConnectionString": "127.0.0.1:6379,ssl=False",
    "PersistDataProtectionKeysToRedis": false
  }
}
```

**Startup Configuration**
```cs
public class Startup
{
    public IConfiguration configuration { get; }

    public Startup(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        #region LiteX Caching

        #region In-Memory

        services.AddLiteXCache();

        #endregion

        #region Redis Cache Configuration

        // 1. Use default configuration from appsettings.json's 'RedisConfig'
        services.AddLiteXRedisCache(configuration);

        // 2. Load configuration settings using options.
        services.AddLiteXRedisCache(option =>
        {
            option.RedisCachingConnectionString = "127.0.0.1:6379,ssl=False";
        });

        // 3. Load configuration settings on your own.
        // (e.g. appsettings, database, hardcoded)
        var redisConfig = new RedisConfig();
        services.AddLiteXRedisCache(configuration, redisConfig);

        #endregion

        #endregion
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {

    }
}
```

### Use in Controller or Business layer

```cs
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
    /// Get a cached item. If it's not in the cache yet, then load and cache it
    /// </summary>
    /// <returns></returns>
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

        return Ok(customers);
    }

    /// <summary>
    /// Get a cached item. If it's not in the cache yet, then load and cache it
    /// </summary>
    /// <param name="cacheTime">Cache time in minutes (0 - do not cache)</param>
    /// <returns></returns>
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

        return Ok(customers);
    }

    /// <summary>
    /// Get a cached item. If it's not in the cache yet, then load and cache it manually
    /// </summary>
    /// <param name="customerId"></param>
    /// <returns></returns>
    public IActionResult CacheCustomer(int customerId)
    {
        Customer customer = null;
        var cacheKey = $"customer-{customerId}";

        customer = _cacheManager.Get<Customer>(cacheKey);

        if (customer == default(Customer))
        {
            //no value in the cache yet
            //let's load customer and cache the result
            customer = GetCustomerById(customerId);
            _cacheManager.Set(cacheKey, customer, 60);
        }
        return Ok(customer);
    }

    /// <summary>
    /// Remove cached item(s).
    /// </summary>
    /// <returns></returns>
    public IActionResult RemoveCachedCustomers()
    {
        //cacheable key
        var cacheKey = "customers";

        _cacheManager.Remove(cacheKey);

        // OR
        var cacheKeyPattern = "customers-";
        // remove by pattern
        _cacheManager.RemoveByPattern(cacheKeyPattern);

        return Ok();
    }

    #endregion

    #region Utilities

    private IList<Customer> GetCustomers()
    {
        IList<Customer> customers = new List<Customer>();

        customers.Add(new Customer() { Id = 1, Username = "ashish", Email = "toaashishpatel@outlook.com" });

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

## Documentation

For more helpful information about EasyCaching, please click [here](http://easycaching.readthedocs.io/en/latest/) for EasyCaching's documentation. 


## Examples

See [sample](https://comming.soon)


## Todo List

### Caching Providers

- [x] InMemory
- [x] Redis

### Basic Caching API

- [x] Get(with data retriever)
- [x] Set
- [x] Remove


