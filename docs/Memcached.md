
# LiteX Memcached Cache
Distributed caching based on Memcached. Small library for manage cache with Memcached. A quick setup for Memcached. LiteX.Cache.Memcached is a Memcached caching library which is based on LiteX.Cache.Core and Memcached.

This package is simple yet powerful and very high performance cache mechanism and incorporating both synchronous and asynchronous usage with some advanced usages of caching which can help us to handle caching more easier!

Provide Cache service for any type of application (.NET Core, .NET Standard).

Very simple yet advanced configuration. The main goal of the LiteXCache package is to make developer's life easier to handle even very complex caching scenarios.


## Basic Usage


### Install the package

> Install via [Nuget](https://www.nuget.org/packages/LiteX.Cache.Memcached/).

```Powershell
PM> Install-Package LiteX.Cache.Memcached
```

##### AppSettings
```js
{  
  //LiteX Memcached Cache settings (don't use this option)
  "MemcachedConfig": {
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

    }
}
```

### Sample Usage Example
> Same for all providers. 

For more helpful information about LiteX Caching, Please click [here.](https://github.com/a-patel/LiteXCache/blob/master/README.md#step-3--use-in-controller-or-business-layer-memo)

