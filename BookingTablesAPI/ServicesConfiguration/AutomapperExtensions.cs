using Core.Services;

namespace BookingTablesAPI.ServicesConfiguration
{
    public static class AutomapperExtensions
    {
        public static void ConfigureAutomapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile));
        }
    }
}
