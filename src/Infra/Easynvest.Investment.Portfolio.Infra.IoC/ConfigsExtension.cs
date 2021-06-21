using Easynvest.Investment.Portfolio.Domain.Configs;
using Easynvest.Investment.Portfolio.Infra.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Easynvest.Investment.Portfolio.Infra.IoC
{
    public static class ConfigsExtension
    {
        public static void AddConfigs(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(x => configuration.GetSection("ServicesConfig").Get<ServicesConfig>());
            services.AddScoped(x => configuration.GetSection("InvestmentsIRFeeConfig").Get<InvestmentsIRFeeConfig>());
        }
    }
}