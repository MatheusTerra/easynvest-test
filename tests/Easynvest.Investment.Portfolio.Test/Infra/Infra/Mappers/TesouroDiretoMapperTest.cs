using System.Collections.Generic;
using AutoFixture;
using Easynvest.Investment.Portfolio.Infra.DTO;
using Easynvest.Investment.Portfolio.Infra.Mappers;
using Xunit;

namespace Easynvest.Investment.Portfolio.Test.Infra.Infra.Mappers
{
    public class TesouroDiretoMapperTest
    {
        private readonly IFixture _fixture;

        public TesouroDiretoMapperTest()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Map_Should_Return_Domain_When_DTO_Is_NotNull()
        {
            var dto = _fixture.Create<TesouroDiretoDTO>();

            var domain = dto.Map();

            Assert.NotEmpty(domain);
        }

        [Fact]
        public void Map_Should_Return_Null_When_DTO_Is_Null()
        {
            var domain = ((TesouroDiretoDTO)null).Map();

            Assert.Empty(domain);
        }
    }
}