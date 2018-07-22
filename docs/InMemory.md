# LiteX InMemory Cache
> LiteX.Cache is a InMemory caching based on on LiteX.Cache.Core and Microsoft.Extensions.Caching.Memory.

This client library enables working with the InMemory Cache for caching any type of data.

Small library to abstract caching mechanism to InMemory Cache. Quick setup for InMemory Cache and very simple wrapper for the InMemory Cache.

Very simple configuration in advanced ways. Purpose of this package is to bring a new level of ease to the developers who deal with InMemory Cache integration with their system.

Note: When you use this library, it means that you will handle the memory of current server.



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

