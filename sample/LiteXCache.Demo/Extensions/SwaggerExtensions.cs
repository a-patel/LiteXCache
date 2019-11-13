#region Imports
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Examples;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
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
                options.SwaggerDoc("v8", new Info
                {
                    Version = "v8",
                    Title = "LiteX Cache",
                    Description = "LiteX Cache (InMemory, Redis, Memcached, SQLite)",
                    TermsOfService = "None",
                    Contact = new Contact() { Name = "Aashish Patel", Email = "toaashishpatel@outlook.com", Url = "https://aashishpatel.netlify.com/" },
                    License = new License() { Name = "LiteX LICENSE", Url = "https://github.com/a-patel/LiteXCache/blob/master/LICENSE" }
                });


                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
                //var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, $"LiteXCache.Demo.xml");
                //options.IncludeXmlComments(filePath);
                ////options.IncludeXmlComments(GetXmlCommentsPath());

                options.DescribeAllEnumsAsStrings();
                options.IgnoreObsoleteProperties();
                options.IgnoreObsoleteActions();

                options.OperationFilter<AddFileParamTypesOperationFilter>(); //Register File Upload Operation Filter

                #region Authorization

                // Swagger 2.+ support
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

                options.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                options.AddSecurityRequirement(security);

                #endregion
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
