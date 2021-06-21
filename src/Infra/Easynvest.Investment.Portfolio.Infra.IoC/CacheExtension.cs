using Easynvest.Investment.Portfolio.Infra.Cache.Clients;
using Easynvest.Investment.Portfolio.Infra.Cache.Clients.Abstracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Easynvest.Investment.Portfolio.Infra.IoC
{
    public static class CacheExtension
    {
        public static void AddCache(this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
        {
            if (isDevelopment)
                services.AddDistributedMemoryCache();
            else
                services.AddStackExchangeRedisCache(options => options.Configuration = configuration.GetSection("RedisConfig:Host").Value);

            services.AddSingleton<ICacheClient, CacheClient>();
        }
    }
}