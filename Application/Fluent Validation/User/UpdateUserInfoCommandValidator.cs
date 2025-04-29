using Application.Commands.User;
using FluentValidation;

namespace Application.Fluent_Validation.User
{
    public class UpdateUserInfoCommandValidator : AbstractValidator<UpdateUserInfoCommand>
    {
        public UpdateUserInfoCommandValidator()
        {
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("phoneNumber is required.");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.").EmailAddress().WithMessage("The email format is not correct.");
        }
    }
}
