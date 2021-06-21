using System.Collections.Generic;
using System.Linq;
using Easynvest.Investment.Portfolio.Domain.Entities;
using Easynvest.Investment.Portfolio.Infra.DTO;

namespace Easynvest.Investment.Portfolio.Infra.Mappers
{
    public static class TesouroDiretoMapper
    {
        public static IEnumerable<TesouroDireto> Map(this TesouroDiretoDTO dto)
        {
            if (dto?.Items == null)
                return new List<TesouroDireto>();

            return dto.Items.Select(x => x.Map());
        }

        private static TesouroDireto Map(this TesouroDiretoItemDTO dto)
        {
            if (dto == null)
                return null;

            return new TesouroDireto(
                dto.ValorInvestido,
                dto.ValorTotal,
                dto.Vencimento,
                dto.DataDeCompra,
                dto.Iof,
                dto.Indice,
                dto.Tipo,
                dto.Nome
            );
        }
    }
}