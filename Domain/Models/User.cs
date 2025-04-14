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

        //public ICollection<Review> Reviews { get; private set; }



        public User(string userName, string email, string phoneNumber, string role)
        {
            Id = Guid.NewGuid().ToString();
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            Role = role;
        }

        public User()
        {
            
        }

    
    }
}
