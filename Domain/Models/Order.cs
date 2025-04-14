using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Order
    {

        public Guid Id { get; private set; }
        //public Guid CarId { get; private set; }
        //public Guid UserId { get; private set; }


        public DateTime OrderDate { get; private set; }
        public OrderStatus Status { get; private set; }
        public decimal TotalAmount { get; private set; }
        public string InvoiceNumber { get; private set; }
        public paymentStatus PaymentStatus { get; private set; }

        //public User User { get; private set; }
        //public Car car { get; private set; }

        //public Transaction Transaction { get; private set; }


        //public Order(Guid carId, Guid userId, decimal totalAmount)
        //{
        //    Id = Guid.NewGuid();
        //    CarId = carId;
        //    UserId = userId;
        //    OrderDate = DateTime.Now;
        //    TotalAmount = totalAmount;
        //    Status = OrderStatus.Pending;
        //    PaymentStatus = paymentStatus.Unpaid;
        //    InvoiceNumber = GenerateInvoiceNumber();
        //}

        public Order()
        {
            
        }

        public enum OrderStatus
        {
            Pending,
            Confirmed,
            Shipped,
            Delivered,
            Canceled
        }

        public enum paymentStatus
        {
            Unpaid,
            Paid,
            Failed,
            Refunded
        }

        // تولید شماره فاکتور 
        private string GenerateInvoiceNumber()
        {
            return $"INV-{DateTime.UtcNow.Ticks}";
        }

        //تغییر وضعیت سفارش
        public void UpdateStatus(OrderStatus status)
        {
            Status = status;
        }

        // ثبت پرداخت
        public void MarkAsPay()
        {
            PaymentStatus = paymentStatus.Paid;
        }

    }
}
