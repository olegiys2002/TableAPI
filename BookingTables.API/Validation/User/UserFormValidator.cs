using Core.DTOs;
using FluentValidation;

namespace BookingTablesAPI.Validation.User
{
    public class UserFormValidator : AbstractValidator<UserFormDTO>
    {
        public UserFormValidator(IValidator<AvatarFormDTO> avatarFormDTOsValidator)
        {
            RuleFor(user => user.Name).Length(2, 10).WithMessage("Name cant be less than 2 or more than 10 symbols");
            RuleFor(user => user.Password).Length(2,20).WithMessage("Password cant be less than 2");
            RuleFor(user => user.Role).NotEmpty().WithMessage("Role cannot be empty");
            RuleFor(user => user.Email).NotEmpty().WithMessage("Email is a required field");
            RuleFor(user => user.AvatarFormDTO).SetValidator(avatarFormDTOsValidator);
           
        }

    }
}
