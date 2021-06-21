using System.Collections.Generic;
using System.Linq;
using Easynvest.Investment.Portfolio.Domain.Entities;
using Easynvest.Investment.Portfolio.Infra.DTO;

namespace Easynvest.Investment.Portfolio.Infra.Mappers
{
    public static class FundosMapper
    {
        public static IEnumerable<Fundos> Map(this FundosDTO dto)
        {
            if (dto?.Items == null)
                return new List<Fundos>();

            return dto.Items.Select(x => x.Map());
        }

        private static Fundos Map(this FundosItemDTO dto)
        {
            if (dto == null)
                return null;

            return new Fundos(
                dto.CapitalInvestido,
                dto.ValorAtual,
                dto.DataResgate,
                dto.DataCompra,
                dto.Iof,
                dto.Nome,
                dto.TotalTaxas,
                dto.Quantity
            );
        }
    }
}