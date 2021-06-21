using System.Collections.Generic;
using AutoFixture;
using Easynvest.Investment.Portfolio.Domain.Entities;
using Easynvest.Investment.Portfolio.Domain.Notifications;
using Easynvest.Investment.Portfolio.Infra.Cache.Clients.Abstracts;
using Easynvest.Investment.Portfolio.Infra.DTO;
using Easynvest.Investment.Portfolio.Infra.Http.Clients.Abstract;
using Easynvest.Investment.Portfolio.Infra.Repositories.Queries;
using Easynvest.Investment.Portfolio.Test.Customization;
using NSubstitute;
using Xunit;

namespace Easynvest.Investment.Portfolio.Test.Infra.Infra.Repositories.Queries
{
    public class FundosQueryRepositoryTest
    {
        private readonly IFixture _fixture;

        public FundosQueryRepositoryTest()
        {
            _fixture = new Fixture().Customize(new AutoPopulatedNSubstitutePropertiesCustomization());
        }

        [Fact]
        public void Get_Should_Return_Fundos_Not_Cache_When_Success()
        {
            var cacheClient = Substitute.For<ICacheClient>();
            cacheClient.GetValueAsync<IEnumerable<Fundos>>(Arg.Any<string>()).Returns((IEnumerable<Fundos>)null);

            var restClient = Substitute.For<IRestClient>();
            restClient.Get<FundosDTO>(Arg.Any<string>(), Arg.Any<int>()).Returns(_fixture.Create<FundosDTO>());

            _fixture.Register(() => cacheClient);
            _fixture.Register(() => restClient);

            var repository = _fixture.Create<FundosQueryRepository>();
            var response = repository.Get().Result;

            Assert.NotEmpty(response);
            Assert.False(repository.HasNotifications);
        }

        [Fact]
        public void Get_Should_Return_Fundos_From_Cache_When_Success()
        {
            var cacheClient = Substitute.For<ICacheClient>();
            cacheClient.GetValueAsync<IEnumerable<Fundos>>(Arg.Any<string>()).Returns(_fixture.Create<IEnumerable<Fundos>>());

            _fixture.Register(() => cacheClient);

            var repository = _fixture.Create<FundosQueryRepository>();
            var response = repository.Get().Result;

            Assert.NotEmpty(response);
            Assert.False(repository.HasNotifications);
        }

        [Fact]
        public void Get_Should_Return_Notification_When_Failure()
        {
            var notificationPool = new NotificationPool();

            var cacheClient = Substitute.For<ICacheClient>();
            cacheClient.GetValueAsync<IEnumerable<Fundos>>(Arg.Any<string>()).Returns((IEnumerable<Fundos>)null);

            var restClient = Substitute.For<IRestClient>();
            restClient.When(mock => mock.Get<FundosDTO>(Arg.Any<string>(), Arg.Any<int>()))
                .Do(call =>
                {
                    notificationPool.Add("Error test", NotificationLevel.Validation);
                    restClient.Notifications.Returns(notificationPool.Notifications);
                    restClient.HasNotifications.Returns(notificationPool.HasNotifications);
                });

            _fixture.Register(() => cacheClient);
            _fixture.Register(() => restClient);
            _fixture.Register(() => notificationPool);

            var repository = _fixture.Create<FundosQueryRepository>();
            var response = repository.Get().Result;

            Assert.Null(response);
            Assert.True(repository.HasNotifications);
            Assert.NotEmpty(repository.Notifications);
        }
    }
}