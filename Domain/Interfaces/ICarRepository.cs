using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
