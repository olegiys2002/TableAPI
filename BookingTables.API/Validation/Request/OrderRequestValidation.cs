using BookingTables.Shared.RequestModels;
using FluentValidation;

namespace BookingTables.API.Validation.Request
{
    public class OrderRequestValidation : AbstractValidator<OrderRequest>
    {
        public OrderRequestValidation()
        {
            RuleFor(request => request.PageSize).GreaterThan(0).WithMessage("page size cant be lower than 1");
            RuleFor(request => request.PageNumber).GreaterThan(0).WithMessage("page number cant be lower than 1");
        }
    }
}
