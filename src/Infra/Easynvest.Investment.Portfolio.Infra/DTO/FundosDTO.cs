using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Easynvest.Investment.Portfolio.Infra.DTO
{
    [DataContract]
    public class FundosDTO
    {
        [DataMember(Name = "fundos")]
        public IEnumerable<FundosItemDTO> Items { get; set; }
    }

    [DataContract]
    public class FundosItemDTO
    {
        [DataMember(Name = "capitalInvestido")]
        public decimal CapitalInvestido { get; set; }

        [DataMember(Name = "ValorAtual")]
        public decimal ValorAtual { get; set; }

        [DataMember(Name = "dataResgate")]
        public DateTime DataResgate { get; set; }

        [DataMember(Name = "dataCompra")]
        public DateTime DataCompra { get; set; }

        [DataMember(Name = "iof")]
        public decimal Iof { get; set; }

        [DataMember(Name = "nome")]
        public string Nome { get; set; }

        [DataMember(Name = "totalTaxas")]
        public decimal TotalTaxas { get; set; }

        [DataMember(Name = "quantity")]
        public int Quantity { get; set; }
    }
}