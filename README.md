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
        // register per request cache service
        services.AddScoped<ICacheManager, PerRequestCacheManager>();

        // static cache manager (use one of below)

        #region In-Memory

        // register static cache service (in-memory)
        services.AddSingleton<IStaticCacheManager, MemoryCacheManager>();

        #endregion

        #region Redis Cache Configuration

        // add redis configuration settings
        services.AddSingleton(configuration.GetSection("RedisConfig").Get<RedisConfig>());

        // register redis configuration service
        services.AddSingleton<IRedisConnectionWrapper, RedisConnectionWrapper>();

        // register static cache service (redis)
        services.AddSingleton<IStaticCacheManager, RedisCacheManager>();

        #endregion
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {

    }
}
```
