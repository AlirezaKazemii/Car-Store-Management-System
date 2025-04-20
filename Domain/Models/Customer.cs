using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Customer
    {
        public string Id { get; private set; }
        public string UserId { get; private set; }

        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Address { get; private set; }

        public User user { get; private set; }
        public ICollection<Order> Orders { get; private set; }
        public ICollection<Review> Reviews { get; private set; }

        //امتیاز وفاداری یا امتیاز جمع شده
        public int LoyaltyPoints { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? LastPurchaseDate { get; private set; }

        public Customer()
        {
            
        }

        public Customer(string fullName, string email, string phoneNumber, string address)
        {
            Id = Guid.NewGuid().ToString();
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            CreatedAt = DateTime.Now;
            LoyaltyPoints = 0;
        }


    }
}
