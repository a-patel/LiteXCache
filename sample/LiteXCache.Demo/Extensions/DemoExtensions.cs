#region Imports
using LiteX.Cache;
using LiteX.Cache.Core;
using LiteX.Cache.Memcached;
using LiteX.Cache.Redis;
using LiteX.Cache.SQLite;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Examples;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.IO;
#endregion

namespace LiteXCache.Demo
{
    /// <summary>
    /// LiteX Cache Demo extensions.
    /// </summary>
    public static class DemoExtensions
    {
        /// <summary>
        /// Add LiteX Cache Demo Configuration
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddLiteXCacheDemoConfiguration(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            CacheProviderType cacheProviderType = configuration.GetValue<CacheProviderType>("CacheProviderType");


            switch (cacheProviderType)
            {
                case CacheProviderType.InMemory:
                    services.AddLiteXCache();
                    break;

                case CacheProviderType.Redis:
                    services.AddLiteXRedisCache();
                    break;

                case CacheProviderType.Memcached:
                    //services.AddLiteXMemcachedCache(providerOption =>
                    //{
                    //    providerOption.Protocol = Enyim.Caching.Memcached.MemcachedProtocol.Binary;
                    //    providerOption.Servers = new System.Collections.Generic.List<Enyim.Caching.Configuration.Server>() { new Enyim.Caching.Configuration.Server() { Address = "", Port = 0 } };

                    //    // configure rest of the options as needed
                    //});
                    break;

                case CacheProviderType.SQLite:
                    services.AddLiteXSQLiteCache();
                    break;

                case CacheProviderType.PerRequest:
                    services.AddLiteXPerRequestCache();
                    break;

                case CacheProviderType.Other:
                    goto default;

                default:
                    services.AddLiteXCache();
                    break;
            }


            // add factory methods for all providers with default configuration
            services.AddLiteXInMemoryCacheFactory();
            services.AddLiteXRedisCacheFactory();
            //services.AddLiteXMemcachedCacheWithFactory();
            services.AddLiteXSQLiteCacheFactory();


            return services;
        }

        /// <summary>
        /// Use LiteX Cache Demo Configuration
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseLiteXCacheDemoConfiguration(this IApplicationBuilder app)
        {
            var configuration = app.ApplicationServices.GetRequiredService<IConfiguration>();
            CacheProviderType cacheProviderType = configuration.GetValue<CacheProviderType>("CacheProviderType");

            if (cacheProviderType == CacheProviderType.Memcached)
                app.UseLiteXMemcachedCache();
            if (cacheProviderType == CacheProviderType.SQLite)
                app.UseLiteXSQLiteCache();

            return app;
        }
    }
}
