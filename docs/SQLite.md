# LiteX SQLite Cache
SQLite caching based on SQLite database. Small library for manage cache with SQLite. A quick setup for SQLite Caching.

This package is simple yet powerful and very high performance cache mechanism and incorporating both synchronous and asynchronous usage with some advanced usages of caching which can help us to handle caching more easier!

Provide Cache service for any type of application (.NET Core, .NET Standard).

Very simple yet advanced configuration. The main goal of the LiteXCache package is to make developer's life easier to handle even very complex caching scenarios.


## Basic Usage


### Install the package

> Install via [Nuget](https://www.nuget.org/packages/LiteX.Cache.SQLite/).

```Powershell
PM> Install-Package LiteX.Cache.SQLite
```

##### AppSettings
```js
{  
  //LiteX SQLite Config settings (Optional)
  "SQLiteConfig": {
    "FilePath": "",
    "FileName": "",
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
    }
}
```

### Sample Usage Example
> Same for all providers. 

For more helpful information about LiteX Caching, Please click [here.](https://github.com/a-patel/LiteXCache/blob/master/README.md#step-3--use-in-controller-or-business-layer-memo)

