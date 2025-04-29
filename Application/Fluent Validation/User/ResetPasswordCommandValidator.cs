using Application.Commands.User;
using FluentValidation;

namespace Application.Fluent_Validation.User
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("CurruntPassword is required.");
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("NewPassword is required");
        }
    }
}
