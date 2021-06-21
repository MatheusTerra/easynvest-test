using System;
using System.Reflection;
using Easynvest.Investment.Portfolio.API.Swagger.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Easynvest.Investment.Portfolio.API.Swagger.Extensions
{
    public static class SwaggerExtension
    {
        public static void AddSwaggerDefinitions(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc(Version, new OpenApiInfo
                {
                    Title = "API Carteira de Investimentos",
                    Version = FullVersion(),
                    Description = "API para consulta de investimentos",
                    Contact = new OpenApiContact
                    {
                        Name = "Matheus Terra",
                        Email = "matheus.tas@hotmail.com",
                        Url = new Uri("https://github.com/MatheusTerra")
                    }
                });
                c.DocumentFilter<TagsFilter>();
            });
        }

        private static string _version;
        private static string FullVersion()
        {
            if (string.IsNullOrEmpty(_version))
                _version = Assembly.GetEntryAssembly().GetName().Version.ToString();

            return _version;
        }

        public static string Version
        {
            get
            {
                var version = "0";
                var versionArr = FullVersion().Split('.');

                if (versionArr.Length > 0)
                    version = versionArr[0];

                return $"v{version}";
            }
        }
    }
}