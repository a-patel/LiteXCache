# LiteX Redis Cache
Distributed caching based on StackExchange.Redis and Redis. Small library for manage cache with Redis. A quick setup for Redis. LiteX.Cache.Redis is a redis caching library which is based on LiteX.Cache.Core and StackExchange.Redis.

When you use this library, it means that you will handle the data of your redis servers. As usual, you can use it as distributed caching.

This package is simple yet powerful and very high performance cache mechanism and incorporating both synchronous and asynchronous usage with some advanced usages of caching which can help us to handle caching more easier!

Provide Cache service for any type of application (.NET Core, .NET Standard).

Very simple yet advanced configuration. The main goal of the LiteXCache package is to make developer's life easier to handle even very complex caching scenarios.


## How to use ?


### Install Nuget packages

Run the nuget command for installing the client as,
```
Install-Package LiteX.Cache.Core
Install-Package LiteX.Cache.Redis
```

### Configuration

##### AppSettings
```js
{
  "RedisConfig": {
    "RedisCachingConnectionString": "127.0.0.1:6379,ssl=False",
    "PersistDataProtectionKeysToRedis": false
  }
}
```

##### Startup Configuration
```cs
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
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
    }
}
```

### Usage Example
> Same for all providers


