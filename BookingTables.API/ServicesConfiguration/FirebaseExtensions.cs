using Core.IServices;
using Core.Services;
using Firebase.Auth;
using Firebase.Storage;

namespace BookingTablesAPI.ServicesConfiguration
{
    public static class FirebaseExtensions
    {
     
      public static void ConfigureFirebaseDatabase(this IServiceCollection services,IConfiguration configuration)
      {
            var fireBaseConfig = configuration.GetSection("FirebaseSettings");
            var authSecret = fireBaseConfig.GetSection("AuthSecret").Value;
            var basePath = fireBaseConfig.GetSection("BasePath").Value;
            //services.AddScoped<IFirebaseConfig, FireSharp.Config.FirebaseConfig>();
      }
        public static void ConfigureStorage(this IServiceCollection services)
        {
            services.AddScoped<IStorage, FireStorage>();
        }

    }
}
