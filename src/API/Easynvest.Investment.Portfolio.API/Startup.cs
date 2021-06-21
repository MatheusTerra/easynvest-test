using Easynvest.Investment.Portfolio.API.Swagger.Extensions;
using Easynvest.Investment.Portfolio.Infra.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Easynvest.Investment.Portfolio.API
{
    public class Startup
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHttpClient();
            services.AddDomainNotification();
            services.AddCache(Configuration, _webHostEnvironment.IsDevelopment());
            services.AddConfigs(Configuration);
            services.AddClients();
            services.AddQueryRepositories();
            services.AddServices();
            services.AddQueries();

            services.AddSwaggerDefinitions();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (_webHostEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{SwaggerExtension.Version}/swagger.json", "API Carteira de Investimentos");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
