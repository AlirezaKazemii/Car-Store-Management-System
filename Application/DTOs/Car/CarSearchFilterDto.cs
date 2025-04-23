using static Domain.Models.Car;

namespace Application.DTOs.Car
{
    public class CarSearchFilterDto
    {
        public string Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public CarStatus Status { get; set; }
    }
}