using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Easynvest.Investment.Portfolio.Infra.DTO
{
    [DataContract]
    public class RendaFixaDTO
    {
        [DataMember(Name = "lcis")]
        public IEnumerable<RendaFixaItemDTO> Items { get; set; }
    }

    [DataContract]
    public class RendaFixaItemDTO
    {
        [DataMember(Name = "capitalInvestido")]
        public decimal CapitalInvestido { get; set; }

        [DataMember(Name = "capitalAtual")]
        public decimal CapitalAtual { get; set; }

        [DataMember(Name = "quantidade")]
        public decimal Quantidade { get; set; }

        [DataMember(Name = "vencimento")]
        public DateTime Vencimento { get; set; }

        [DataMember(Name = "iof")]
        public decimal Iof { get; set; }

        [DataMember(Name = "outrasTaxas")]
        public decimal OutrasTaxas { get; set; }

        [DataMember(Name = "taxas")]
        public decimal Taxas { get; set; }

        [DataMember(Name = "indice")]
        public string Indice { get; set; }

        [DataMember(Name = "tipo")]
        public string Tipo { get; set; }

        [DataMember(Name = "nome")]
        public string Nome { get; set; }

        [DataMember(Name = "guarantidoFGC")]
        public bool GarantidoFGC { get; set; }

        [DataMember(Name = "dataOperacao")]
        public DateTime DataOperacao { get; set; }

        [DataMember(Name = "precoUnitario")]
        public decimal PrecoUnitario { get; set; }

        [DataMember(Name = "primario")]
        public bool Primario { get; set; }
    }
}