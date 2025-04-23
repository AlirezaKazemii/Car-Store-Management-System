using Microsoft.AspNetCore.Http;
using static Domain.Models.Car;

namespace Application.Commands.Car
{
    public class CreateCarCommand
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public string VIN { get; set; }
        public CarStatus Status { get; set; }

        public List<IFormFile>? Images { get; set; }
    }
}