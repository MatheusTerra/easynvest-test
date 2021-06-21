using System;
using Easynvest.Investment.Portfolio.Domain.Entities.Abstracts;

namespace Easynvest.Investment.Portfolio.Domain.Entities
{
    public class Fundos : InvestimentoBase
    {
        public Fundos(decimal capitalInvestido, decimal valorAtual, DateTime dataResgate,
            DateTime dataCompra, decimal iof, string nome, decimal totalTaxas, int quantity)
        {
            CapitalInvestido = capitalInvestido;
            ValorAtual = valorAtual;
            DataResgate = dataResgate;
            DataCompra = dataCompra;
            Iof = iof;
            Nome = nome;
            TotalTaxas = totalTaxas;
            Quantity = quantity;
        }

        public decimal CapitalInvestido { get; set; }
        public decimal ValorAtual { get; set; }
        public DateTime DataResgate { get; set; }
        public DateTime DataCompra { get; set; }
        public decimal Iof { get; set; }
        public string Nome { get; set; }
        public decimal TotalTaxas { get; set; }
        public int Quantity { get; set; }

        public override decimal ValorResgate => base.GetValorResgate(DataCompra, DataResgate, ValorAtual);

        public override decimal IR(decimal taxa) => base.GetIR(taxa, ValorAtual - CapitalInvestido);

        public override Investimento CreateInvestimento(decimal taxaIR)
        {
            return new Investimento(
                Nome,
                CapitalInvestido,
                ValorAtual,
                DataResgate,
                IR(taxaIR),
                ValorResgate);
        }
    }
}