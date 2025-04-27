using Domain.IRepostories;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using static Domain.Models.Order;

namespace Infrastructure.Repositories
{
    public class ReportRepository : IReportReposiroty
    {
        private readonly AppDbContext _context;

        public ReportRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<List<Order>> GetOrdersByDateRange(DateTime startDate, DateTime endDate)
        {
            return await _context.Orders.Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate).ToListAsync();
        }

        public async Task<List<Order>> GetTopSellingCars(DateTime startDate, DateTime endDate, int topCount)
        {
            return await _context.Orders
                .Where(o => o.Status == OrderStatus.PaymentConfirmed && o.OrderDate >= startDate && o.OrderDate <= endDate)
                .Include(o => o.car)
                .ToListAsync();
        }
        public async Task<List<Order>> GetOrdersByCustomerId(string customerId)
        {
            return await _context.Orders.Include(o => o.car).Where(o => o.CustomerId == customerId).ToListAsync();
        }
    }
}
