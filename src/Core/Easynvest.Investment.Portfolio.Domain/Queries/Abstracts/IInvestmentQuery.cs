using System.Collections.Generic;
using System.Threading.Tasks;
using Easynvest.Investment.Portfolio.Domain.Entities;
using Easynvest.Investment.Portfolio.Domain.Notifications;

namespace Easynvest.Investment.Portfolio.Domain.Queries.Abstracts
{
    public interface IInvestmentQuery : INotifiable
    {
         Task<IEnumerable<Investimento>> GetInvestments();
    }
}