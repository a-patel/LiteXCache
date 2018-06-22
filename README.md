# LiteXCache
LiteXCache is a caching library that contains basic usages and some advanced usages of caching which can help us to handle caching more easier!
Provide Cache service for any type of application (Asp.Net Core, .Net Standard 2.x). Having a default implementation to wrap the MemoryCache, Redis Cache and Memcached Cache. 


## Basic Usage


### Install Nuget packages

Run the nuget command for installing the client as,
```
Install-Package LiteX.Cache.Core
Install-Package LiteX.Cache
Install-Package LiteX.Cache.Redis
Install-Package LiteX.Cache.Memcached
Install-Package LiteX.Cache.SQLite
```

### Configuration

##### AppSettings
```js
{  
  //LiteX Redis Cache settings
  "RedisConfig": {
    "RedisCachingConnectionString": "127.0.0.1:6379,ssl=False",
    "PersistDataProtectionKeysToRedis": false
  },

  //LiteX Memcached Cache settings
  "MemcachedConfig": {
    "PersistDataProtectionKeysToMemcached": false
  },

  //LiteX SQLite Config settings (Optional)
  "SQLiteConfig": {
    "FilePath": "",
    "FileName": ""
  }
}
```

##### Startup Configuration
```cs
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        #region LiteX Caching (In-Memory)

        services.AddLiteXCache();

        #endregion

        #region LiteX Caching (Redis)

        // 1. Use default configuration from appsettings.json's 'RedisConfig'
        services.AddLiteXRedisCache();

        //OR
        // 2. Load configuration settings using options.
        services.AddLiteXRedisCache(option =>
        {
            option.RedisCachingConnectionString = "127.0.0.1:6379,ssl=False";
            //option.PersistDataProtectionKeysToRedis = true;
        });

        //OR
        // 3. Load configuration settings on your own.
        // (e.g. appsettings, database, hardcoded)
        var redisConfig = new RedisConfig()
        {
            RedisCachingConnectionString = "127.0.0.1:6379,ssl=False",
            //PersistDataProtectionKeysToRedis = true
        };
        services.AddLiteXRedisCache(redisConfig);

        #endregion


        #region LiteX Caching (SQLite)

        // 1. Use default configuration from appsettings.json's 'SQLiteConfig'
        services.AddLiteXSQLiteCache();

        //OR
        // 2. Load configuration settings using options.
        services.AddLiteXSQLiteCache(option =>
        {
            option.FileName = "";
            option.FilePath = "";
            option.OpenMode = Microsoft.Data.Sqlite.SqliteOpenMode.ReadWriteCreate;
            option.CacheMode = Microsoft.Data.Sqlite.SqliteCacheMode.Default;
        });

        //OR
        // 3. Load configuration settings on your own.
        // (e.g. appsettings, database, hardcoded)
        var sqLiteConfig = new SQLiteConfig()
        {
            FileName = "",
            FilePath = "",
            OpenMode = Microsoft.Data.Sqlite.SqliteOpenMode.ReadWriteCreate,
            CacheMode = Microsoft.Data.Sqlite.SqliteCacheMode.Default
        };
        services.AddLiteXSQLiteCache(sqLiteConfig);

        #endregion


        #region LiteX Caching (Memcached)

        // 1. Use default configuration from appsettings.json's 'MemcachedConfig'
        services.AddLiteXMemcachedCache(providerOption =>
        {
            providerOption.Protocol = Enyim.Caching.Memcached.MemcachedProtocol.Binary;
            providerOption.Servers = new System.Collections.Generic.List<Enyim.Caching.Configuration.Server>() { new Enyim.Caching.Configuration.Server() { Address = "", Port = 0 } };

            // configure rest of the options as needed
        });

        //OR
        // 2. Load configuration settings using options.
        services.AddLiteXMemcachedCache(providerOption =>
        {
            providerOption.Protocol = Enyim.Caching.Memcached.MemcachedProtocol.Binary;
            providerOption.Servers = new System.Collections.Generic.List<Enyim.Caching.Configuration.Server>() { new Enyim.Caching.Configuration.Server() { Address = "", Port = 0 } };

            // configure rest of the options as needed
        }, option =>
        {
            option.PersistDataProtectionKeysToMemcached = true;
        });

        //OR
        // 3. Load configuration settings on your own.
        // (e.g. appsettings, database, hardcoded)
        var memcachedConfig = new MemcachedConfig()
        {
            PersistDataProtectionKeysToMemcached = true

        };
        services.AddLiteXMemcachedCache(providerOption =>
        {
            providerOption.Protocol = Enyim.Caching.Memcached.MemcachedProtocol.Binary;
            providerOption.Servers = new System.Collections.Generic.List<Enyim.Caching.Configuration.Server>() { new Enyim.Caching.Configuration.Server() { Address = "", Port = 0 } };

            // configure rest of the options as needed
        }, memcachedConfig);

        #endregion
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        //Memcached
        app.UseLiteXMemcachedCache();

        //SQLite
        app.UseLiteXSQLiteCache();
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


## Examples

See [sample](https://comming.soon)


## Todo List

#### Caching Providers

- [x] InMemory
- [x] Redis
- [x] Memcached
- [x] SQLite

#### Basic Caching API

- [x] Get(with data retriever)
- [x] Set
- [x] Remove
- [x] Clear


## Coming soon

* Logging
