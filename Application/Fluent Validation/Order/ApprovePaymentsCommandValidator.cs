using Application.Commands.Orders;
using FluentValidation;

namespace Application.Fluent_Validation.Order
{
    public class ApprovePaymentsCommandValidator : AbstractValidator<ApprovePaymentsCommand>
    {
        public ApprovePaymentsCommandValidator()
        {
            RuleFor(x => x.OrderId).NotEmpty().WithMessage("OrderId is required.");
        }
    }
}
