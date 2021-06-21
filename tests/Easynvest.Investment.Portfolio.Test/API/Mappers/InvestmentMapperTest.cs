using System;
using System.Collections.Generic;
using AutoFixture;
using Easynvest.Investment.Portfolio.API.Mappers;
using Easynvest.Investment.Portfolio.Domain.Entities;
using Easynvest.Investment.Portfolio.Test.Customization;
using Xunit;

namespace Easynvest.Investment.Portfolio.Test.API.Mappers
{
    public class InvestmentMapperTest
    {
        private readonly IFixture _fixture;

        public InvestmentMapperTest()
        {
            _fixture = new Fixture().Customize(new AutoPopulatedNSubstitutePropertiesCustomization());
        }

        [Fact]
        public void Map_Should_Return_Model_When_Domain_Is_NotNull()
        {
            var domain = _fixture.Create<IEnumerable<Investimento>>();

            var model = domain.Map();

            Assert.NotNull(model);
            Assert.NotEmpty(model.Investimentos);
            Assert.True(model.ValorTotal > 0);
        }

        [Fact]
        public void Map_Should_Return_Null_When_Domain_Is_Null()
        {
            var model = ((IEnumerable<Investimento>)null).Map();

            Assert.Null(model);
        }

        public void Map_Should_Return_Items_When_Domain_Has_Items()
        {
            int expected = 350;

            var domain = new List<Investimento>
            {
                new Investimento("Test 1", 100, 150, DateTime.Now, 10, 150),
                new Investimento("Test 2", 100, 150, DateTime.Now, 10, 150),
                new Investimento("Test 3", 100, 150, DateTime.Now, 10, 150)
            };

            var model = domain.Map();

            Assert.NotNull(model);
            Assert.NotEmpty(model.Investimentos);
            Assert.Equal(expected, model.ValorTotal);
        }
    }
}