using Core.IServices;
using Core.Models.JWT;
using Core.Services;
using Infrastructure;
using MediatR;

namespace BookingTablesAPI.ServicesConfiguration
{
    public static class ApplicationExtensions
    {
        public static void ConfigureAppServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<ITableService, TableService>();
            serviceCollection.AddScoped<IOrderService, OrderService>();
            serviceCollection.AddMediatR(AppDomain.CurrentDomain.Load("BookingTables.Core"));
            serviceCollection.AddHttpContextAccessor();
            serviceCollection.AddSignalR();
            serviceCollection.AddRouting(opt => opt.LowercaseUrls = true);    
        }
    }
}
