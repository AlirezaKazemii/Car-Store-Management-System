using Application.Commands.Orders;
using FluentValidation;

namespace Application.Fluent_Validation.Order
{
    public class UploadReceiptCommandValidator : AbstractValidator<UploadReceiptCommand>
    {
        public UploadReceiptCommandValidator()
        {
            RuleFor(x => x.OrderId).NotEmpty().WithMessage("OrderId is required.");
        }
    }
}
