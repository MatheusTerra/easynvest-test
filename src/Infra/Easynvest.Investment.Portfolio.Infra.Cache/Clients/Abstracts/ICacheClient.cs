using System;
using System.Threading.Tasks;

namespace Easynvest.Investment.Portfolio.Infra.Cache.Clients.Abstracts
{
    public interface ICacheClient
    {
         Task<string> GetValueAsync(string key);
         Task<T> GetValueAsync<T>(string key);
         Task WriteValueAsync(string key, object value, TimeSpan duration);
    }
}