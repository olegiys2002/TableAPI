using BookingTablesAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BookingTables.API.ServicesConfiguration
{
    public static class VersioningAPIExtensions
    {
        public static void ConfigureVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(opt =>
            {
                opt.ReportApiVersions = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.Conventions.Controller<TableController>().HasApiVersion(new ApiVersion(1,0));
            });
            
        }
    }
}
