using Easynvest.Investment.Portfolio.Domain.Queries.Repositories;
using Easynvest.Investment.Portfolio.Infra.Repositories.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Easynvest.Investment.Portfolio.Infra.IoC
{
    public static class QueryRepositoriesExtension
    {
        public static void AddQueryRepositories(this IServiceCollection services)
        {
            services.AddTransient<ITesouroDiretoQueryRepository, TesouroDiretoQueryRepository>();
            services.AddTransient<IRendaFixaQueryRepository, RendaFixaQueryRepository>();
            services.AddTransient<IFundosQueryRepository, FundosQueryRepository>();
        }
    }
}