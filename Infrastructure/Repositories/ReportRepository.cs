using Domain.IRepostories;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ReportRepository : IReportReposiroty
    {
        private readonly AppDbContext _context;

        public ReportRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<List<Order>> GetOrdersByDateRange(DateTime startDate, DateTime endDate)
        {
            return _context.Orders.Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate).ToListAsync();
        }
    }
}
