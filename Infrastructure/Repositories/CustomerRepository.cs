using Domain.IRepostories;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(Customer customer)
        {
            await _context.Customers.AddAsync(customer);    
            await _context.SaveChangesAsync();
        }

        public async Task<Customer?> GetByUserId(string userId)  
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.UserId == userId);
        }


        public async Task Update(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Order>> GetOrdersWithCarsByCustomerId(string customerId)
        {
            return await _context.Orders
                .Include(o => o.car)
                .Where(o => o.CustomerId == customerId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();  
        }

        public async Task<Customer?> GetById(string customerId)
        {
            return await _context.Customers.FindAsync(customerId);
        }
    }
}
