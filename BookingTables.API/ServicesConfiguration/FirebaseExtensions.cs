using Core.IServices;
using Core.Services;
using Firebase.Auth;
using Firebase.Storage;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;

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

            IFirebaseConfig firebaseConfig = new FireSharp.Config.FirebaseConfig()
            {
                AuthSecret = authSecret,
                BasePath = basePath
            };
            services.AddScoped<IFirebaseClient>(opt => new FirebaseClient(firebaseConfig));
      }
        public static void ConfigureStorage(this IServiceCollection services)
        {
            services.AddScoped<IStorage, FireStorage>();
        }

    }
}
