namespace Domain.Models
{
    public class CarImage
    {
        public string Id { get; private set; }
        public string CarId { get; private set; }
        public string ImageUrl { get; private set; }
        public DateTime UploadedAt { get; private set; }

        public Car Car { get; private set; }

        public CarImage()
        {

        }

        public CarImage(string carId, string imageUrl)
        {
            Id = Guid.NewGuid().ToString();
            CarId = carId;
            ImageUrl = imageUrl;
            UploadedAt = DateTime.Now;
        }
    }
}
