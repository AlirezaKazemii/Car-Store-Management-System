namespace Domain.Models
{
    public class AppTransaction
    {

        public string Id { get; private set; }
        public string OrderId { get; private set; }

        public DateTime TransactionDate { get; private set; }
        public decimal Amount { get; private set; }
        //public string PaymentMethod { get; private set; }  // "BankTransfer", "CreditCard", "Cash"
        public string PaymentReceiptFileName { get; private set; }
        public TransactionStatus Status { get; private set; }
        public string? ReferenceNumber { get; private set; }
        public string? BankName { get; private set; }

        public Order Order { get; private set; }
        public ICollection<Notification> Notifications { get; private set; }

        public AppTransaction()
        {

        }

        public AppTransaction(string orderId, decimal amount, string paymentReceiptFileName)
        {
            Id = Guid.NewGuid().ToString();
            OrderId = orderId;
            Amount = amount;
            PaymentReceiptFileName = paymentReceiptFileName;
            //PaymentMethod = paymentMethod;
            TransactionDate = DateTime.Now;
            Status = TransactionStatus.Pending;
        }

        public enum TransactionStatus
        {
            Pending, // در انتظار
            Successful, // تایید شده
            Faild // رد شده
        }

        public void UpdateStatus(TransactionStatus newStatus)
        {
            Status = newStatus;
        }


    }
}
