using System.Collections.Generic;
using System.Threading.Tasks;
using Easynvest.Investment.Portfolio.Domain.Entities;
using Easynvest.Investment.Portfolio.Domain.Notifications;

namespace Easynvest.Investment.Portfolio.Domain.Services.Abstract
{
    public interface IInvestmentService : INotifiable
    {
         Task<IEnumerable<Investimento>> GetInvestments();
    }
}