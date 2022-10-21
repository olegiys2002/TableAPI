using IdentityServer4.AccessTokenValidation;

namespace BookingTablesAPI.ServicesConfiguration
{
    public static class AuthenticationExtension
    {
        public static void ConfigureAuthentication(this IServiceCollection services,IConfiguration configuration)
        {

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                    .AddIdentityServerAuthentication(options =>
            {
                options.Authority = "http://identity";
                options.RequireHttpsMetadata = false;
                options.ApiName = "tablesAPI";
            });


        }
    }
}
