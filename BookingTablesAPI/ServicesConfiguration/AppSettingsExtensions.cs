using Core.Models.JWT;

namespace BookingTablesAPI.ServicesConfiguration
{
    public static class AppSettingsExtensions
    {
        public static void ConfigureAppSettings(this IServiceCollection services,IConfiguration configuration)
        {
            services.Configure<JwtSettingsOptions>(configuration.GetSection(JwtSettingsOptions.JwtSettings));
        }
    }
}
