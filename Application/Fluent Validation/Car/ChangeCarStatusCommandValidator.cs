using Application.Commands.Car;
using FluentValidation;

namespace Application.Fluent_Validation.Car
{
    public class ChangeCarStatusCommandValidator : AbstractValidator<ChangeCarStatusCommand>
    {
        public ChangeCarStatusCommandValidator()
        {
            RuleFor(x => x.CarId).NotEmpty().WithMessage("CarId is required.");
        }
    }
}
