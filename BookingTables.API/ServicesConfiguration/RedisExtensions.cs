using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace BookingTablesAPI.ServicesConfiguration
{
    public static class RedisExtensions
    {
        public static void ConfigureRedis(this IServiceCollection serviceCollection,IConfiguration configuration)
        {
            var redisConfig = configuration.GetSection("redis").Value;
            serviceCollection.AddDistributedRedisCache(
                options =>
                {
                    options.Configuration = redisConfig;
                });
           
        }
    }
}
