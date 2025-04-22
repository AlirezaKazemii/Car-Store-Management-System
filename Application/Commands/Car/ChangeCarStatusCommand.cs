using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.Car;

namespace Application.Commands.Car
{
    public class ChangeCarStatusCommand
    {
        public string CarId { get; set; }
        public CarStatus NewStatus { get; set; }
    }
}
