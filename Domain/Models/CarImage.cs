using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class CarImage
    {
        public Guid Id { get; private set; }
        //public Guid CarId { get; private set; }
        public string ImageUrl { get; private set; }
        public DateTime UploadedAt { get; private set; }

        public CarImage()
        {
            
        }

        //public CarImage(Guid carId, string imageUrl)
        //{
        //    Id = Guid.NewGuid();
        //    CarId = carId;
        //    ImageUrl = imageUrl;
        //    UploadedAt = DateTime.Now;
        //}
    }
}
