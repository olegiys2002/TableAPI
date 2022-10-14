using MassTransit;
using MassTransit.Transports;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BookingTables.API.ServicesConfiguration
{
    public static class MassTransitExtensions
    {
        public static void ConfigureMassTransit(this IServiceCollection services)
        {
            services.AddMassTransit(x => x.UsingRabbitMq());
        }

    }
}
