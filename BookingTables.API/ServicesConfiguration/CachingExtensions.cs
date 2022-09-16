using Core.IServices;
using Core.Models;
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
            services.AddScoped<ICacheService<List<Table>>,CacheService<List<Table>>>();
            services.AddScoped<ICacheService<List<User>>, CacheService<List<User>>>();
            services.AddScoped<ICacheService<Order>, CacheService<Order>>();
            services.AddScoped<ICacheService<User>,CacheService<User>>();
        }
    }
}
