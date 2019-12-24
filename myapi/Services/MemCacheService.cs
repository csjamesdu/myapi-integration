using Microsoft.Extensions.Caching.Memory;
using System;

namespace myapi.Services
{
    public class MemCacheService : IMemCacheService
    {
        private readonly IMemoryCache _memoryCache;

        public MemCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public bool CacheHit(string cacheKey)
        {
            return _memoryCache.TryGetValue(cacheKey, out string result);
        }

        public string GetValue(string cacheKey)
        {
            return _memoryCache.Get<string>(cacheKey);
        }

        public void SetValueWithExpire(string jsonStr, string cacheKey, TimeSpan timeSpan)
        {
            _memoryCache.Set(cacheKey, jsonStr, timeSpan);
        }
    }
}
