using static Domain.Models.Order;

namespace Application.DTOs.Orders
{
    public class GetUserOrdersDto
    {
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public decimal CarPrice { get; set; }
    }
}
