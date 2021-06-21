using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Easynvest.Investment.Portfolio.Domain.Notifications;
using Easynvest.Investment.Portfolio.Infra.Http.Clients.Abstract;
using Newtonsoft.Json;

namespace Easynvest.Investment.Portfolio.Infra.Http.Clients
{
    public class RestClient : IRestClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private NotificationPool _notificationPool;

        public RestClient(IHttpClientFactory httpClientFactory, NotificationPool notificationPool)
        {
            _httpClientFactory = httpClientFactory;
            _notificationPool = notificationPool;
        }

        public bool HasNotifications => _notificationPool.HasNotifications;
        public IReadOnlyCollection<Notification> Notifications => _notificationPool.Notifications;

        public async Task<T> Get<T>(string route, int timeout = 10000)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, route);

                var client = _httpClientFactory.CreateClient();
                client.Timeout = TimeSpan.FromMilliseconds(timeout);

                using (var httpResponse = await client.SendAsync(request))
                {
                    var response = await httpResponse.Content.ReadAsStringAsync();

                    switch (httpResponse.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            return JsonConvert.DeserializeObject<T>(response);
                        case HttpStatusCode.NotFound:
                            return default(T);
                        case HttpStatusCode.BadRequest:
                            _notificationPool.Add(response, NotificationLevel.Validation);
                            return default(T);
                        case HttpStatusCode.Unauthorized:
                            _notificationPool.AddForbiddenNotification();
                            return default(T);
                        default:
                            string msg = !string.IsNullOrEmpty(response) ? response : $"Erro inesperado na chamada da api: {route}";
                            _notificationPool.Add(msg, NotificationLevel.Error);
                            return default(T);
                    }
                }
            }
            catch (Exception ex)
            {
                _notificationPool.Add(ex.Message, NotificationLevel.Error);
                return default(T);
            }
        }

        public async Task<bool> Get(string route, int timeout = 10000)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, route);

                var client = _httpClientFactory.CreateClient();
                client.Timeout = TimeSpan.FromMilliseconds(timeout);

                using (var httpResponse = await client.SendAsync(request))
                    return httpResponse.StatusCode == HttpStatusCode.OK;
            }
            catch
            {
                return false;
            }
        }
    }
}
