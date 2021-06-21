using System;

namespace Easynvest.Investment.Portfolio.Domain.Entities
{
    public class Investimento
    {
        public Investimento(string nome, decimal valorInvestido,
            decimal valorTotal, DateTime vencimento, decimal ir, decimal valorResgate)
        {
            Nome = nome;
            ValorInvestido = valorInvestido;
            ValorTotal = valorTotal;
            Vencimento = vencimento;
            IR = ir;
            ValorResgate = valorResgate;
        }

        public string Nome { get; set; }
        public decimal ValorInvestido { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime Vencimento { get; set; }
        public decimal IR { get; set; }
        public decimal ValorResgate { get; set; }
    }
}