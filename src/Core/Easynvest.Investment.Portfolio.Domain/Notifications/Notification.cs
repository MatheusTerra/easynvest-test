namespace Easynvest.Investment.Portfolio.Domain.Notifications
{
    public class Notification
    {
        public NotificationLevel Level { get; set; }
        public string Description { get; set; } 
        public string Label { get; set; }
        public string Message { get; set; }

        public Notification(NotificationLevel level, string description)
        {
            Level = level;
            Description = Message = description;
        }

        public Notification(NotificationLevel level, string message, string label)
        {
            Level = level;
            Description = Message = message;
            Label = label;
        }
    }

    public enum NotificationLevel
    {
        None,
        Validation,
        Error,
        Forbiden
    }
}