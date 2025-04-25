using Application.Commands.Report;
using Application.DTOs.Report;
using Application.Interfaces;
using Domain.IRepostories;

namespace Application.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportReposiroty _reportReposiroty;
        private readonly IOrderRepository _orderRepository;

        public ReportService(IReportReposiroty reportReposiroty , IOrderRepository orderRepository)
        {
            _reportReposiroty = reportReposiroty;
            _orderRepository = orderRepository;
        }

        public async Task<List<SalesReportDto>> GenerateSalesReportByDateRange(SalesReportCommand command)
        {
            var orders = await _reportReposiroty.GetOrdersByDateRange(command.StartDate, command.EndDate);

            var grouped = orders
                .GroupBy(order => order.OrderDate.Date)
                .Select(group => new SalesReportDto
                {
                    Date = group.Key.ToString("yyyy/mm/d"),
                    OrdersCount = group.Count(),
                    TotalSales = group.Sum(o => o.TotalAmount)
                })
                .OrderBy(item =>  item.Date)
                .ToList();
                
            return grouped;
        }
    }
}
