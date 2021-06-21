using System;
using System.Collections.Generic;
using Easynvest.Investment.Portfolio.Domain.Entities;
using Xunit;

namespace Easynvest.Investment.Portfolio.Test.Domain.Entities
{
    public class TesouroDiretoTest
    {
        [Theory]
        [InlineData(5000, 6000, 10, 100)]
        [InlineData(5000, -4950, 10, 0)]
        public void IR_Should_Return_IR_From_Params(decimal valorInvestido, decimal valorTotal, decimal taxaIR, decimal expected)
        {
            var tesouroDireto = new TesouroDireto(
                valorInvestido,
                valorTotal,
                DateTime.Now,
                DateTime.Now,
                0,
                "Test",
                "Test",
                "Test"
            );

            var ir = tesouroDireto.IR(taxaIR);

            Assert.Equal(expected, ir);
        }

        public static List<object[]> testData = new List<object[]>
        {
            new object[] { 1000, DateTime.Now.AddYears(4).Date, DateTime.Now.AddYears(-6), 850 },
            new object[] { 1000, DateTime.Now.AddMonths(2).Date, DateTime.Now.AddMonths(58), 940 },
            new object[] { 1000, DateTime.Now.AddDays(-1).Date, DateTime.Now.AddMonths(-5), 1000 },
            new object[] { 1000, DateTime.Now.AddYears(4).Date, DateTime.Now.AddYears(-3), 700 },
        };

        [Theory]
        [MemberData(nameof(testData))]
        public void ValorResgate_Should_Return_ValorResgate_From_Params(decimal valorTotal, DateTime vencimento, DateTime dataInvestimento, decimal expected)
        {
            var tesouroDireto = new TesouroDireto(
                5000,
                valorTotal,
                vencimento,
                dataInvestimento,
                0,
                "Test",
                "Test",
                "Test"
            );

            var valorResgate = tesouroDireto.ValorResgate;

            Assert.Equal(expected, valorResgate);
        }
    }
}