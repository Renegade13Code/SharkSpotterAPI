using FluentValidation;
using SharkSpotterAPI.Models.DTO;

namespace SharkSpotterAPI.Validators
{
    public class UpdateFlagRequestValidator : AbstractValidator<UpdateFlagRequest>
    {
        public UpdateFlagRequestValidator()
        {
            RuleFor(x => x.Color).NotEmpty().MaximumLength(255);
        }
    }
}
