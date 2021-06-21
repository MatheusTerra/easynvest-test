using System.Collections.Generic;
using System.Net;
using AutoFixture;
using Easynvest.Investment.Portfolio.API.Controllers;
using Easynvest.Investment.Portfolio.API.Models;
using Easynvest.Investment.Portfolio.Infra.Config;
using Easynvest.Investment.Portfolio.Infra.Http.Clients.Abstract;
using Easynvest.Investment.Portfolio.Test.Customization;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Easynvest.Investment.Portfolio.Test.API.Controllers
{
    public class HealthControllerTest
    {
        private readonly IFixture _fixture;

        public HealthControllerTest()
        {
            _fixture = new Fixture().Customize(new ControllerCustomization())
                                    .Customize(new AutoPopulatedNSubstitutePropertiesCustomization());
        }

        [Fact]
        public void HealthCheck_Should_Return_Health_True_When_Success()
        {
            var controller = _fixture.Build<HealthController>().OmitAutoProperties().Create();
            var response = controller.HealthCheck().Result as OkObjectResult;
            var model = response.Value as HealthCheckModel;

            Assert.NotNull(model);
            Assert.True(model.Online);
            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public void HealthCheckDetails_Should_Return_All_Online_When_Services_Success()
        {
            var servicesConfig = _fixture.Create<ServicesConfig>();

            var restClient = Substitute.For<IRestClient>();
            restClient.Get(Arg.Any<string>(), Arg.Any<int>()).Returns(true);
            
            _fixture.Register(() => restClient);
            _fixture.Register(() => servicesConfig);

            var controller = _fixture.Build<HealthController>().OmitAutoProperties().Create();
            var response = controller.HealthCheckDetails().Result as OkObjectResult;
            var model = response.Value as IEnumerable<HealthCheckModel>;

            Assert.NotEmpty(model);
            Assert.All(model, item => Assert.True(item.Online));
            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
        }
        
        [Fact]
        public void HealthCheckDetails_Should_Return_One_Offline_When_Service_Failure()
        {
            var servicesConfig = _fixture.Create<ServicesConfig>();

            var restClient = Substitute.For<IRestClient>();
            restClient.Get(Arg.Any<string>(), Arg.Any<int>()).Returns(x => true, x => false, x => true);
            
            _fixture.Register(() => restClient);
            _fixture.Register(() => servicesConfig);

            var controller = _fixture.Build<HealthController>().OmitAutoProperties().Create();
            var response = controller.HealthCheckDetails().Result as OkObjectResult;
            var model = response.Value as IEnumerable<HealthCheckModel>;

            Assert.NotEmpty(model);
            Assert.Single(model, item => !item.Online);
            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
        }
    }
}