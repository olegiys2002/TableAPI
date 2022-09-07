﻿using Core.IServices;
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
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<IOrderService, OrderService>();
            serviceCollection.AddScoped<ITokenService, TokenService>();
            serviceCollection.AddScoped<IAuthenticationManager, AuthenticationManager>();
            serviceCollection.AddScoped<IRabbitMqService, RabbitMqProducer>();
            serviceCollection.AddScoped<IJWTService, JWTService>();
            serviceCollection.AddMediatR(AppDomain.CurrentDomain.Load("Core"));
            serviceCollection.AddHttpContextAccessor();
            serviceCollection.AddSignalR();

        }
    }
}