using System.Text;
using AutoFixture;
using Easynvest.Investment.Portfolio.Infra.Cache.Clients;
using Easynvest.Investment.Portfolio.Infra.DTO;
using Microsoft.Extensions.Caching.Distributed;
using NSubstitute;
using Xunit;

namespace Easynvest.Investment.Portfolio.Test.Infra.Cache.Clients
{
    public class CacheClientTest
    {
        private readonly IFixture _fixture;

        public CacheClientTest()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void GetValueAsync_Should_Return_Cache_When_Key_Exists()
        {
            var expected = "test";

            var cache = Substitute.For<IDistributedCache>();
            cache.GetAsync(Arg.Any<string>()).Returns(Encoding.ASCII.GetBytes(expected));

            _fixture.Register(() => cache);

            var cacheClient = _fixture.Create<CacheClient>();
            var response = cacheClient.GetValueAsync("key").Result;

            Assert.NotEmpty(response);
            Assert.Equal(expected, response);
        }

        [Fact]
        public void GetValueAsync_Should_Return_Cache_Object_When_Key_Exists()
        {
            var json = "{\"tds\":[{\"valorInvestido\":799.472,\"valorTotal\":829.68,\"vencimento\":\"2025-03-01T00:00:00\",\"dataDeCompra\":\"2015-03-01T00:00:00\",\"iof\":0,\"indice\":\"SELIC\",\"tipo\":\"TD\",\"nome\":\"Tesouro Selic 2025\"}]}";

            var cache = Substitute.For<IDistributedCache>();
            cache.GetAsync(Arg.Any<string>()).Returns(Encoding.ASCII.GetBytes(json));

            _fixture.Register(() => cache);

            var cacheClient = _fixture.Create<CacheClient>();
            var response = cacheClient.GetValueAsync<TesouroDiretoDTO>("key").Result;

            Assert.NotNull(response);
            Assert.NotEmpty(response.Items);
            Assert.IsAssignableFrom<TesouroDiretoDTO>(response);
        }
    }
}