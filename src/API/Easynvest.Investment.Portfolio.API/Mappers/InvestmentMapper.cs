using System.Collections.Generic;
using System.Linq;
using Easynvest.Investment.Portfolio.API.Models;
using Easynvest.Investment.Portfolio.Domain.Entities;

namespace Easynvest.Investment.Portfolio.API.Mappers
{
    public static class InvestmentMapper
    {
        public static InvestimentoModel Map(this IEnumerable<Investimento> domain)
        {
            if (domain == null)
                return null;

            return new InvestimentoModel
            {
                ValorTotal = domain.Sum(x => x.ValorTotal),
                Investimentos = domain.Select(x => x.Map()).OrderBy(o => o.Vencimento)
            };
        }

        private static InvestimentoItemModel Map(this Investimento domain)
        {
            if (domain == null)
                return null;

            return new InvestimentoItemModel
            {
                Nome = domain.Nome,
                ValorInvestido = domain.ValorInvestido,
                ValorTotal = domain.ValorTotal,
                Vencimento = domain.Vencimento,
                IR = domain.IR,
                ValorResgate = domain.ValorResgate
            };
        }
    }
}