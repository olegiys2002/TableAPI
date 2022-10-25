using MassTransit;


namespace BookingTables.API.ServicesConfiguration
{
    public static class MassTransitExtensions
    {
        public static void ConfigureMassTransit(this IServiceCollection services,IConfiguration configuration)
        {
            var rabbitMqHost = configuration["RabbitMq:hostname"];
            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(rabbitMqHost);
                });
            });
        }

    }
}
