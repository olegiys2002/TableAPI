using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;

namespace BookingTablesAPI.ServiceExtensions
{
    public static class DatabaseExtensions
    {
        public static void ConfigureDatabase(this IServiceCollection services,IConfiguration configuration)
        {
            var connectionString = Environment.GetEnvironmentVariable("ConnectionString");
            //string connectionString = configuration.GetConnectionString("sqlConnection");

            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString,
                                                      builder => builder.MigrationsAssembly("BookingTables.API")));
         
            //services.AddDbContext<ApplicationContext>(options =>
            //{
            //    var connectionString = Environment.GetEnvironmentVariable("ConnectionString");
            //    options.UseSqlServer(connectionString, builder => builder.MigrationsAssembly("BookingTablesAPI"));
            //});
        }

        public static WebApplication MigrateDatabase(this WebApplication webHost) 
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var db = services.GetRequiredService<ApplicationContext>();
                    db.Database.Migrate();
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
