# LiteXCache
Provide Cache service for any type of application (Asp.Net Core, .Net Standard 2.x). Having a default implementation to wrap the MemoryCache. 


## Add a dependency

### nuget

Run the nuget command for installing the client as,
`Install-Package LiteX.Cache`
`Install-Package LiteX.Cache.Redis`


## Usage

### Configuration

**AppSettings**
```json
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
        #region In-Memory

        services.AddLiteXCache(configuration);

        #endregion

        #region Redis Cache Configuration

        services.AddLiteXRedisCache(configuration);

        // OR
        // load configuration settings on your own.
        // from appsettings, database, hardcoded etc.
        var redisConfig = new RedisConfig();
        services.AddLiteXRedisCache(configuration, redisConfig);

        #endregion
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {

    }
}
```
