using Domain.Models;

namespace Domain.IRepostories
{
    public interface ICustomerRepository
    {
        Task Add(Customer customer);
        Task<Customer?> GetByUserId(string userId); 
    }
}
