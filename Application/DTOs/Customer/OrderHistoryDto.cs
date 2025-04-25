namespace Application.DTOs.Customer
{
    public class OrderHistoryDto
    {
        public string OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public int CarYear { get; set; }
        public decimal CarPrice { get; set; }
        public decimal CarTotalAmount { get; set; }
        public string Status { get; set; }
    }
}