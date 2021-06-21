using System.Linq;
using System.Net;
using Easynvest.Investment.Portfolio.Domain.Notifications;

namespace Easynvest.Investment.Portfolio.API.Models.Handlers
{
    public static class ErrorHandler
    {
        public static ErrorModel CreateError(string message, int code)
        {
            return new ErrorModel(code, message, string.Empty);
        }

        public static ErrorModel CreateError(INotifiable subject)
        {
            if (subject.Notifications.Any(s => s.Level == NotificationLevel.Error))
                return CreateError((int)HttpStatusCode.InternalServerError, "Erro inesperado", subject);

            return CreateError((int)HttpStatusCode.BadRequest, "Requisição inválida", subject);
        }

        public static ErrorModel CreateError(string message, INotifiable subject)
        {
            var code = GetErrorCode(subject);
            var error = new ErrorModel(code, message, string.Empty);

            foreach (var notification in subject.Notifications)
                error.AddItem(notification.Label ?? notification.Level.ToString(), notification.Message);

            return error;
        }

        private static ErrorModel CreateError(int code, string message, INotifiable subject)
        {
            if (subject.Notifications.Count == 1)
                return new ErrorModel(code, message, subject.Notifications.First().Description);

            var notifications = subject.Notifications.Select(s => new ErrorItemModel(s.Level.ToString(), s.Description)).ToList();

            return new ErrorModel(code, message, null, notifications);
        }

        private static int GetErrorCode(INotifiable subject)
        {
            var code = (int)HttpStatusCode.BadRequest;

            if (subject.Notifications.Any(s => s.Level == NotificationLevel.Error))
                code = (int)HttpStatusCode.InternalServerError;

            if (subject.Notifications.Any(s => s.Level == NotificationLevel.Forbiden))
                code = (int)HttpStatusCode.Forbidden;

            if (subject.Notifications.Any(s => s.Level == NotificationLevel.Validation))
                code = (int)HttpStatusCode.BadRequest;

            return code;
        }
    }
}