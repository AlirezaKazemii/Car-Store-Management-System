using Application.Commands.Customer;
using FluentValidation;
using System.Data;

namespace Application.Fluent_Validation.Customer
{
    public class RegisterOrUpdateCustomerCommandValidator : AbstractValidator<RegisterOrUpdateCustomerCommand>
    {
        public RegisterOrUpdateCustomerCommandValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("FullName is required.").MinimumLength(3).WithMessage("FullName must be at least 3 characters.");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.").EmailAddress().WithMessage("The email format is not correct.");

            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("PhoneNumber is required.");

            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required.").MinimumLength(3).WithMessage("Address must be at least 3 characters.");
        }
    }
}
