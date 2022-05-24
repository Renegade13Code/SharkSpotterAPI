using FluentValidation;
using SharkSpotterAPI.Models.DTO;

namespace SharkSpotterAPI.Validators
{
    public class AddBeachRequestValidator : AbstractValidator<AddBeachRequest>
    {
        public AddBeachRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(255);
            RuleFor(x => x.Geolocation).NotEmpty().MaximumLength(255);
            RuleFor(x => x.Latitude).InclusiveBetween(-90, 90);
            RuleFor(x => x.Longitude).InclusiveBetween(-180, 180);
        }
    }
}
