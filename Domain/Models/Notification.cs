using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Notification
    {
        public Guid Id { get; private set; }
        //public Guid UserId { get; private set; }
        public string Message { get; private set; }
        public NotificationType Type { get; private set; }
        public NotificationStatus Status { get; private set; }
        public DateTime SentAt { get; private set; }

        //public Notification(Guid userId, string message, NotificationType type)
        //{
        //    Id = Guid.NewGuid();
        //    UserId = userId;
        //    Message = message;
        //    Type = type;
        //    Status = NotificationStatus.UnRead;
        //    SentAt = DateTime.Now;
        //}

        public enum NotificationType
        {
            Email,
            SMS,
            InApp
        }

        public enum NotificationStatus
        {
            UnRead,
            Read,
            Sent,
            Faild
        }
    }
}
