using FluentValidation;
using SharkSpotterAPI.Models.DTO;

namespace SharkSpotterAPI.Validators
{
    public class AddFlagRequestValidator : AbstractValidator<AddFlagRequest>
    {
        public AddFlagRequestValidator()
        {
            RuleFor(x => x.Color).NotEmpty().MaximumLength(255);
        }
    }
}
