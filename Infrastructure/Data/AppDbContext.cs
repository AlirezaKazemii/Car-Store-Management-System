using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<AppTransaction> Transactions { get; set; }

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


            // User and Customer (1:1)
            builder.Entity<User>()
                .HasOne(u => u.Customer)
                .WithOne(c => c.user)
                .HasForeignKey<Customer>(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // User and Car (1:many)
            builder.Entity<User>()
                .HasMany(u => u.Cars)
                .WithOne(c => c.Seller)
                .HasForeignKey(c => c.SellerId)
                .OnDelete(DeleteBehavior.Restrict);

            // User and Notification (1:many)
            builder.Entity<Notification>()
                .HasOne(n => n.user)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId);

            // Car and CarImage (1:many)
            builder.Entity<CarImage>()
                .HasOne(i => i.Car)
                .WithMany(c => c.Images)
                .HasForeignKey(i => i.CarId)
                .OnDelete(DeleteBehavior.Restrict);             

            // Order and Customer (1:many)
            builder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Order and Car (1:1)
            builder.Entity<Order>()
                .HasOne(o => o.car)
                .WithOne(c => c.Order)
                .HasForeignKey<Order>(o => o.CarId)
                .OnDelete(DeleteBehavior.Restrict);

            // Order and Transaction (1:1)
            builder.Entity<AppTransaction>()
                .HasOne(t => t.Order)
                .WithOne(o => o.Transaction)
                .HasForeignKey<AppTransaction>(t => t.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
            

            // car and category 
            // نیازی به پیکربندی ندارد 

        }


    }
}
