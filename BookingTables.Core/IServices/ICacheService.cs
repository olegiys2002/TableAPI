

namespace Core.IServices
{
    public interface ICacheService<T>
    {
        //void CacheItems(string key, T items);
        Task CacheItems(string key, T items);
        Task<T> TryGetCache(string key);
        Task RemoveCache(string key);
    }
}
