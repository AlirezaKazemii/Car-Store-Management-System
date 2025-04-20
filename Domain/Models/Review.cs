using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Review
    {
        public string Id { get; private set; }
        public string CarId { get; private set; }
        public string UserId { get; private set; }
        public string CustomerId { get; private set; }

        public int Rating { get; private set; } // 1 to 5
        public string Comment { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Car Car { get; private set; }
        public User User { get; private set; }
        public Customer Customer { get; private set; }

        public Review()
        {
            
        }

        public Review(string carId, string userId,string customerId ,int rating, string comment)
        {
            Id = Guid.NewGuid().ToString();
            CarId = carId;
            UserId = userId;
            CustomerId = customerId;
            Rating = rating;
            Comment = comment?.Trim();
            CreatedAt = DateTime.Now;
        }

    }
}
