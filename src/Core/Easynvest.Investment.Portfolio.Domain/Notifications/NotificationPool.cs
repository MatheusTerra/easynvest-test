using System.Collections.Generic;
using System.Linq;

namespace Easynvest.Investment.Portfolio.Domain.Notifications
{
    public class NotificationPool
    {
        private List<Notification> _notifications;
        public IReadOnlyCollection<Notification> Notifications => _notifications;
        public bool HasNotifications => Notifications.Any();

        public NotificationPool()
        {
            _notifications = new List<Notification>();
        }

        public void Add(NotificationLevel level, string description)
        {
            _notifications.Add(new Notification(level, description));
        }

        public void Add(IEnumerable<Notification> notifications)
        {
            _notifications.AddRange(notifications);
        }

        public void Add(string message, NotificationLevel level, string label = null)
        {
            _notifications.Add(new Notification(level, message, label));
        }

        public void AddForbiddenNotification()
        {
            _notifications.Add(new Notification(NotificationLevel.Forbiden, null));
        }

        public void Reset()
        {
            _notifications.Clear();
            _notifications = new List<Notification>();
        }
    }
}