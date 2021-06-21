using System.Net;
using System.Threading.Tasks;
using Easynvest.Investment.Portfolio.API.Mappers;
using Easynvest.Investment.Portfolio.API.Models;
using Easynvest.Investment.Portfolio.API.Models.Handlers;
using Easynvest.Investment.Portfolio.Domain.Queries.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Easynvest.Investment.Portfolio.API.Controllers
{
    [ApiController]
    [Route("investments")]
    public class InvestmentsController : ControllerBase
    {
        private readonly IInvestmentQuery _investmentQuery;

        public InvestmentsController(IInvestmentQuery investmentQuery)
        {
            _investmentQuery = investmentQuery;
        }

        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, "Sucesso", typeof(InvestimentoModel))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Fala ao obter investimentos", typeof(ErrorModel))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Erro inesperado ao obter investimentos", typeof(ErrorModel))]
        [Produces("application/json")]
        [SwaggerOperation(Tags = new[] { "Investimentos" })]
        public async Task<IActionResult> Get()
        {
            var investments = await _investmentQuery.GetInvestments();

            if (_investmentQuery.HasNotifications)
            {
                var error = ErrorHandler.CreateError("Falha ao obter Investimentos", _investmentQuery);
                return StatusCode(error.Code, error);
            }

            return Ok(investments.Map());
        }
    }
}
