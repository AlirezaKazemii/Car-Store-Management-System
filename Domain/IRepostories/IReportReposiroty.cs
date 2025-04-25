using Domain.Models;

namespace Domain.IRepostories
{
    public interface IReportReposiroty
    {
        Task<List<Order>> GetOrdersByDateRange(DateTime startDate, DateTime endDate);
    }
}
