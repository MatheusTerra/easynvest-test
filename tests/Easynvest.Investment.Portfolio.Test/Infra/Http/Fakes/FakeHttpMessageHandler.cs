using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Easynvest.Investment.Portfolio.Test.Infra.Http.Fakes
{
    public class FakeOKHttpMessageHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var json = "{\"tds\":[{\"valorInvestido\":799.472,\"valorTotal\":829.68,\"vencimento\":\"2025-03-01T00:00:00\",\"dataDeCompra\":\"2015-03-01T00:00:00\",\"iof\":0,\"indice\":\"SELIC\",\"tipo\":\"TD\",\"nome\":\"Tesouro Selic 2025\"}]}";

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json)
            };

            return Task.FromResult(response);
        }
    }

    public class FakeNotFoundHttpMessageHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent("")
            };

            return Task.FromResult(response);
        }
    }

    public class FakeBadRequestHttpMessageHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("Error")
            };

            return Task.FromResult(response);
        }
    }

    public class FakeUnauthorizedHttpMessageHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
            {
                Content = new StringContent("")
            };

            return Task.FromResult(response);
        }
    }

    public class FakeInternalServerErrorHttpMessageHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("InternalServerError")
            };

            return Task.FromResult(response);
        }
    }
}