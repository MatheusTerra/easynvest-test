using System.Collections.Generic;
using System.Threading.Tasks;
using Easynvest.Investment.Portfolio.Domain.Entities;
using Easynvest.Investment.Portfolio.Domain.Notifications;
using Easynvest.Investment.Portfolio.Domain.Queries.Abstracts;
using Easynvest.Investment.Portfolio.Domain.Services.Abstract;

namespace Easynvest.Investment.Portfolio.Domain.Queries
{
    public class InvestmentQuery : IInvestmentQuery
    {
        private readonly IInvestmentService _investmentService;
        private readonly NotificationPool _notificationPool;

        public InvestmentQuery(
            IInvestmentService investmentService,
            NotificationPool notificationPool)
        {
            _investmentService = investmentService;
            _notificationPool = notificationPool;
        }

        public bool HasNotifications => _notificationPool.HasNotifications;
        public IReadOnlyCollection<Notification> Notifications => _notificationPool.Notifications;

        public async Task<IEnumerable<Investimento>> GetInvestments()
        {
            var investments = await _investmentService.GetInvestments();

            if (_investmentService.HasNotifications)
                return null;

            return investments;
        }
    }
}