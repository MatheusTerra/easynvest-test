using System;
using Easynvest.Investment.Portfolio.Domain.Entities.Abstracts;

namespace Easynvest.Investment.Portfolio.Domain.Entities
{
    public class RendaFixa : InvestimentoBase
    {
        public RendaFixa(decimal capitalInvestido, decimal capitalAtual, decimal quantidade,
            DateTime vencimento, decimal iof, decimal outrasTaxas, decimal taxas, string indice,
            string tipo, string nome, bool garantidoFGC, DateTime dataOperacao, decimal precoUnitario, bool primario)
        {
            CapitalInvestido = capitalInvestido;
            CapitalAtual = capitalAtual;
            Quantidade = quantidade;
            Vencimento = vencimento;
            Iof = iof;
            OutrasTaxas = outrasTaxas;
            Taxas = taxas;
            Indice = indice;
            Tipo = tipo;
            Nome = nome;
            GarantidoFGC = garantidoFGC;
            DataOperacao = dataOperacao;
            PrecoUnitario = precoUnitario;
            Primario = primario;
        }

        public decimal CapitalInvestido { get; set; }
        public decimal CapitalAtual { get; set; }
        public decimal Quantidade { get; set; }
        public DateTime Vencimento { get; set; }
        public decimal Iof { get; set; }
        public decimal OutrasTaxas { get; set; }
        public decimal Taxas { get; set; }
        public string Indice { get; set; }
        public string Tipo { get; set; }
        public string Nome { get; set; }
        public bool GarantidoFGC { get; set; }
        public DateTime DataOperacao { get; set; }
        public decimal PrecoUnitario { get; set; }
        public bool Primario { get; set; }

        public override decimal ValorResgate => base.GetValorResgate(DataOperacao, Vencimento, CapitalAtual);

        public override decimal IR(decimal taxa) => base.GetIR(taxa, CapitalAtual - CapitalInvestido);

        public override Investimento CreateInvestimento(decimal taxaIR)
        {
            return new Investimento(
                Nome,
                CapitalInvestido,
                CapitalAtual,
                Vencimento,
                IR(taxaIR),
                ValorResgate);
        }
    }
}