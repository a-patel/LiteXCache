# LiteXCache

> LiteXCache is simple yet powerful and very high performance cache mechanism and incorporating both synchronous and asynchronous usage with some advanced usages of caching which can help us to handle caching more easier!




Provide Storage service for ASP.NET Core (2.0 and later) applications.

Small library to abstract caching functionalities. Quick setup for any caching provider and very simple wrapper for widely used providers. LiteX Cache uses the least common denominator of functionality between the supported providers to build a caching solution. Abstract interface to implement any kind of basic caching services. Having a default/generic implementation to wrap the MemoryCache, Redis Cache, Memcached, SQLite, HTTP Request cache and independent on the underlying caching framework(s).

Very simple configuration in advanced ways. Purpose of this package is to bring a new level of ease to the developers who deal with different caching provider integration with their system and implements many advanced features. You can also write your own and extend it also extend existing providers. Easily migrate or switch between one to another provider with no code breaking changes.

LiteXCache is an interface to unify the programming model for various cache providers. The Core library contains all base interfaces and tools. One should install at least one other LiteXCache package to get caching mechanism implementation. 

Achieve significant performance by better use of Http request cache for other external cache providers (Redis, Memcached, SQLite etc).





## Cache Providers :books:

- [InMemory](docs/InMemory.md) [![](https://img.shields.io/nuget/dt/LiteX.Cache.svg)](https://www.nuget.org/packages/LiteX.Cache/) [![](https://img.shields.io/nuget/v/LiteX.Cache.svg)](https://www.nuget.org/packages/LiteX.Cache/)
- [Redis](docs/Redis.md) [![](https://img.shields.io/nuget/dt/LiteX.Cache.Redis.svg)](https://www.nuget.org/packages/LiteX.Cache.Redis/) [![](https://img.shields.io/nuget/v/LiteX.Cache.Redis.svg)](https://www.nuget.org/packages/LiteX.Cache.Redis/)
- [Memcached](docs/Memcached.md) [![](https://img.shields.io/nuget/dt/LiteX.Cache.Memcached.svg)](https://www.nuget.org/packages/LiteX.Cache.Memcached/) [![](https://img.shields.io/nuget/v/LiteX.Cache.Memcached.svg)](https://www.nuget.org/packages/LiteX.Cache.Memcached/)
- [SQLite](docs/SQLite.md) [![](https://img.shields.io/nuget/dt/LiteX.Cache.SQLite.svg)](https://www.nuget.org/packages/LiteX.Cache.SQLite/) [![](https://img.shields.io/nuget/v/LiteX.Cache.SQLite.svg)](https://www.nuget.org/packages/LiteX.Cache.SQLite/)



## Features :pager:

- Multiple provider support (using provider factory)
- Cache any type of data
- Cache data for specific time
- Distributed Cache
- Async compatible
- Cache Removal and Flush support
- Many other features
- Simple API with familiar sliding or absolute expiration
- Guaranteed single evaluation of your factory delegate whose results you want to cache
- Strongly typed generics based API. No need to cast your cached objects every time you retrieve them
- Thread safe, concurrency ready
- Obsolete sync methods
- Interface based API to support the test driven development and dependency injection
- Leverages a provider model on top of ILiteXCacheManager under the hood and can be extended with your own implementation



## Basic Usage :page_facing_up:

### Step 1 : Install the package :package:

> Choose one kinds of caching type that you needs and install it via [Nuget](https://www.nuget.org/profiles/iamaashishpatel).
> To install LiteXCache, run the following command in the [Package Manager Console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console)

```Powershell
PM> Install-Package LiteX.Cache
PM> Install-Package LiteX.Cache.Redis
PM> Install-Package LiteX.Cache.Memcached
PM> Install-Package LiteX.Cache.SQLite
```


### Step 2 : Configuration 🔨

> Different types of caching provider have their own way to config.
> Here are samples that show you how to config.

##### 2.1 : AppSettings

```js
{
  //LiteX InMemory Cache settings (Optional)
  "InMemoryConfig": {
    "EnableLogging": true
  },
  
  //LiteX Redis Cache settings
  "RedisConfig": {
    "RedisCachingConnectionString": "127.0.0.1:6379,ssl=False",
    "EnableLogging": true
  },

  //LiteX Memcached Cache settings (don't use this option)
  "MemcachedConfig": {
    "EnableLogging": true
  },

  //LiteX SQLite Config settings (Optional)
  "SQLiteConfig": {
    "FilePath": "",
    "FileName": "",
    "EnableLogging": true
  }
}
```


##### 2.2 : Configure Startup Class

```cs
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        #region LiteX Caching (InMemory)

        // 1. Use default configuration from appsettings.json's 'InMemoryConfig'
        services.AddLiteXCache();

        //OR
        // 2. Load configuration settings using options.
        services.AddLiteXCache(option =>
        {
            option.EnableLogging = false;
        });

        //OR
        // 3. Load configuration settings on your own.
        // (e.g. appsettings, database, hardcoded)
        var inMemoryConfig = new InMemoryConfig()
        {
            EnableLogging = false,
        };
        services.AddLiteXCache(inMemoryConfig);

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
            option.EnableLogging = false;
        });

        //OR
        // 3. Load configuration settings on your own.
        // (e.g. appsettings, database, hardcoded)
        var redisConfig = new RedisConfig()
        {
            RedisCachingConnectionString = "127.0.0.1:6379,ssl=False",
            //PersistDataProtectionKeysToRedis = true
            EnableLogging = false,
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
            option.EnableLogging = false;
        });

        //OR
        // 3. Load configuration settings on your own.
        // (e.g. appsettings, database, hardcoded)
        var sqLiteConfig = new SQLiteConfig()
        {
            FileName = "",
            FilePath = "",
            OpenMode = Microsoft.Data.Sqlite.SqliteOpenMode.ReadWriteCreate,
            CacheMode = Microsoft.Data.Sqlite.SqliteCacheMode.Default,
            EnableLogging = false,
        };
        services.AddLiteXSQLiteCache(sqLiteConfig);

        #endregion


        #region LiteX Caching (Memcached)

        // don't use this option
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
            option.EnableLogging = false;
        });

        //OR
        // 3. Load configuration settings on your own.
        // (e.g. appsettings, database, hardcoded)
        var memcachedConfig = new MemcachedConfig()
        {
            PersistDataProtectionKeysToMemcached = true,
            EnableLogging = false,
        };
        services.AddLiteXMemcachedCache(providerOption =>
        {
            providerOption.Protocol = Enyim.Caching.Memcached.MemcachedProtocol.Binary;
            providerOption.Servers = new System.Collections.Generic.List<Enyim.Caching.Configuration.Server>() { new Enyim.Caching.Configuration.Server() { Address = "", Port = 0 } };

            // configure rest of the options as needed
        }, memcachedConfig);

        #endregion
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
        //Memcached
        app.UseLiteXMemcachedCache();

        //SQLite
        app.UseLiteXSQLiteCache();
    }
}
```


### Step 3 : Use in Controller or Business layer :memo:

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


## Todo List :clipboard:

#### Caching Providers

- [x] InMemory
- [x] Redis
- [x] Memcached
- [x] SQLite


#### Basic Caching API

- [x] Get (with data retriever)
- [x] Set
- [x] Remove
- [x] Clear


#### Coming soon

- .NET Standard 2.1 support
- .NET 5.0 support
- Remove sync methods





---





## Give a Star! :star:

Feel free to request an issue on github if you find bugs or request a new feature. Your valuable feedback is much appreciated to better improve this project. If you find this useful, please give it a star to show your support for this project.



## Support :telephone:

> Reach out to me at one of the following places!

- Email :envelope: at <a href="mailto:toaashishpatel@gmail.com" target="_blank">`toaashishpatel@gmail.com`</a>
- NuGet :package: at <a href="https://www.nuget.org/profiles/iamaashishpatel" target="_blank">`@iamaashishpatel`</a>



## Author :boy:

* **Ashish Patel** - [A-Patel](https://github.com/a-patel)


##### Connect with me

| Linkedin | Website | Medium | NuGet | GitHub | Microsoft | Facebook | Twitter | Instagram | Tumblr |
|----------|----------|----------|----------|----------|----------|----------|----------|----------|----------|
| [![linkedin](https://img.icons8.com/ios-filled/96/000000/linkedin.png)](https://www.linkedin.com/in/iamaashishpatel) | [![website](https://img.icons8.com/wired/96/000000/domain.png)](https://aashishpatel.netlify.app/) | [![medium](https://img.icons8.com/ios-filled/96/000000/medium-monogram.png)](https://medium.com/@iamaashishpatel) | [![nuget](https://img.icons8.com/windows/96/000000/nuget.png)](https://nuget.org/profiles/iamaashishpatel) | [![github](https://img.icons8.com/ios-glyphs/96/000000/github.png)](https://github.com/a-patel) | [![microsoft](https://img.icons8.com/ios-filled/90/000000/microsoft.png)](https://docs.microsoft.com/en-us/users/iamaashishpatel) | [![facebook](https://img.icons8.com/ios-filled/90/000000/facebook.png)](https://www.facebook.com/aashish.mrcool) | [![twitter](https://img.icons8.com/ios-filled/96/000000/twitter.png)](https://twitter.com/aashish_mrcool) | [![instagram](https://img.icons8.com/ios-filled/90/000000/instagram-new.png)](https://www.instagram.com/iamaashishpatel/) | [![tumblr](https://img.icons8.com/ios-filled/96/000000/tumblr--v1.png)](https://iamaashishpatel.tumblr.com/) |



## Donate :dollar:

If you find this project useful — or just feeling generous, consider buying me a beer or a coffee. Cheers! :beers: :coffee:

| PayPal | BMC | Patreon |
| ------------- | ------------- | ------------- |
| [![PayPal](https://www.paypalobjects.com/webstatic/en_US/btn/btn_donate_pp_142x27.png)](https://www.paypal.me/iamaashishpatel) | [![Buy Me A Coffee](https://www.buymeacoffee.com/assets/img/custom_images/orange_img.png)](https://www.buymeacoffee.com/iamaashishpatel) | [![Patreon](https://c5.patreon.com/external/logo/become_a_patron_button.png)](https://www.patreon.com/iamaashishpatel) |



## License :lock:

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
