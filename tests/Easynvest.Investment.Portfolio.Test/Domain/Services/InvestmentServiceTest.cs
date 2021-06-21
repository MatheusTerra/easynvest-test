using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using Easynvest.Investment.Portfolio.Domain.Configs;
using Easynvest.Investment.Portfolio.Domain.Entities;
using Easynvest.Investment.Portfolio.Domain.Notifications;
using Easynvest.Investment.Portfolio.Domain.Queries.Repositories;
using Easynvest.Investment.Portfolio.Domain.Services;
using Easynvest.Investment.Portfolio.Test.Customization;
using NSubstitute;
using Xunit;

namespace Easynvest.Investment.Portfolio.Test.Domain.Services
{
    public class InvestmentServiceTest
    {
        private readonly IFixture _fixture;

        public InvestmentServiceTest()
        {
            _fixture = new Fixture().Customize(new AutoPopulatedNSubstitutePropertiesCustomization());
        }

        [Fact]
        public void GetInvestments_Should_Return_Investments_When_Success()
        {
            var investmentsIRFeeConfig = _fixture.Create<InvestmentsIRFeeConfig>();

            var tesouroDiretoQueryRepository = Substitute.For<ITesouroDiretoQueryRepository>();
            tesouroDiretoQueryRepository.Get().Returns(_fixture.Create<IEnumerable<TesouroDireto>>());

            var rendaFixaQueryRepository = Substitute.For<IRendaFixaQueryRepository>();
            rendaFixaQueryRepository.Get().Returns(_fixture.Create<IEnumerable<RendaFixa>>());

            var fundosQueryRepository = Substitute.For<IFundosQueryRepository>();
            fundosQueryRepository.Get().Returns(_fixture.Create<IEnumerable<Fundos>>());

            _fixture.Register(() => investmentsIRFeeConfig);
            _fixture.Register(() => tesouroDiretoQueryRepository);
            _fixture.Register(() => rendaFixaQueryRepository);
            _fixture.Register(() => fundosQueryRepository);

            var investmentService = _fixture.Create<InvestmentService>();
            var response = investmentService.GetInvestments().Result;

            Assert.NotEmpty(response);
            Assert.False(investmentService.HasNotifications);
        }

        [Fact]
        public void GetInvestments_Should_Not_Return_TesouroDireto_When_Failure()
        {
            int expected = 4;

            var notificationPool = new NotificationPool();

            var rendaFixaList = new List<RendaFixa>
            {
                _fixture.Create<RendaFixa>(),
                _fixture.Create<RendaFixa>()
            };

            var fundosList = new List<Fundos>
            {
                _fixture.Create<Fundos>(),
                _fixture.Create<Fundos>()
            };

            var investmentsIRFeeConfig = _fixture.Create<InvestmentsIRFeeConfig>();

            var tesouroDiretoQueryRepository = Substitute.For<ITesouroDiretoQueryRepository>();
            tesouroDiretoQueryRepository.When(mock => mock.Get())
                .Do(call =>
                {
                    notificationPool.Add("Error test", NotificationLevel.Validation);
                    tesouroDiretoQueryRepository.Notifications.Returns(notificationPool.Notifications);
                    tesouroDiretoQueryRepository.HasNotifications.Returns(notificationPool.HasNotifications);
                });

            var rendaFixaQueryRepository = Substitute.For<IRendaFixaQueryRepository>();
            rendaFixaQueryRepository.Get().Returns(rendaFixaList);

            var fundosQueryRepository = Substitute.For<IFundosQueryRepository>();
            fundosQueryRepository.Get().Returns(fundosList);

            _fixture.Register(() => investmentsIRFeeConfig);
            _fixture.Register(() => tesouroDiretoQueryRepository);
            _fixture.Register(() => rendaFixaQueryRepository);
            _fixture.Register(() => fundosQueryRepository);
            _fixture.Register(() => notificationPool);

            var investmentService = _fixture.Create<InvestmentService>();
            var response = investmentService.GetInvestments().Result;

            Assert.NotEmpty(response);
            Assert.Equal(expected, response.Count());
            Assert.True(investmentService.HasNotifications);
        }

        [Fact]
        public void GetInvestments_Should_Not_Return_RendaFixa_When_Failure()
        {
            int expected = 4;

            var notificationPool = new NotificationPool();

            var tesouroDiretoList = new List<TesouroDireto>
            {
                _fixture.Create<TesouroDireto>(),
                _fixture.Create<TesouroDireto>()
            };

            var fundosList = new List<Fundos>
            {
                _fixture.Create<Fundos>(),
                _fixture.Create<Fundos>()
            };

            var investmentsIRFeeConfig = _fixture.Create<InvestmentsIRFeeConfig>();

            var tesouroDiretoQueryRepository = Substitute.For<ITesouroDiretoQueryRepository>();
            tesouroDiretoQueryRepository.Get().Returns(tesouroDiretoList);

            var rendaFixaQueryRepository = Substitute.For<IRendaFixaQueryRepository>();
            rendaFixaQueryRepository.When(mock => mock.Get())
                .Do(call =>
                {
                    notificationPool.Add("Error test", NotificationLevel.Validation);
                    rendaFixaQueryRepository.Notifications.Returns(notificationPool.Notifications);
                    rendaFixaQueryRepository.HasNotifications.Returns(notificationPool.HasNotifications);
                });

            var fundosQueryRepository = Substitute.For<IFundosQueryRepository>();
            fundosQueryRepository.Get().Returns(fundosList);

            _fixture.Register(() => investmentsIRFeeConfig);
            _fixture.Register(() => tesouroDiretoQueryRepository);
            _fixture.Register(() => rendaFixaQueryRepository);
            _fixture.Register(() => fundosQueryRepository);
            _fixture.Register(() => notificationPool);

            var investmentService = _fixture.Create<InvestmentService>();
            var response = investmentService.GetInvestments().Result;

            Assert.NotEmpty(response);
            Assert.Equal(expected, response.Count());
            Assert.True(investmentService.HasNotifications);
        }

        [Fact]
        public void GetInvestments_Should_Not_Return_Fundos_When_Failure()
        {
            int expected = 4;

            var notificationPool = new NotificationPool();

            var tesouroDiretoList = new List<TesouroDireto>
            {
                _fixture.Create<TesouroDireto>(),
                _fixture.Create<TesouroDireto>()
            };

            var rendaFixaList = new List<RendaFixa>
            {
                _fixture.Create<RendaFixa>(),
                _fixture.Create<RendaFixa>()
            };

            var investmentsIRFeeConfig = _fixture.Create<InvestmentsIRFeeConfig>();

            var tesouroDiretoQueryRepository = Substitute.For<ITesouroDiretoQueryRepository>();
            tesouroDiretoQueryRepository.Get().Returns(tesouroDiretoList);

            var rendaFixaQueryRepository = Substitute.For<IRendaFixaQueryRepository>();
            rendaFixaQueryRepository.Get().Returns(rendaFixaList);

            var fundosQueryRepository = Substitute.For<IFundosQueryRepository>();
            fundosQueryRepository.When(mock => mock.Get())
                .Do(call =>
                {
                    notificationPool.Add("Error test", NotificationLevel.Validation);
                    fundosQueryRepository.Notifications.Returns(notificationPool.Notifications);
                    fundosQueryRepository.HasNotifications.Returns(notificationPool.HasNotifications);
                });

            _fixture.Register(() => investmentsIRFeeConfig);
            _fixture.Register(() => tesouroDiretoQueryRepository);
            _fixture.Register(() => rendaFixaQueryRepository);
            _fixture.Register(() => fundosQueryRepository);
            _fixture.Register(() => notificationPool);

            var investmentService = _fixture.Create<InvestmentService>();
            var response = investmentService.GetInvestments().Result;

            Assert.NotEmpty(response);
            Assert.Equal(expected, response.Count());
            Assert.True(investmentService.HasNotifications);
        }
    }
}