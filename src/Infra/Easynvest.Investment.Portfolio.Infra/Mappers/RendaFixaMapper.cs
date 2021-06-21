using System.Collections.Generic;
using System.Linq;
using Easynvest.Investment.Portfolio.Domain.Entities;
using Easynvest.Investment.Portfolio.Infra.DTO;

namespace Easynvest.Investment.Portfolio.Infra.Mappers
{
    public static class RendaFixaMapper
    {
        public static IEnumerable<RendaFixa> Map(this RendaFixaDTO dto)
        {
            if (dto?.Items == null)
                return new List<RendaFixa>();

            return dto.Items.Select(x => x.Map());
        }

        private static RendaFixa Map(this RendaFixaItemDTO dto)
        {
            if (dto == null)
                return null;

            return new RendaFixa(
                dto.CapitalInvestido,
                dto.CapitalAtual,
                dto.Quantidade,
                dto.Vencimento,
                dto.Iof,
                dto.OutrasTaxas,
                dto.Taxas,
                dto.Indice,
                dto.Tipo,
                dto.Nome,
                dto.GarantidoFGC,
                dto.DataOperacao,
                dto.PrecoUnitario,
                dto.Primario
            );
        }
    }
}