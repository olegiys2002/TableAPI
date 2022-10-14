using IdentityServer4.AccessTokenValidation;

namespace BookingTablesAPI.ServicesConfiguration
{
    public static class AuthenticationExtension
    {
        public static void ConfigureAuthentication(this IServiceCollection services,IConfiguration configuration)
        {

            //services.AddAuthentication("Bearer")
            //         .AddJwtBearer("Bearer", options =>
            //         {
            //             options.Authority = "https://localhost:5090";
            //             options.RequireHttpsMetadata = false;
            //             options.Audience = "tablesAPI";

            //           

            //         });

            services.AddAuthentication("Bearer")
                    .AddJwtBearer("Bearer", options =>
                     {
                         options.Authority = "https://identity:5090";
                         options.RequireHttpsMetadata = false;
                         options.Audience = "tablesAPI";
                         options.MetadataAddress = "https://identity:5090/.well-known/openid-configuration";

                     });


        }
    }
}
