using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Easynvest.Investment.Portfolio.API.Models;
using Easynvest.Investment.Portfolio.Infra.Config;
using Easynvest.Investment.Portfolio.Infra.Http.Clients.Abstract;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Easynvest.Investment.Portfolio.API.Controllers
{
    [ApiController]
    [Route("healthcheck")]
    public class HealthController : ControllerBase
    {
        private readonly Dictionary<string, Resource> _services;
        private readonly IRestClient _restClient;

        private struct Resource
        {
            public string Url;
            public int Timeout;
        }

        public HealthController(ServicesConfig servicesConfig, IRestClient restClient)
        {
            _restClient = restClient;
            _services = GetServices(servicesConfig);
        }

        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, "Sucesso ao checar aplicação", typeof(HealthCheckModel))]
        [Produces("application/json")]
        [SwaggerOperation(Tags = new[] { "Aplicação" })]
        public async Task<IActionResult> HealthCheck()
        {
            var health = new HealthCheckModel("Me", true, GetType().Assembly.GetName().Version.ToString());
            return Ok(await Task.Run(() => health));
        }

        [HttpGet("details")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Sucesso ao checar aplicação e serviços", typeof(IEnumerable<HealthCheckModel>))]
        [Produces("application/json")]
        [SwaggerOperation(Tags = new[] { "Aplicação" })]
        public async Task<IActionResult> HealthCheckDetails()
        {
            var requests = new List<Task<HealthCheckModel>>();

            foreach (var service in _services.Keys)
            {
                var item = _services[service];
                requests.Add(TestUrl(service, item.Url, item.Timeout));
            }

            return Ok(await Task.WhenAll(requests));
        }

        private async Task<HealthCheckModel> TestUrl(string service, string url, int timeout)
        {
            var result = await _restClient.Get(url, timeout);
            return new HealthCheckModel(service, result);
        }

        private Dictionary<string, Resource> GetServices(ServicesConfig servicesConfig)
        {
            return new Dictionary<string, Resource>
            {
                {"Tesouro Direto", new Resource { Url = servicesConfig.TesouroDiretoRoute, Timeout = servicesConfig.Timeout } },
                {"Renda Fixa", new Resource { Url = servicesConfig.RendaFixaRoute, Timeout = servicesConfig.Timeout } },
                {"Fundos", new Resource { Url = servicesConfig.FundosRoute, Timeout = servicesConfig.Timeout } }
            };
        }
    }
}