# LiteX InMemory Cache
> LiteX.Cache is a InMemory caching based on on LiteX.Cache.Core and Microsoft.Extensions.Caching.Memory. Small library for manage cache with InMemory. A quick setup for InMemory Caching.

> Wrapper library is just written for the purpose to bring a new level of ease to the developers who deal with InMemory Cache integration with your system.

Provide Cache service for ASP.NET Core (2.0 and later) application.

This package is simple yet powerful and very high performance cache mechanism and incorporating both synchronous and asynchronous usage with some advanced usages of caching which can help us to handle caching more easier!

Very simple and advanced configuration. The main goal of the this package is to make developer's life easier to handle even very complex caching scenarios.

When you use this library, it means that you will handle the memory of current server. As usual, I named it as local caching.



## Basic Usage


### Install the package

> Install via [Nuget](https://www.nuget.org/packages/LiteX.Cache/).

```Powershell
PM> Install-Package LiteX.Cache
```

##### AppSettings
```js
{  
  //LiteX InMemory Cache settings (Optional)
  "InMemoryConfig": {
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
    }
}
```

### Sample Usage Example
> Same for all providers. 

For more helpful information about LiteX Caching, Please click [here.](https://github.com/a-patel/LiteXCache/blob/master/README.md#step-3--use-in-controller-or-business-layer-memo)

