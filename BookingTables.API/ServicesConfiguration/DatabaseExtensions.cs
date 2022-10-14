using Infrastructure;
using Microsoft.EntityFrameworkCore;


namespace BookingTablesAPI.ServiceExtensions
{
    public static class DatabaseExtensions
    {
        public static void ConfigureDatabase(this IServiceCollection services,IConfiguration configuration)
        {
            //var connectionString = Environment.GetEnvironmentVariable("ConnectionString");
            string connectionString = configuration.GetConnectionString("sqlConnection");
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString,
                                                      builder => builder.MigrationsAssembly("BookingTables.API")));
            services.AddHealthChecks().AddSqlServer(connectionString);
        }

        public static WebApplication MigrateDatabase(this WebApplication webHost) 
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var db = services.GetRequiredService<ApplicationContext>();

                    if (db.Database.GetPendingMigrations().Count() > 0)
                    {
                        db.Database.Migrate();
                    }

                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }
            return webHost;
        }

    }
}
