using Application.Commands.Car;
using Application.Commands.Notification;
using Domain.Models;

namespace Application.Interfaces
{
    public interface INotificationService
    {
        Task CreateNotification(CreateNotificationCommand command);
        Task SendAllUnsentNotifications();
    }
}
