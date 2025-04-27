using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Notification
    {
        public string Id { get; private set; }
        public string UserId { get; private set; }
        public string? TransactionId { get; set; }

        public string Message { get; private set; }
        public NotificationType Type { get; private set; }
        public bool IsSent { get; set; }
        public DateTime SentAt { get; private set; }
            
        public User user { get; private set; }
        public AppTransaction Transaction { get; private set; }

        public Notification(string userId,string message, NotificationType type)
        {
            Id = Guid.NewGuid().ToString();
            UserId = userId;
            //TransactionId = transactionId;
            Message = message;
            Type = type;
            IsSent = false;
            SentAt = DateTime.Now;
        }
           
        public enum NotificationType
        {
            Email,
            SMS,
            InApp
        }


        public void MarkAsSent()
        {
            IsSent = true;  
        }

    }
}
