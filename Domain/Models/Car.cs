using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Car
    {
        public Guid Id { get; private set; }
        public string Brand { get; private set; }
        public string Model { get; private set; }
        public int Year { get; private set; }
        public string Color { get; private set; }
        public decimal Price { get; private set; }
        public string VIN { get; private set; }
        public CarStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        //public ICollection<CarImage> Images { get; private set; }
        //public ICollection<CarCategory> CarCategories { get; private set; }
        //public ICollection<Review> Reviews { get; private set; }

        public Car()
        {
            
        }

        public Car(string brand, string model, int year, string color, decimal price, string vIN, CarStatus status)
        {
            Id = Guid.NewGuid();
            Brand = brand;
            Model = model;
            Year = year;
            Color = color;
            Price = price;
            VIN = vIN;
            Status = status;
            CreatedAt = DateTime.Now;
        }

        public enum CarStatus
        {
            New,
            Used,
            Sold
        }

        public void UpdateDetails(string brand, string model, int year, string color, decimal price)
        {
            Brand = brand;
            Model = model;
            Year = year;
            Color = color;
            Price= price;   
            UpdatedAt = DateTime.Now;
        }

        public void ChangeStatus(CarStatus status)
        {
            Status = status;
            UpdatedAt = DateTime.Now;
        }

        //public void AddImage(string imageUrl)
        //{
        //    CarImage carImage = new CarImage(Id,imageUrl);
        //    Images.Add(carImage);
        //}

    }
}
