using System;

namespace Easynvest.Investment.Portfolio.Domain.Entities.Abstracts
{
    public abstract class InvestimentoBase
    {
        public abstract decimal IR(decimal taxa);
        public abstract decimal ValorResgate { get; }
        public abstract Investimento CreateInvestimento(decimal taxaIR);

        protected decimal GetValorResgate(DateTime dataInvestimento, DateTime vencimento, decimal valorTotal)
        {
            var percentage = GetPercentageFee(dataInvestimento, vencimento);

            return CalculateValorResgate(valorTotal, percentage);
        }

        protected decimal GetIR(decimal taxaIr, decimal rentabilidade)
        {
            return rentabilidade > 0 ? CalculateIR(taxaIr, rentabilidade) : 0;
        }

        private decimal CalculateIR(decimal taxaIr, decimal rentabilidade)
        {
            return (rentabilidade * taxaIr) / 100M;
        }

        private decimal GetPercentageFee(DateTime dataInvestimento, DateTime vencimento)
        {
            if (IsExpirated(vencimento))
                return 0;

            if (IsThreeMonthsToExpirationDate(vencimento))
                return 6;

            if (IsHalfCustodyTime(vencimento, dataInvestimento))
                return 15;

            return 30;
        }

        private bool IsExpirated(DateTime vencimento)
        {
            return DateTime.Now >= vencimento;
        }

        private bool IsThreeMonthsToExpirationDate(DateTime vencimento)
        {
            var date = vencimento.AddMonths(-3);
            return DateTime.Now > date;
        }

        private bool IsHalfCustodyTime(DateTime vencimento, DateTime dataInvestimento)
        {
            var halfInvestmentDays = (vencimento - dataInvestimento).Days / 2;
            var halfInvestmentDate = vencimento.AddDays(-halfInvestmentDays);

            return DateTime.Now > halfInvestmentDate;
        }

        private decimal CalculateValorResgate(decimal amount, decimal percentage)
        {
            return amount - ((amount * percentage) / 100);
        }
    }
}