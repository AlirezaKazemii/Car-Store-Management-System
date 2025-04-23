using Domain.IRepostories;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using static Domain.Models.Order;

namespace Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }


        public async Task<Order?> GetById(string orderId)
        {
            return await _context.Orders.FirstOrDefaultAsync(x => x.Id == orderId); 
        }

        public async Task SaveChange()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<Order>> GetAllPendingOrders()
        {
            return await _context.Orders.Where(x => x.Status == OrderStatus.Pending).ToListAsync();
        }

        public async Task<List<Order>> GetAllPaymentUploadedOrders()
        {
            return await _context.Orders.Where(x => x.Status == OrderStatus.PaymentUploaded).ToListAsync();
        }
    }
}
 