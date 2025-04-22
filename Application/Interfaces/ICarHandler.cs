using Application.Commands.Car;
using Application.DTOs.Car;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICarHandler
    {
        Task<string> CreateCarAsync(CreateCarCommand command, string sellerId);
        Task<Car> UpdateCarAsync(CarUpdateCommand command, string carId);
        Task<List<GetAllForSellerDto>> GetAllForSeller(string sellerId);
        Task<List<GetAllForManagerDto>> GetAllForManager();
        Task<object?> GetByIdAsync(string carId, ClaimsPrincipal user);
        Task DeleteAsync(string carId);
        Task<bool> ChangeCarStatusAsync(ChangeCarStatusCommand command);
        Task<PagedResultDto<CarSearchFilterDto>> SearchCarsAsync(CarSearchFilterCommand command, ClaimsPrincipal user);

    }
}
