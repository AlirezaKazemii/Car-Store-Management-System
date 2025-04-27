using Application.Commands.Report;
using Application.DTOs.Report;
using Application.Interfaces;
using Domain.IRepostories;
using Domain.Models;

namespace Application.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportReposiroty _reportReposiroty;
        private readonly ICustomerRepository _customerRepository;

        public ReportService(IReportReposiroty reportReposiroty, ICustomerRepository customerRepository)
        {
            _reportReposiroty = reportReposiroty;
            _customerRepository = customerRepository;
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


        public async Task<List<GetTopSellingCarsDto>> GetTopSellingCar(GetTopSellingCarsCommand command)
        {
            var orders = await _reportReposiroty.GetTopSellingCars(command.StartDate, command.EndDate,command.TopCount);

            var grouped = orders
                .GroupBy(o =>  new {o.car.Brand , o.car.Model})
                .Select(g => new GetTopSellingCarsDto
                {
                    Brand = g.Key.Brand,
                    Model = g.Key.Model,
                    TotalSold = g.Count(),
                })
                .OrderByDescending(x => x.TotalSold)
                .ToList();

            return grouped;
        }

        public async Task<CustomerBehaviorDto> GetCustomerBehavior(string customerId)
        {
            var customer = await _customerRepository.GetById(customerId);

            if (customer is null)
                throw new Exception("Customer not found!");

            var orders = await _reportReposiroty.GetOrdersByCustomerId(customerId);

            var ordersDto = new CustomerBehaviorDto
            {
                CustomerId = customer.Id,
                Name = customer.FullName,
                TotalOrders = orders.Count(),
                TotalSpent = orders.Sum(o => o.TotalAmount),
                LastOrderDate = orders.MaxBy(o => o.OrderDate)?.OrderDate,
                AveragePurchaseInterval = CalculateAverageInterval(orders),
                FavoriteBrand = FavoriteBrands(orders),
                IsActive = IsCustomerActive(orders),
                LoyaltyScore = customer.LoyaltyPoints
            };

            return ordersDto;
        }

        private static double? CalculateAverageInterval(List<Order> orders)
        {
            var dates = orders.OrderBy(o => o.OrderDate).Select(o => o.OrderDate).ToList();

            if(dates.Count < 2) return null;

            var intervals = new List<double>();

            for(int i=1; i<dates.Count; i++)
                intervals.Add((dates[i] - dates[i-1]).TotalDays);

            return intervals.Average();
        }

        private static List<string> FavoriteBrands(List<Order> orders)
        {
            return orders
            .Where(o => o.car != null)
            .GroupBy(o => o.car.Brand)
            .OrderByDescending(g => g.Count())
            .Take(3)
            .Select(g => g.Key)
            .ToList();
        }

        private static bool IsCustomerActive(List<Order> orders)
        {
            var last = orders.MaxBy(o => o.OrderDate)?.OrderDate;
            return last.HasValue && (DateTime.Now - last.Value).TotalDays <= 90;
        }

    }
}
