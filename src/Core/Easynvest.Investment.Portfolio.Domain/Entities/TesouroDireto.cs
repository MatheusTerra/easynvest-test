using System;
using Easynvest.Investment.Portfolio.Domain.Entities.Abstracts;

namespace Easynvest.Investment.Portfolio.Domain.Entities
{
    public class TesouroDireto : InvestimentoBase
    {
        public TesouroDireto(decimal valorInvestido, decimal valorTotal, DateTime vencimento,
            DateTime dataDeCompra, decimal iof, string indice, string tipo, string nome)
        {
            ValorInvestido = valorInvestido;
            ValorTotal = valorTotal;
            Vencimento = vencimento;
            DataDeCompra = dataDeCompra;
            Iof = iof;
            Indice = indice;
            Tipo = tipo;
            Nome = nome;
        }

        public decimal ValorInvestido { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime Vencimento { get; set; }
        public DateTime DataDeCompra { get; set; }
        public decimal Iof { get; set; }
        public string Indice { get; set; }
        public string Tipo { get; set; }
        public string Nome { get; set; }

        public override decimal ValorResgate => base.GetValorResgate(DataDeCompra, Vencimento, ValorTotal);

        public override decimal IR(decimal taxa) => base.GetIR(taxa, ValorTotal - ValorInvestido);

        public override Investimento CreateInvestimento(decimal taxaIR)
        {
            return new Investimento(
                Nome,
                ValorInvestido,
                ValorTotal,
                Vencimento,
                IR(taxaIR),
                ValorResgate);
        }
    }
}