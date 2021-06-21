using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Easynvest.Investment.Portfolio.Domain.Entities;
using Easynvest.Investment.Portfolio.Domain.Notifications;
using Easynvest.Investment.Portfolio.Domain.Queries.Repositories;
using Easynvest.Investment.Portfolio.Infra.Cache.Clients.Abstracts;
using Easynvest.Investment.Portfolio.Infra.Config;
using Easynvest.Investment.Portfolio.Infra.DTO;
using Easynvest.Investment.Portfolio.Infra.Http.Clients.Abstract;
using Easynvest.Investment.Portfolio.Infra.Mappers;
using Easynvest.Investment.Portfolio.Infra.Repositories.Queries.Abstracts;

namespace Easynvest.Investment.Portfolio.Infra.Repositories.Queries
{
    public class RendaFixaQueryRepository : QueryRepositoryBase, IRendaFixaQueryRepository
    {
        private readonly IRestClient _client;
        private readonly ServicesConfig _config;
        private readonly NotificationPool _notificationPool;

        public RendaFixaQueryRepository(
            IRestClient client,
            ServicesConfig config,
            NotificationPool notificationPool,
            ICacheClient cacheClient) : base(cacheClient)
        {
            _client = client;
            _config = config;
            _notificationPool = notificationPool;
        }

        public bool HasNotifications => _notificationPool.HasNotifications;
        public IReadOnlyCollection<Notification> Notifications => _notificationPool.Notifications;

        public async Task<IEnumerable<RendaFixa>> Get()
        {
            string cacheKey = "rendaFixa_key";

            var cacheResponse = await base.GetFromCache<IEnumerable<RendaFixa>>(cacheKey);
            if (cacheResponse != null) return cacheResponse;

            var route = _config.RendaFixaRoute;

            var response = await _client.Get<RendaFixaDTO>(route, _config.Timeout);
            if (_client.HasNotifications) return null;

            var entity = response.Map();

            if (entity.Any())
                base.SetCache(cacheKey, entity);

            return entity;
        }
    }
}