using Application.Commands.User;
using FluentValidation;

namespace Application.Fluent_Validation.User
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required.")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("The email format is not correct.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("PhoneNumber is required");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role is required");
        }
    }
}
