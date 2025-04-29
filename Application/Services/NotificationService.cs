using Application.Commands.Car;
using Application.Commands.Notification;
using Application.Interfaces;
using Domain.IRepostories;
using Domain.Models;
using FluentValidation;
using static Domain.Models.Notification;

namespace Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IValidator<CreateNotificationCommand> _createNotificationValidator;

        public NotificationService(INotificationRepository notificationRepository,
            IValidator<CreateNotificationCommand> CreateNotificationValidator)
        {
            _notificationRepository = notificationRepository;
            _createNotificationValidator = CreateNotificationValidator;
        }


        public async Task CreateNotification(CreateNotificationCommand command)
        {

            var validationResult = await _createNotificationValidator.ValidateAsync(command);

            if(!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var notification = new Notification(command.UserId, command.Message, command.Type);

            await _notificationRepository.Add(notification);
            await _notificationRepository.SaveChange();
        }

        public async Task SendAllUnsentNotifications()
        {
            var notifications = await _notificationRepository.GetUnsentNotification();

            foreach (var notification in notifications)
            {
                switch (notification.Type)
                {
                    case NotificationType.Email:
                        Console.WriteLine($"[EMAIL] Sent to User {notification.UserId}: {notification.Message}");
                        break;
                    case NotificationType.SMS:
                        Console.WriteLine($"[SMS] Sent to User {notification.UserId}: {notification.Message}");
                        break;
                    case NotificationType.InApp:
                        Console.WriteLine($"[INTERNAL] Sent to User {notification.UserId}: {notification.Message}");
                        break;
                }
                notification.MarkAsSent();
            }

            await _notificationRepository.SaveChange();

        }
    }
}
