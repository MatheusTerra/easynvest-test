using Easynvest.Investment.Portfolio.Infra.Http.Clients;
using Easynvest.Investment.Portfolio.Infra.Http.Clients.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace Easynvest.Investment.Portfolio.Infra.IoC
{
    public static class ClientsExtension
    {
        public static void AddClients(this IServiceCollection services)
        {
            services.AddScoped<IRestClient, RestClient>();
        }
    }
}