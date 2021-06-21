using System;
using System.Threading.Tasks;
using Easynvest.Investment.Portfolio.Infra.Cache.Clients.Abstracts;

namespace Easynvest.Investment.Portfolio.Infra.Repositories.Queries.Abstracts
{
    public class QueryRepositoryBase
    {
        private readonly ICacheClient _cacheClient;

        public QueryRepositoryBase(ICacheClient cacheClient)
        {
            _cacheClient = cacheClient;
        }

        protected async Task<T> GetFromCache<T>(string cacheKey)
        {
            return await _cacheClient.GetValueAsync<T>(cacheKey);
        }

        protected async Task SetCache<T>(string cacheKey, T subject)
        {
            var duration = DateTime.Today.AddDays(1).Subtract(DateTime.Now.Add(TimeSpan.FromMilliseconds(1))); //expira meia noite

            await _cacheClient.WriteValueAsync(cacheKey, subject, duration);
        }
    }
}