using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BookingTablesAPI.ServiceExtensions
{
    public static class DatabaseExtensions
    {
        public static void ConfigureDatabase(this IServiceCollection services,IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("sqlConnection");
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString,
                                                      builder => builder.MigrationsAssembly("BookingTablesAPI")));
        }
    }
}
