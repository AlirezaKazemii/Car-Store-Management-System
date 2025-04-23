namespace Application.DTOs.Orders
{
    public class InvoiceDto
    {
        public string InvoiceNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CarTitle { get; set; }
        public decimal Price { get; set; }
        public DateTime orderDate { get; set; }
        public DateTime PaidDate { get; set; }
        public string OrderID { get; set; }

    }
}
