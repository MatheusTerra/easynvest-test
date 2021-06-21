using System;
using System.Net.Http;
using AutoFixture;
using Easynvest.Investment.Portfolio.Domain.Notifications;
using Easynvest.Investment.Portfolio.Infra.DTO;
using Easynvest.Investment.Portfolio.Infra.Http.Clients;
using Easynvest.Investment.Portfolio.Test.Infra.Http.Fakes;
using NSubstitute;
using Xunit;

namespace Easynvest.Investment.Portfolio.Test.Infra.Http.Clients
{
    public class RestClientTest
    {
        private readonly IFixture _fixture;

        public RestClientTest()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Get_Should_Return_Object_When_Success()
        {
            var httpClient = new HttpClient(new FakeOKHttpMessageHandler()) { BaseAddress = new Uri("https://localhost") };

            var httpClientFactory = Substitute.For<IHttpClientFactory>();
            httpClientFactory.CreateClient().Returns(httpClient);

            _fixture.Register(() => httpClientFactory);

            var restClient = _fixture.Create<RestClient>();
            var response = restClient.Get<TesouroDiretoDTO>("http://localhost").Result;

            Assert.NotNull(response);
            Assert.NotEmpty(response.Items);
            Assert.False(restClient.HasNotifications);
        }

        [Fact]
        public void Get_Should_Return_Null_When_NotFound()
        {
            var httpClient = new HttpClient(new FakeNotFoundHttpMessageHandler()) { BaseAddress = new Uri("https://localhost") };

            var httpClientFactory = Substitute.For<IHttpClientFactory>();
            httpClientFactory.CreateClient().Returns(httpClient);

            _fixture.Register(() => httpClientFactory);

            var restClient = _fixture.Create<RestClient>();
            var response = restClient.Get<TesouroDiretoDTO>("http://localhost").Result;

            Assert.Null(response);
            Assert.False(restClient.HasNotifications);
        }

        [Fact]
        public void Get_Should_Return_Notification_When_BadRequest()
        {
            var httpClient = new HttpClient(new FakeBadRequestHttpMessageHandler()) { BaseAddress = new Uri("https://localhost") };

            var httpClientFactory = Substitute.For<IHttpClientFactory>();
            httpClientFactory.CreateClient().Returns(httpClient);

            _fixture.Register(() => httpClientFactory);

            var restClient = _fixture.Create<RestClient>();
            var response = restClient.Get<TesouroDiretoDTO>("http://localhost").Result;

            Assert.Null(response);
            Assert.True(restClient.HasNotifications);
        }

        [Fact]
        public void Get_Should_Return_Notification_When_Unauthorized()
        {
            var httpClient = new HttpClient(new FakeUnauthorizedHttpMessageHandler()) { BaseAddress = new Uri("https://localhost") };

            var httpClientFactory = Substitute.For<IHttpClientFactory>();
            httpClientFactory.CreateClient().Returns(httpClient);

            _fixture.Register(() => httpClientFactory);

            var restClient = _fixture.Create<RestClient>();
            var response = restClient.Get<TesouroDiretoDTO>("http://localhost").Result;

            Assert.Null(response);
            Assert.True(restClient.HasNotifications);
            Assert.All(restClient.Notifications, x => Assert.Equal(NotificationLevel.Forbiden, x.Level));
        }

        [Fact]
        public void Get_Should_Return_True_When_Success()
        {
            var httpClient = new HttpClient(new FakeOKHttpMessageHandler()) { BaseAddress = new Uri("https://localhost") };

            var httpClientFactory = Substitute.For<IHttpClientFactory>();
            httpClientFactory.CreateClient().Returns(httpClient);

            _fixture.Register(() => httpClientFactory);

            var restClient = _fixture.Create<RestClient>();
            var response = restClient.Get("http://localhost").Result;

            Assert.True(response);
        }

        [Fact]
        public void Get_Should_Return_False_When_Not_Success()
        {
            var httpClient = new HttpClient(new FakeBadRequestHttpMessageHandler()) { BaseAddress = new Uri("https://localhost") };

            var httpClientFactory = Substitute.For<IHttpClientFactory>();
            httpClientFactory.CreateClient().Returns(httpClient);

            _fixture.Register(() => httpClientFactory);

            var restClient = _fixture.Create<RestClient>();
            var response = restClient.Get("http://localhost").Result;

            Assert.False(response);
        }
    }
}