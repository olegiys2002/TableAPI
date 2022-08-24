using Core.Models.JWT;
using Core.Models.Storage;

namespace BookingTablesAPI.ServicesConfiguration
{
    public static class AppSettingsExtensions
    {
        public static void ConfigureAppSettings(this IServiceCollection services,IConfiguration configuration)
        {
            services.Configure<JwtSettingsOptions>(configuration.GetSection(JwtSettingsOptions.JwtSettings));
            services.Configure<FireStorageOptions>(configuration.GetSection(FireStorageOptions.FireStorageSettings));
            
        }
    }
}
