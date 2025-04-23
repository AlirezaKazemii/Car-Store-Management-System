using static Domain.Models.Car;

namespace Application.Commands.Car
{
    public class CarUpdateCommand
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public CarStatus Status { get; set; }
    }
}