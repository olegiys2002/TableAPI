using Core.DTOs;
using FluentValidation;

namespace BookingTablesAPI.Validation.Avatar
{
    public class AvatarFormValidator : AbstractValidator<AvatarFormDTO>
    {
        public AvatarFormValidator()
        {
            RuleFor(avatar => avatar.Image).NotNull().WithMessage("image is a required field");
        }
    }
}
