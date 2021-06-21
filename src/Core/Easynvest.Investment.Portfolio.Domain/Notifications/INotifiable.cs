using System.Collections.Generic;

namespace Easynvest.Investment.Portfolio.Domain.Notifications
{
    public interface INotifiable
    {
        bool HasNotifications { get; }
        IReadOnlyCollection<Notification> Notifications { get; }
    }
}