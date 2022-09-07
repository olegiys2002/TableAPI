using Core.DTOs;
using FluentValidation;

namespace BookingTablesAPI.Validation.Order
{
    public class OrderDTOValidation : AbstractValidator<OrderFormDTO>
    {
        public OrderDTOValidation()
        {
            RuleFor(order => order.CountOfPeople).NotNull()
                                                 .ExclusiveBetween(0, 100)
                                                 .WithMessage("Required,count of people cant be more than 100");
            RuleFor(order => order.TablesId).NotNull();
        }
    }
}
