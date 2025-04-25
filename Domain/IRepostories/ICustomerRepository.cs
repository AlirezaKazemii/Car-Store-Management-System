using Domain.Models;

namespace Domain.IRepostories
{
    public interface ICustomerRepository
    {
        Task Add(Customer customer);
        Task<Customer?> GetByUserId(string userId); 
        Task Update(Customer customer);
        Task<List<Order>> GetOrdersWithCarsByCustomerId(string customerId);
        Task<Customer?> GetById(string customerId);
    }
}
