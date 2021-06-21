using System.Collections.Generic;
using System.Net;
using AutoFixture;
using Easynvest.Investment.Portfolio.API.Controllers;
using Easynvest.Investment.Portfolio.API.Models;
using Easynvest.Investment.Portfolio.Domain.Entities;
using Easynvest.Investment.Portfolio.Domain.Notifications;
using Easynvest.Investment.Portfolio.Domain.Queries.Abstracts;
using Easynvest.Investment.Portfolio.Test.Customization;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Easynvest.Investment.Portfolio.Test.API.Controllers
{
    public class InvestmentsControllerTest
    {
        private readonly IFixture _fixture;

        public InvestmentsControllerTest()
        {
            _fixture = new Fixture().Customize(new ControllerCustomization())
                                    .Customize(new AutoPopulatedNSubstitutePropertiesCustomization());
        }

        [Fact]
        public void Get_Should_Return_Investimento_When_Success()
        {
            var investimentos = _fixture.Create<List<Investimento>>();

            var investmentQuery = Substitute.For<IInvestmentQuery>();
            investmentQuery.GetInvestments().Returns(investimentos);

            _fixture.Register(() => investmentQuery);

            var controller = _fixture.Build<InvestmentsController>().OmitAutoProperties().Create();
            var response = controller.Get().Result as OkObjectResult;
            var model = response.Value as InvestimentoModel;

            Assert.NotNull(model);
            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public void Get_Should_Return_Empty_When_NotFound()
        {
            var investmentQuery = Substitute.For<IInvestmentQuery>();
            investmentQuery.GetInvestments().Returns(new List<Investimento>());

            _fixture.Register(() => investmentQuery);

            var controller = _fixture.Build<InvestmentsController>().OmitAutoProperties().Create();
            var response = controller.Get().Result as OkObjectResult;
            var model = response.Value as InvestimentoModel;

            Assert.NotNull(model);
            Assert.Empty(model.Investimentos);
            Assert.Equal(0, model.ValorTotal);
            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public void Get_Should_Return_Notification_When_Failure()
        {
            var notificationPool = new NotificationPool();

            var investmentQuery = Substitute.For<IInvestmentQuery>();
            investmentQuery.When(mock => mock.GetInvestments())
                .Do(call =>
                {
                    notificationPool.Add("Error test", NotificationLevel.Validation);
                    investmentQuery.Notifications.Returns(notificationPool.Notifications);
                    investmentQuery.HasNotifications.Returns(notificationPool.HasNotifications);
                });

            _fixture.Register(() => investmentQuery);
            _fixture.Register(() => notificationPool);

            var controller = _fixture.Build<InvestmentsController>().OmitAutoProperties().Create();
            var response = controller.Get().Result as ObjectResult;

            Assert.NotNull(response);
            Assert.Equal((int)HttpStatusCode.BadRequest, response.StatusCode);
            Assert.IsAssignableFrom<ErrorModel>(response.Value);
        }
    }
}