using static Domain.Models.Car;

namespace Application.DTOs.Car
{
    internal class GetByIdCarDto
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public string VIN { get; set; }
        public CarStatus Status { get; set; }
        public List<string> Images { get; set; }
    }
}