using Application.Commands.Car;
using FluentValidation;
using FluentValidation.Validators;

namespace Application.Fluent_Validation.Car
{
    public class CarUpdateCommandValidator : AbstractValidator<CarUpdateCommand>
    {
        public CarUpdateCommandValidator()
        {

            RuleFor(x => x.CarId).NotEmpty().WithMessage("CarId is required.");

            RuleFor(x => x.Brand).NotEmpty().WithMessage("Brand is required.");

            RuleFor(x => x.Model).NotEmpty().WithMessage("Model is required.");

            RuleFor(x => x.Year).NotEmpty().WithMessage("Year is required.").GreaterThan(0).WithMessage("Year must be greater than zero.");

            RuleFor(x => x.Color).NotEmpty().WithMessage("Color is required.");

            RuleFor(x => x.Price).NotEmpty().WithMessage("Price is required.").GreaterThan(0).WithMessage("Year must be greater than zero.");

            
        }
    }
}
