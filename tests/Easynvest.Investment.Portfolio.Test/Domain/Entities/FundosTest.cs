using System;
using System.Collections.Generic;
using Easynvest.Investment.Portfolio.Domain.Entities;
using Xunit;

namespace Easynvest.Investment.Portfolio.Test.Domain.Entities
{
    public class FundosTest
    {
        [Theory]
        [InlineData(5000, 6000, 15, 150)]
        [InlineData(5000, -4950, 15, 0)]
        public void IR_Should_Return_IR_From_Params(decimal capitalInvestido, decimal capitalAtual, decimal taxaIR, decimal expected)
        {
            var fundos = new Fundos(
                capitalInvestido,
                capitalAtual,
                DateTime.Now,
                DateTime.Now,
                0,
                "Test",
                0,
                1
            );

            var ir = fundos.IR(taxaIR);

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
            var fundos = new Fundos(
               5000,
               valorTotal,
               vencimento,
               dataInvestimento,
               0,
               "Test",
               0,
               1
           );

            var valorResgate = fundos.ValorResgate;

            Assert.Equal(expected, valorResgate);
        }
    }
}