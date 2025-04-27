using Application.Commands.Car;
using Application.DTOs.Car;
using Domain.Models;
using System.Security.Claims;

namespace Application.Interfaces
{
    public interface ICarService
    {
        Task<string> CreateCarAsync(CreateCarCommand command, ClaimsPrincipal user);
        Task<Car> UpdateCarAsync(CarUpdateCommand command);
        Task<List<GetAllForSellerDto>> GetAllForSeller(ClaimsPrincipal user);
        Task<List<GetAllForManagerDto>> GetAllForManager();
        Task<object?> GetByIdAsync(GetCarByIdCommand command, ClaimsPrincipal user);
        Task DeleteAsync(DeleteCarByIdCommand command);
        Task<bool> ChangeCarStatusAsync(ChangeCarStatusCommand command);
        Task<PagedResultDto<CarSearchFilterDto>> SearchCarsAsync(CarSearchFilterCommand command, ClaimsPrincipal user);

    }
}
