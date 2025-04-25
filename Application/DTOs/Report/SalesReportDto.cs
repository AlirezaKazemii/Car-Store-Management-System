namespace Application.DTOs.Report
{
    public class SalesReportDto
    {
        public string Date { get; set; }
        public int OrdersCount { get; set; }
        public decimal TotalSales { get; set; }
    }
}
