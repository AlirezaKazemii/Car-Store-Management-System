

namespace Domain.Models
{
    public class Car
    {
        public string Id { get; private set; }
        public string Brand { get; private set; }
        public string Model { get; private set; }
        public int Year { get; private set; }
        public string Color { get; private set; }
        public decimal Price { get; private set; }
        public string VIN { get; private set; }
        public CarStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        public string SellerId { get; private set; }
        public User Seller { get; private set; }
        public Order Order { get; private set; }
        public ICollection<CarImage> Images { get; private set; }
        public ICollection<Review> Reviews  { get; private set; }
        public ICollection<Category> CarCategories { get; private set; }

        public Car()
        {
            
        }

        public Car(string brand, string model, int year, string color, decimal price, string vIN, CarStatus status,string sellerId)
        {
            Id = Guid.NewGuid().ToString();
            Brand = brand;
            Model = model;
            Year = year;
            Color = color;
            Price = price;
            VIN = vIN;
            Status = status;
            SellerId = sellerId;
            CreatedAt = DateTime.Now;
            Images = new List<CarImage>();
        }
            
        public enum CarStatus
        {
            Available, // موجود
            Inmaintenance, // درحال بررسی  یا درحال تعمیر
            Sold, //  فروخته شده
            Reserved, //  رزرو شده
            Inactive //  غیرفعال
        }

        public void UpdateDetails(string brand, string model, int year, string color, decimal price,CarStatus status)
        {

            if(year > DateTime.Now.Year)
            {
                throw new Exception("The production year cannot be older than the current year.");
            }

            if(price < 0)
            {
                throw new Exception("The price must be greater than zero.");
            }

            Brand = brand;
            Model = model;
            Year = year;
            Color = color;
            Price= price;   
            Status = status;
            UpdatedAt = DateTime.Now;
        }

        public void ChangeStatus(CarStatus status)
        {
            Status = status;
            UpdatedAt = DateTime.Now;
        }

        public void AddImage(string imageUrl)
        {
            CarImage carImage = new CarImage(Id, imageUrl);
            Images.Add(carImage);
        }

    }
}
