using Core.IServices;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;


namespace Core.Services
{
    public class CacheService<T> : ICacheService<T>
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IDistributedCache _cache;
        public CacheService(IMemoryCache memoryCache, IDistributedCache cache)
        {
            _memoryCache = memoryCache;
            _cache = cache;
        }
        public DistributedCacheEntryOptions GetCacheOptions()
        {

            var cacheEntryOptions = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                        .SetSlidingExpiration(TimeSpan.FromSeconds(60));

            return cacheEntryOptions;
        }
        public async Task CacheItems(string key, T items)
        {
           
            var jsonItem = JsonSerializer.Serialize(items);

            var cacheEntryOptions = GetCacheOptions();
            await _cache.SetStringAsync(key, jsonItem, cacheEntryOptions);

        }
        public async Task<T> TryGetCache(string key)
        {
            var jsonItem = await _cache.GetStringAsync(key);

            if (jsonItem == null)
            {
                return default(T);
            }

            T items = JsonSerializer.Deserialize<T>(jsonItem);

            if (items == null)
            {
                return default(T);
            }
            
            return items;
        }
        public async Task RemoveCache(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }
}
