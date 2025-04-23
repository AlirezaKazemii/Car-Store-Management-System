using static Domain.Models.Car;

namespace Application.Commands.Car
{
    public class ChangeCarStatusCommand
    {
        public string CarId { get; set; }
        public CarStatus NewStatus { get; set; }
    }
}