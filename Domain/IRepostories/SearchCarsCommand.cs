using static Domain.Models.Car;

namespace Domain.IRepostories
{
    public class SearchCarsCommand
    {
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public CarStatus? Status { get; set; }
        public string? Color { get; set; }


        public int? MinYear { get; set; }
        public int? MaxYear { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
