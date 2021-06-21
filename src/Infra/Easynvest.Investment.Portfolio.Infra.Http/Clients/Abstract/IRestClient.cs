using System.Threading.Tasks;
using Easynvest.Investment.Portfolio.Domain.Notifications;

namespace Easynvest.Investment.Portfolio.Infra.Http.Clients.Abstract
{
    public interface IRestClient : INotifiable
    {
        Task<T> Get<T>(string route, int timeout = 10000);
        Task<bool> Get(string route, int timeout = 10000);
    }
}