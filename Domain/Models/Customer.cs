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
        public string UserId { get; set; }

        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Address { get; private set; }

        public User user { get; set; }
        public ICollection<Order> Orders { get; private set; }

        //امتیاز وفاداری یا امتیاز جمع شده
        public int LoyaltyPoints { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime? LastPurchaseDate { get; private set; }

        public Customer()
        {
            
        }

        public Customer(string userId,string fullName, string email, string phoneNumber, string address)
        {
            Id = Guid.NewGuid().ToString();
            UserId = userId;
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            CreatedAt = DateTime.Now;
            LoyaltyPoints = 0;
            Orders = new List<Order>();
        }

        public void UpdateCustomer(string fullName, string email, string phoneNumber, string address)
        {
            fullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
        }

        public void AddLoyaltyPoints(int point)
        {
            if (LoyaltyPoints <= 0)
                throw new ArgumentException("Points must be greater than zero.");

            LoyaltyPoints += point; 
        }


    }
}
