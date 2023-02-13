using Core.DTOs;
using FluentValidation;

namespace BookingTablesAPI.Validation.Table
{
    public class TableFormValidator : AbstractValidator<TableFormDTO>
    {
        public TableFormValidator()
        {
            RuleFor(table => table.CountOfSeats).ExclusiveBetween(0, 40).WithMessage("count of seats can t be low than 0 and higher than 40");
            RuleFor(table => table.Number).ExclusiveBetween(0, int.MaxValue).WithMessage("number of table can t be lower than 0 ");
            RuleFor(table => table.Cost).GreaterThan(0).WithMessage("Cost can't be lower than 0");
        }

    }
}
