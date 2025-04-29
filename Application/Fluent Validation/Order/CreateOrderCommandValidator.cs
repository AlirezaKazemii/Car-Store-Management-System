using Application.Commands.Orders;
using FluentValidation;

namespace Application.Fluent_Validation.Order
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.CarId).NotEmpty().WithMessage("CarId is required.");
        }
    }
}

