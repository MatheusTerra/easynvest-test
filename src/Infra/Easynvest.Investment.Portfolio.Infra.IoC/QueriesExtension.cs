using Easynvest.Investment.Portfolio.Domain.Queries;
using Easynvest.Investment.Portfolio.Domain.Queries.Abstracts;
using Microsoft.Extensions.DependencyInjection;

namespace Easynvest.Investment.Portfolio.Infra.IoC
{
    public static class QueriesExtension
    {
        public static void AddQueries(this IServiceCollection services)
        {
            services.AddTransient<IInvestmentQuery, InvestmentQuery>();
        }
    }
}