using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class User : IdentityUser
    {
        public string Role { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? LastLogin { get; private set; }
        public string? OtpCode { get; private set; }
        public DateTime? OtpGeneratedAt { get; private set; }


        public Customer Customer { get; set; } // اگر مشتری باشه 
        public ICollection<Order> Orders { get; set; }
        public ICollection<Car> Cars { get; set; } // اگر فروشنده باشه 
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Review> Reviews { get; private set; }


        public User()
        {

        }

        public User(string userName, string email, string phoneNumber, string role)
        {
            Id = Guid.NewGuid().ToString();
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            Role = role;
            CreatedAt = DateTime.Now;
        }

        public void UpdateProfile(string email, string phoneNumber)
        {
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public string GenerateOtp()
        {
            OtpCode = new Random().Next(1000, 9999).ToString();
            OtpGeneratedAt = DateTime.Now;
            return OtpCode;
        }

        // expired
        public void ClearOtp()
        {
            OtpCode = null;
            OtpGeneratedAt = null;
        }

        public void SetLastLogin(DateTime? lastLogin)
        {
            LastLogin = lastLogin;
        }

        public void SetCreatedAt(DateTime createdAt)
        {
            CreatedAt = createdAt;
        }

    }
}