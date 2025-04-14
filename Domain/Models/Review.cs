using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Review
    {
        public Guid Id { get; private set; }
        //public Guid CarId { get; private set; }
        //public Guid UserId { get; private set; }
        public int Rating { get; private set; } // 1 to 5
        public string Comment { get; private set; }
        public DateTime CreatedAt { get; private set; }

        //public Car Car { get; private set; }
        //public User User { get; private set; }

        public Review()
        {
            
        }

        //public Review(Guid carId, Guid userId, int rating, string comment)
        //{
        //    Id = Guid.NewGuid();
        //    CarId = carId;
        //    UserId = userId;
        //    Rating = rating;
        //    Comment = comment?.Trim();
        //    CreatedAt = DateTime.Now;
        //}

    }
}
