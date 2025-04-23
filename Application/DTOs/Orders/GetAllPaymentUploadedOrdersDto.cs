using static Domain.Models.Order;

namespace Application.DTOs.Orders
{
    public class GetAllPaymentUploadedOrdersDto
    {
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public decimal Price { get; set; }
        public string InvoiceNumber { get; set; }
    }
}
