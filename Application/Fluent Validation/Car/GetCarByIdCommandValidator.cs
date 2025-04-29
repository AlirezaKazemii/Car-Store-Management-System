using Application.Commands.Car;
using FluentValidation;

namespace Application.Fluent_Validation.Car
{
    public class GetCarByIdCommandValidator : AbstractValidator<GetCarByIdCommand>
    {
        public GetCarByIdCommandValidator()
        {
            RuleFor(x => x.CarId).NotEmpty().WithMessage("CarId is required.");
        }
    }
}
