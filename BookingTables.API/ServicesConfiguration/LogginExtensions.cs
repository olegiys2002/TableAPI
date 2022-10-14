using Serilog;

namespace BookingTables.API.ServicesConfiguration
{
    public static class LogginExtensions
    {
        public static void ConfigureSerilog(this ILoggingBuilder logging,IConfiguration configuration)
        {
            var logger = new LoggerConfiguration()
                              .ReadFrom.Configuration(configuration)
                              .Enrich.FromLogContext()
                              .CreateLogger();
            logging.ClearProviders();
            logging.AddSerilog(logger);
            
        }
    }
}
