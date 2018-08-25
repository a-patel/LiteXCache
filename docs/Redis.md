# LiteX Redis Cache
> Distributed caching based on StackExchange.Redis and Redis.

This client library enables working with the Redis Cache for caching any type of data.

Small library to abstract caching mechanism to Redis Cache. Quick setup for Redis Cache and very simple wrapper for the Redis Cache.

Very simple configuration in advanced ways. Purpose of this package is to bring a new level of ease to the developers who deal with Redis Cache integration with their system.


## Basic Usage

### Install the package

> Install via [Nuget](https://www.nuget.org/packages/LiteX.Cache.Redis/).

```Powershell
PM> Install-Package LiteX.Cache.Redis
```

##### AppSettings
```js
{  
  //LiteX Redis Cache settings
  "RedisConfig": {
    "RedisCachingConnectionString": "127.0.0.1:6379,ssl=False",
    "PersistDataProtectionKeysToRedis": false,
    "EnableLogging": true
  }
}
```

##### Configure Startup Class
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
            option.EnableLogging = false;
        });

        //OR
        // 3. Load configuration settings on your own.
        // (e.g. appsettings, database, hardcoded)
        var redisConfig = new RedisConfig()
        {
            RedisCachingConnectionString = "127.0.0.1:6379,ssl=False",
            EnableLogging = false,
        };
        services.AddLiteXRedisCache(redisConfig);
    }
}
```

### Sample Usage Example
> Same for all providers. 

For more helpful information about LiteX Caching, Please click [here.](https://github.com/a-patel/LiteXCache/blob/master/README.md#step-3--use-in-controller-or-business-layer-memo)

