using System.Collections.Generic;
using AutoFixture;
using Easynvest.Investment.Portfolio.Domain.Entities;
using Easynvest.Investment.Portfolio.Domain.Notifications;
using Easynvest.Investment.Portfolio.Domain.Queries;
using Easynvest.Investment.Portfolio.Domain.Services.Abstract;
using Easynvest.Investment.Portfolio.Test.Customization;
using NSubstitute;
using Xunit;

namespace Easynvest.Investment.Portfolio.Test.Domain.Queries
{
    public class InvestmentQueryTest
    {
        private readonly IFixture _fixture;

        public InvestmentQueryTest()
        {
            _fixture = new Fixture().Customize(new AutoPopulatedNSubstitutePropertiesCustomization());
        }

        [Fact]
        public void GetInvestments_Should_Return_Investments_When_Success()
        {
            var investmentService = Substitute.For<IInvestmentService>();
            investmentService.GetInvestments().Returns(_fixture.Create<IEnumerable<Investimento>>());

            _fixture.Register(() => investmentService);

            var query = _fixture.Create<InvestmentQuery>();
            var response = query.GetInvestments().Result;

            Assert.NotEmpty(response);
            Assert.False(query.HasNotifications);
        }

        [Fact]
        public void GetInvestments_Should_Return_Notfication_When_Failure()
        {
            var notificationPool = new NotificationPool();

            var investmentService = Substitute.For<IInvestmentService>();
            investmentService.When(mock => mock.GetInvestments())
                .Do(call =>
                {
                    notificationPool.Add("Error test", NotificationLevel.Validation);
                    investmentService.Notifications.Returns(notificationPool.Notifications);
                    investmentService.HasNotifications.Returns(notificationPool.HasNotifications);
                });

            _fixture.Register(() => investmentService);
            _fixture.Register(() => notificationPool);

            var investmentQuery = _fixture.Create<InvestmentQuery>();
            var response = investmentQuery.GetInvestments().Result;

            Assert.Null(response);
            Assert.True(investmentQuery.HasNotifications);
            Assert.NotEmpty(investmentQuery.Notifications);
        }
    }
}