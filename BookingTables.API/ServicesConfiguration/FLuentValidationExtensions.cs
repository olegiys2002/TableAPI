using BookingTablesAPI.Validation.Order;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace BookingTablesAPI.ServicesConfiguration
{
    public static class FLuentValidationExtensions
    {
        public static void ConfigureFluentValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<OrderDTOValidation>();
            services.AddFluentValidationAutoValidation();
        }
    }
}
