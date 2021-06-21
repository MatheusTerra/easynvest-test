using System;
using System.Threading.Tasks;
using Easynvest.Investment.Portfolio.Infra.Cache.Clients.Abstracts;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Easynvest.Investment.Portfolio.Infra.Cache.Clients
{
    public class CacheClient : ICacheClient
    {
        private readonly IDistributedCache _cache;

        public CacheClient(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<string> GetValueAsync(string key)
        {
            return await _cache.GetStringAsync(key);
        }

        public async Task<T> GetValueAsync<T>(string key)
        {
            var value = await GetValueAsync(key);

            if (string.IsNullOrEmpty(value))
                return default(T);

            return JsonConvert.DeserializeObject<T>(value);
        }

        public async Task WriteValueAsync(string key, object value, TimeSpan duration)
        {
            var json = JsonConvert.SerializeObject(value);

            var options = new DistributedCacheEntryOptions
            {
                SlidingExpiration = duration
            };

            await _cache.SetStringAsync(key, json, options);
        }
    }
}