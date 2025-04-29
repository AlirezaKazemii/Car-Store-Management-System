using Application.Commands.Notification;
using FluentValidation;

namespace Application.Fluent_Validation.Notification
{
    public class CreateNotificationCommanValidator : AbstractValidator<CreateNotificationCommand>
    {
        public CreateNotificationCommanValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");

            RuleFor(x => x.Message).NotEmpty().WithMessage("Message is required.").MinimumLength(5)
                .WithMessage("Message must be at least 5 characters.");
        }
    }
}
