using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Transaction
    {
        
        public string Id { get; private set; }
        public string OrderId { get; private set; }

        public DateTime TransactionDate { get; private set; }
        public decimal Amount { get; private set; }
        public string PaymentMethod { get; private set; }  // "BankTransfer", "CreditCard", "Cash"
        public TransactionStatus Status { get; private set; }
        public string? ReferenceNumber { get; private set; }
        public string? BankName { get; private set; }

        public Order Order { get; private set; }
        public ICollection<Notification> Notifications { get; private set; }

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
