using Application.Commands.User;
using FluentValidation;

namespace Application.Fluent_Validation.User
{
    public class GetUserByIdCommandValidator : AbstractValidator<GetUserByIdCommand>
    {
        public GetUserByIdCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserID is required.");
        }
    }
}
