using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Easynvest.Investment.Portfolio.API.Models
{
    [DataContract]
    public class InvestimentoModel
    {
        [DataMember(Name = "valorTotal")]
        public decimal ValorTotal { get; set; }

        [DataMember(Name = "investimentos")]
        public IEnumerable<InvestimentoItemModel> Investimentos { get; set; }
    }

    [DataContract]
    public class InvestimentoItemModel
    {
        [DataMember(Name = "nome")]
        public string Nome { get; set; }

        [DataMember(Name = "valorInvestimento")]
        public decimal ValorInvestido { get; set; }

        [DataMember(Name = "valorTotal")]
        public decimal ValorTotal { get; set; }

        [DataMember(Name = "vencimento")]
        public DateTime Vencimento { get; set; }

        [DataMember(Name = "Ir")]
        public decimal IR { get; set; }

        [DataMember(Name = "valorResgate")]
        public decimal ValorResgate { get; set; }
    }
}