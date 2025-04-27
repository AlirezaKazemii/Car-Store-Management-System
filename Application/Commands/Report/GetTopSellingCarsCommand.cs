namespace Application.Commands.Report
{
    public class GetTopSellingCarsCommand
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TopCount { get; set; }
    }
}
