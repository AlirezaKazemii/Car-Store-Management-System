using Domain.Models;

namespace Domain.IRepostories
{
    public interface IReportReposiroty
    {
        Task<List<Order>> GetOrdersByDateRange(DateTime startDate, DateTime endDate);
        Task<List<Order>> GetTopSellingCars(DateTime startDate, DateTime endDate, int topCount);
        Task<List<Order>> GetOrdersByCustomerId(string customerId);
    }
}
