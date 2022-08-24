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
       
            string connectionString = configuration.GetConnectionString("sqlConnection");

            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString,
                                                      builder => builder.MigrationsAssembly("BookingTablesAPI")));
            //services.AddDbContext<ApplicationContext>(options =>
            //{
            //    /*var server = Environment.GetEnvironmentVariable("ServerName");
            //    var database = Environment.GetEnvironmentVariable("Database");
            //    var userName = Environment.GetEnvironmentVariable("UserName");
            //    var password = Environment.GetEnvironmentVariable("Password");*/
            //    var connectionString = Environment.GetEnvironmentVariable("ConnectionString");
            //    options.UseSqlServer(connectionString, builder=>builder.MigrationsAssembly("BookingTablesAPI"));
            //});
        }
    }
}
