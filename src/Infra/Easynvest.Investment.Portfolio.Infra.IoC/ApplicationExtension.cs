using System;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Easynvest.Investment.Portfolio.Domain.Notifications;

namespace Easynvest.Investment.Portfolio.Infra.IoC
{
    public static class ApplicationExtension
    {
        public static void AddDomainNotification(this IServiceCollection services)
        {
            services.AddScoped<NotificationPool>();
        }
    }
}
