using Core.IServices;
using Core.Models.JWT;
using Core.Services;
using Infrastructure;

namespace BookingTablesAPI.ServicesConfiguration
{
    public static class ApplicationExtensions
    {
        public static void ConfigureAppServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<ITableService, TableService>();
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<IOrderService, OrderService>();
            serviceCollection.AddScoped<ITokenService, TokenService>();
            serviceCollection.AddScoped<IAuthenticationManager, AuthenticationManager>();
        
        }
    }
}
