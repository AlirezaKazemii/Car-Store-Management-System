namespace Application.DTOs.Report
{
    public class GetTopSellingCarsDto
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int TotalSold { get; set; }
    }
}
