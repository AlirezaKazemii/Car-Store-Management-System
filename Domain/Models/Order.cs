namespace Domain.Models
{
    public class Order
    {

        public string Id { get; private set; }
        public string CarId { get; private set; }
        public string UserId { get; private set; }
        public string CustomerId { get; set; }


        public DateTime OrderDate { get; private set; }
        public DateTime UpdateAt { get; private set; }
        public OrderStatus Status { get; private set; }
        public decimal TotalAmount { get; private set; }
        public string InvoiceNumber { get; private set; }
        public paymentStatus PaymentStatus { get; private set; }

        public User User { get; private set; }
        public Car car { get; private set; }
        public Customer Customer { get; private set; }
        public AppTransaction Transaction { get; private set; }


        public Order(string carId, string userId, string customerId, decimal totalAmount)
        {
            Id = Guid.NewGuid().ToString();
            CarId = carId;
            UserId = userId;
            CustomerId = customerId;
            TotalAmount = totalAmount;
            OrderDate = DateTime.Now;
            CustomerId = customerId;
            Status = OrderStatus.Pending;
            PaymentStatus = paymentStatus.Unpaid;
            InvoiceNumber = GenerateInvoiceNumber();
        }

        public Order()
        {

        }

        public enum OrderStatus
        {
            Pending, // سفارش ثبت شده، در انتظار بررسی مدیر
            Approved, // تایید شده توسط مدیر
            Rejected,  // رد شده
            AwaitingPayment, // تایید شده، در انتظار پرداخت توسط مشتری
            PaymentUploaded,
            PaymentConfirmed,
            Canceled  // لغو شده توسط مشتری یا سیستم
        }

        public enum paymentStatus
        {
            Unpaid,  // هنوز پرداختی انجام نشده
            Paid,  // پرداخت با موفقیت انجام شده
            Failed,  // پرداخت ناموفق بوده
            Refunded // مبلغ پرداختی برگشت داده شده
        }

        // تولید شماره فاکتور 
        private string GenerateInvoiceNumber()
        {
            return $"INV-{DateTime.Now.Ticks}";
        }

        //تغییر وضعیت سفارش
        public void UpdateStatus(OrderStatus newStatus)
        {
            if (newStatus == Status) return;

            Status = newStatus;
            UpdateAt = DateTime.Now;
        }

        // ثبت پرداخت
        public void MarkAsPay()
        {
            PaymentStatus = paymentStatus.Paid;
        }

    }
}
