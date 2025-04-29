using Application.Commands.User;
using FluentValidation;

namespace Application.Fluent_Validation.User
{
    public class SoftDeleteCommandValidator : AbstractValidator<SoftDeleteCommand>
    {
        public SoftDeleteCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
        }
    }
}
