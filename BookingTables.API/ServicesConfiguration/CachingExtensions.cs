using Core.IServices;
using Core.Services;
using Models.Models;

namespace BookingTablesAPI.ServicesConfiguration
{
    public static class CachingExtensions
    {
        public static void ConfigureCaching(this IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddMemoryCache();
            services.AddSingleton<ICacheService<List<Table>>,CacheService<List<Table>>>();
            services.AddSingleton<ICacheService<Order>, CacheService<Order>>();
        }
    }
}
