using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Transaction
    {
        
        public string Id { get; set; }
        public string OrderId { get; set; }

        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }  // "BankTransfer", "CreditCard", "Cash"
        public TransactionStatus Status { get; set; }
        public string? ReferenceNumber { get; set; }
        public string? BankName { get; set; }

        public Order Order { get; set; }
        public ICollection<Notification> Notifications { get; set; }

        public Transaction()
        {
            
        }

        public Transaction(Guid orderId, decimal amount, string paymentMethod)
        {
            Id = Guid.NewGuid().ToString();
            OrderId = OrderId;
            Amount = amount;
            PaymentMethod = paymentMethod;
            TransactionDate = DateTime.Now;
            Status = TransactionStatus.Pending;
        }

        public enum TransactionStatus
        {
            Pending,
            Successful,
            Faild
        }


    }
}
