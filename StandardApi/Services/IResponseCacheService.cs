using System;
using System.Threading.Tasks;

namespace StandardApi.Services
{
    public interface IResponseCacheService
    {
        Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLeave);

        Task<string> GetCachedResponseAsync(string cacheKey);
    }
}
