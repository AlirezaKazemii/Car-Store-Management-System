using Domain.IRepostories;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface ICarRepository
    {
        Task CreateCarAsync(Car car);
        Task UpdateCarAsync(Car car);
        Task<Car?> FindByIdAsync(string carId);
        Task<List<Car>> GetAllForSerller(string sellerId);
        Task<List<Car>> GetAllForManager();
        Task SaveChangeAsync();
        Task<(List<Car> items, int totalCount)> SearchCarsAsync(SearchCarsCommand command , bool isSeller, string id);
    }
}
