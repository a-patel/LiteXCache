#region Imports
using LiteX.Cache;
using LiteX.Cache.Memcached;
using LiteX.Cache.Redis;
using LiteX.Cache.SQLite;
using LiteX.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
#endregion

namespace LiteXCache.Demo
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            #region LiteX Caching

            #region LiteX Caching (Register more than one providers)

            // register default provider
            services.AddLiteXCache();


            //// register another provider using factory (AmazonS3)
            //// 1. Use default configuration from appsettings.json's 'RedisConfig'
            //services.AddLiteXRedisCacheFactory();

            ////OR
            //// 2. Load configuration settings using options.
            //services.AddLiteXRedisCacheFactory(option =>
            //{
            //    option.RedisCachingConnectionString = "127.0.0.1:6379,ssl=False";
            //    //option.PersistDataProtectionKeysToRedis = true;
            //    option.EnableLogging = false;
            //}, providerName: "redis");

            ////OR
            //// 3. Load configuration settings on your own.
            //// (e.g. appsettings, database, hardcoded)
            //var redisConfig2 = new RedisConfig()
            //{
            //    RedisCachingConnectionString = "127.0.0.1:6379,ssl=False",
            //    //PersistDataProtectionKeysToRedis = true
            //    EnableLogging = false,
            //};
            //services.AddLiteXRedisCacheFactory(providerName: "redis", config: redisConfig2);


            //// TODO: register more providers using factory

            //#endregion

            //#region LiteX Caching (InMemory)

            //// 1. Use default configuration from appsettings.json's 'InMemoryConfig'
            //services.AddLiteXCache();

            ////or
            //// 2. load configuration settings using options.
            //services.AddLiteXCache(option =>
            //{
            //    option.EnableLogging = false;
            //});

            ////OR
            //// 3. Load configuration settings on your own.
            //// (e.g. appsettings, database, hardcoded)
            //var inMemoryConfig = new InMemoryConfig()
            //{
            //    EnableLogging = false,
            //};
            //services.AddLiteXCache(inMemoryConfig);

            //#endregion

            //#region LiteX Caching (Redis)

            //// 1. Use default configuration from appsettings.json's 'RedisConfig'
            //services.AddLiteXRedisCache();

            ////OR
            //// 2. Load configuration settings using options.
            //services.AddLiteXRedisCache(option =>
            //{
            //    option.RedisCachingConnectionString = "127.0.0.1:6379,ssl=False";
            //    //option.PersistDataProtectionKeysToRedis = true;
            //    option.EnableLogging = false;
            //});

            ////OR
            //// 3. Load configuration settings on your own.
            //// (e.g. appsettings, database, hardcoded)
            //var redisConfig = new RedisConfig()
            //{
            //    RedisCachingConnectionString = "127.0.0.1:6379,ssl=False",
            //    //PersistDataProtectionKeysToRedis = true
            //    EnableLogging = false,
            //};
            //services.AddLiteXRedisCache(redisConfig);

            //#endregion


            //#region LiteX Caching (SQLite)

            //// 1. Use default configuration from appsettings.json's 'SQLiteConfig'
            //services.AddLiteXSQLiteCache();

            ////OR
            //// 2. Load configuration settings using options.
            //services.AddLiteXSQLiteCache(option =>
            //{
            //    option.FileName = "";
            //    option.FilePath = "";
            //    option.OpenMode = Microsoft.Data.Sqlite.SqliteOpenMode.ReadWriteCreate;
            //    option.CacheMode = Microsoft.Data.Sqlite.SqliteCacheMode.Default;
            //    option.EnableLogging = false;
            //});

            ////OR
            //// 3. Load configuration settings on your own.
            //// (e.g. appsettings, database, hardcoded)
            //var sqLiteConfig = new SQLiteConfig()
            //{
            //    FileName = "",
            //    FilePath = "",
            //    OpenMode = Microsoft.Data.Sqlite.SqliteOpenMode.ReadWriteCreate,
            //    CacheMode = Microsoft.Data.Sqlite.SqliteCacheMode.Default,
            //    EnableLogging = false,
            //};
            //services.AddLiteXSQLiteCache(sqLiteConfig);

            //#endregion


            //#region LiteX Caching (Memcached)

            //// 1. Use default configuration from appsettings.json's 'MemcachedConfig'
            //services.AddLiteXMemcachedCache(providerOption =>
            //{
            //    providerOption.Protocol = Enyim.Caching.Memcached.MemcachedProtocol.Binary;
            //    providerOption.Servers = new System.Collections.Generic.List<Enyim.Caching.Configuration.Server>() { new Enyim.Caching.Configuration.Server() { Address = "", Port = 0 } };

            //    // configure rest of the options as needed
            //});

            ////OR
            //// 2. Load configuration settings using options.
            //services.AddLiteXMemcachedCache(providerOption =>
            //{
            //    providerOption.Protocol = Enyim.Caching.Memcached.MemcachedProtocol.Binary;
            //    providerOption.Servers = new System.Collections.Generic.List<Enyim.Caching.Configuration.Server>() { new Enyim.Caching.Configuration.Server() { Address = "", Port = 0 } };

            //    // configure rest of the options as needed
            //}, option =>
            //{
            //    option.PersistDataProtectionKeysToMemcached = true;
            //    option.EnableLogging = false;
            //});

            ////OR
            //// 3. Load configuration settings on your own.
            //// (e.g. appsettings, database, hardcoded)
            //var memcachedConfig = new MemcachedConfig()
            //{
            //    PersistDataProtectionKeysToMemcached = true,
            //    EnableLogging = false,
            //};
            //services.AddLiteXMemcachedCache(providerOption =>
            //{
            //    providerOption.Protocol = Enyim.Caching.Memcached.MemcachedProtocol.Binary;
            //    providerOption.Servers = new System.Collections.Generic.List<Enyim.Caching.Configuration.Server>() { new Enyim.Caching.Configuration.Server() { Address = "", Port = 0 } };

            //    // configure rest of the options as needed
            //}, memcachedConfig);

            #endregion

            #endregion


            services.AddLiteXCacheSwagger();

            services.AddLiteXLogging();

            services.AddHttpContextAccessor();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();


            #region LiteX Caching

            ////Memcached
            //app.UseLiteXMemcachedCache();

            ////SQLite
            //app.UseLiteXSQLiteCache();

            #endregion


            app.UseLiteXCacheSwagger();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
