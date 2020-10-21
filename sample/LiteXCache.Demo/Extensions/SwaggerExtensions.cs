#region Imports
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Reflection;
#endregion

namespace LiteXCache.Demo
{
    /// <summary>
    /// Swagger extensions.
    /// </summary>
    public static class SwaggerExtensions
    {
        /// <summary>
        /// Add LiteX Cache Swagger services
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddLiteXCacheSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v8", new OpenApiInfo
                {
                    Version = "v8",
                    Title = "LiteX Cache",
                    Description = "LiteX Cache (InMemory, Redis, Memcached, SQLite)",
                    TermsOfService = new Uri("https://github.com/a-patel/LiteXCache/blob/master/LICENSE"),
                    Contact = new OpenApiContact() { Name = "Ashish Patel", Email = "toaashishpatel@gmail.com", Url = new Uri("https://aashishpatel.netlify.app/") },
                    License = new OpenApiLicense() { Name = "LICENSE", Url = new Uri("https://github.com/a-patel/LiteXCache/blob/master/LICENSE") }
                });


                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                //// Set the comments path for the Swagger JSON and UI. For all libraries
                //var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
                //xmlFiles.ForEach(xmlFile => options.IncludeXmlComments(xmlFile));

                options.DescribeAllEnumsAsStrings();
                options.IgnoreObsoleteProperties();
                options.IgnoreObsoleteActions();
            });


            return services;
        }

        /// <summary>
        /// Use LiteX Cache Swagger services
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseLiteXCacheSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v8/swagger.json", "LiteX Cache (V8)");
                options.DocumentTitle = "LiteX Cache";
                options.DocExpansion(DocExpansion.None);
                options.DisplayRequestDuration();
            });

            return app;
        }
    }
}
