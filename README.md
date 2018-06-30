# LiteXCache
LiteXCache is simple yet powerful and very high performance cache mechanism and incorporating both synchronous and asynchronous usage with some advanced usages of caching which can help us to handle caching more easier!

Provide Cache service for any type of application (.NET Core, .NET Standard).

Having a default/generic implementation to wrap the MemoryCache, Redis Cache, Memcached, SQLite, HTTP Request cache and independed on the underlying caching framework(s).

Very simple yet advanced configuration. Easily migrate or switch between one to another provider with no code breaking changes. Minimal (one line) code configuration is required.

It supports various cache providers and implements many advanced features. You can also write your own and extend it also extend eaxting providers. 

LiteXCache is an interface to unify the programming model for various cache providers.

Better use of Http request cache for other external cache providers (Redis, Memcached, SQLite etc).

The Core library contains all base interfaces and tools. One should install at least one other LiateXCache package to get cache handle implementations.

The main goal of the LiteXCache package is to make developer's life easier to handle even very complex caching scenarios.


## Cache Providers
- [Redis](https://github.com/a-patel/LiteXCache/blob/master/docs/Redis.md)
- [InMemory](https://github.com/a-patel/LiteXCache/blob/master/docs/In-Memory.md)
- [SQLite](https://github.com/a-patel/LiteXCache/blob/master/docs/SQLite.md)
- [Memcached](https://github.com/a-patel/LiteXCache/blob/master/docs/Memcached.md)


## Basic Usage


### Step 1 : Install the package

Choose one kinds of caching type that you needs and install it via [Nuget](https://www.nuget.org/profiles/iamaashishpatel).
To install LiteXCache, run the following command in the [Package Manager Console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console)

```Powershell
PM> Install-Package LiteX.Cache
PM> Install-Package LiteX.Cache.Redis
PM> Install-Package LiteX.Cache.Memcached
PM> Install-Package LiteX.Cache.SQLite
```

### Step 2 : Configuration
Different types of caching provider have their own way to config.
Here are samples that show you how to config.

##### 2.1 : AppSettings
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

##### 2.2 : Configure Startup Class
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

### Step 3 : Use in Controller or Business layer

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



