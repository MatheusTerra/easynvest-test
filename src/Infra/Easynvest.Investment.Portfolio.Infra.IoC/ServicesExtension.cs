using Easynvest.Investment.Portfolio.Domain.Services;
using Easynvest.Investment.Portfolio.Domain.Services.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace Easynvest.Investment.Portfolio.Infra.IoC
{
    public static class ServicesExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IInvestmentService, InvestmentService>();
        }
    }
}