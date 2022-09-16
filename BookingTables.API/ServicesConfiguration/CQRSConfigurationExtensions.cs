namespace BookingTablesAPI.ServicesConfiguration
{
    public static class CQRSConfigurationExtensions
    {
        public static void ConfigureCQRS (this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });

        }
    }
}
