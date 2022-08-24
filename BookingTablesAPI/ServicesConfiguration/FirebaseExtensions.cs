﻿using Core.IServices;
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
      private static string apiKey = "AIzaSyBq-_ZZkpv45Myw3NDuwajH5QX1aF6pr7k";
      private static string Bucket = "tableapi-882ce.appspot.com";
      private static string AuthEmail = "test@gmail.com";
      private static string AuthPassword = "123456";
      public static void ConfigureFirebaseDatabase(this IServiceCollection services)
      {
            services.AddScoped<IFirebaseConfig, FireSharp.Config.FirebaseConfig>();

            IFirebaseConfig firebaseConfig = new FireSharp.Config.FirebaseConfig()
            {
                AuthSecret = "nokbEXFT29xfzDBmiKf9GMQ4Xxhho7n9Clu0CkEl",
                BasePath = "https://tableapi-882ce-default-rtdb.firebaseio.com/"
            };
            services.AddScoped<IFirebaseClient>(opt => new FirebaseClient(firebaseConfig));
      }
        public static void ConfigureStorage(this IServiceCollection services)
        {
            services.AddScoped<IStorage, FireStorage>();
        }

    }
}
