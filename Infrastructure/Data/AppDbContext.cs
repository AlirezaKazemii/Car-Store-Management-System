using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext()
        {            
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }  
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var managerRoleId = "3b30be5a-525f-4ca2-877e-950378d8a5c1";
            var buyerRoleId = "67d3150a-0ae9-4fe3-81f5-9bcaad261855";
            var sellerRoleId = "0312a64f-f09d-4f57-8665-42aa66483f5b";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = managerRoleId,
                    ConcurrencyStamp = managerRoleId,
                    Name = "Manager",
                    NormalizedName = "Manager".ToUpper()
                },
                new IdentityRole
                {
                    Id = buyerRoleId,
                    ConcurrencyStamp = buyerRoleId,
                    Name = "Buyer",
                    NormalizedName = "Buyer".ToUpper()
                },
                new IdentityRole
                {
                    Id = sellerRoleId,
                    ConcurrencyStamp = sellerRoleId,
                    Name = "Seller",
                    NormalizedName = "Seller".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

        }


    }
}
