using System;

namespace myapi.Services
{
    public interface IMemCacheService
    {
        void SetValueWithExpire(string jsonStr, string cacheKey, TimeSpan timeSpan);
        string GetValue(string cacheKey);
        bool CacheHit(string cacheKey);
    }
}
