using Application.Commands.User;
using FluentValidation;

namespace Application.Fluent_Validation.User
{
    public class OtpCodeCommandValidator : AbstractValidator<OtpCodeCommand>
    {
        public OtpCodeCommandValidator()
        {
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("PhoneNumber is required.");
        }
    }
}
