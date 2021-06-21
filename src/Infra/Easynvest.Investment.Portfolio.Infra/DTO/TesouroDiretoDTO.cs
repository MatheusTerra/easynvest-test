using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Easynvest.Investment.Portfolio.Infra.DTO
{
    [DataContract]
    public class TesouroDiretoDTO
    {
        [DataMember(Name = "tds")]
        public IEnumerable<TesouroDiretoItemDTO> Items { get; set; }
    }

    [DataContract]
    public class TesouroDiretoItemDTO
    {
        [DataMember(Name = "valorInvestido")]
        public decimal ValorInvestido { get; set; }

        [DataMember(Name = "valorTotal")]
        public decimal ValorTotal { get; set; }

        [DataMember(Name = "vencimento")]
        public DateTime Vencimento { get; set; }

        [DataMember(Name = "dataDeCompra")]
        public DateTime DataDeCompra { get; set; }

        [DataMember(Name = "iof")]
        public decimal Iof { get; set; }

        [DataMember(Name = "indice")]
        public string Indice { get; set; }

        [DataMember(Name = "tipo")]
        public string Tipo { get; set; }

        [DataMember(Name = "nome")]
        public string Nome { get; set; }
    }
}