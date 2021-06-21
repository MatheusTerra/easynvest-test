using System.Collections.Generic;
using System.Threading.Tasks;
using Easynvest.Investment.Portfolio.Domain.Entities;
using Easynvest.Investment.Portfolio.Domain.Notifications;

namespace Easynvest.Investment.Portfolio.Domain.Queries.Repositories
{
    public interface IFundosQueryRepository : INotifiable
    {
         Task<IEnumerable<Fundos>> Get();
    }
}