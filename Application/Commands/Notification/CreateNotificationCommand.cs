using static Domain.Models.Notification;

namespace Application.Commands.Notification
{
    public class CreateNotificationCommand
    {
        public string UserId { get; set; }
        public string Message { get; set; }
        public NotificationType Type { get; set; }
    }
}
   