using System.Collections.Generic;
using AutoFixture;
using Easynvest.Investment.Portfolio.Infra.DTO;
using Easynvest.Investment.Portfolio.Infra.Mappers;
using Xunit;

namespace Easynvest.Investment.Portfolio.Test.Infra.Infra.Mappers
{
    public class RendaFixaMapperTest
    {
        private readonly IFixture _fixture;

        public RendaFixaMapperTest()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Map_Should_Return_Domain_When_DTO_Is_NotNull()
        {
            var dto = _fixture.Create<RendaFixaDTO>();

            var domain = dto.Map();

            Assert.NotEmpty(domain);
        }

        [Fact]
        public void Map_Should_Return_Null_When_DTO_Is_Null()
        {
            var domain = ((RendaFixaDTO)null).Map();

            Assert.Empty(domain);
        }
    }
}