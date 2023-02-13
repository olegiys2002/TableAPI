using BookingTables.Shared.RequestModels;
using FluentValidation;

namespace BookingTables.API.Validation.Request
{
    public class TableRequestValidation : AbstractValidator<TableRequest>
    {
        public TableRequestValidation()
        {
            RuleFor(r => r.PageSize).GreaterThan(0).WithMessage("page size cant be lower than 1");
            RuleFor(r => r.PageNumber).GreaterThan(0).WithMessage("page number cant be lower than 1");
            RuleFor(r => r.MaxCountOfSeats).ExclusiveBetween(0, 100).WithMessage("max count of seats can t be greater than 100");
            RuleFor(r => r.MaxCountOfSeats).ExclusiveBetween(0, 100).WithMessage("min count of seats can t be greater than 100");
        }
    }
}
