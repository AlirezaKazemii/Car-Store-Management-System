using Domain.Models;

namespace Domain.IRepostories
{
    public interface ITransactionRepository
    {
        Task Add(AppTransaction transaction);
    }
}
