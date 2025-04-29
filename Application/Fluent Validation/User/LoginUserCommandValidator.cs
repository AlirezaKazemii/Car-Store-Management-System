using Application.Commands.User;
using FluentValidation;

namespace Application.Fluent_Validation.User
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName is required")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required");        
        }
    }
}
