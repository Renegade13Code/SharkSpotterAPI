using FluentValidation;
using SharkSpotterAPI.Models.DTO;

namespace SharkSpotterAPI.Validators
{
    public class AddUserRequestValidator : AbstractValidator<AddUserRequest>
    {
        public AddUserRequestValidator()
        {
            RuleFor(x => x.Username).NotEmpty().MaximumLength(128).MinimumLength(6);
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MaximumLength(128).MinimumLength(6);
            RuleFor(x => x.Firstname).NotEmpty().MaximumLength(128);
            RuleFor(x => x.Lastname).NotEmpty().MaximumLength(128);
        }
    }
}
