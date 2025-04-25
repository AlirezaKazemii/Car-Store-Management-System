using Application.Commands.Customer;
using Application.DTOs.Customer;
using Domain.Models;
using System.Security.Claims;

namespace Application.Interfaces
{
    public interface ICustomerService
    {
        Task RegisterOrUpdateCustomer(RegisterOrUpdateCustomerCommand command,ClaimsPrincipal user);
        Task<List<OrderHistoryDto>> GetOrderHistories(ClaimsPrincipal user);
        Task AddLoyaltyPoints(string customerId, decimal orderAmount);
        Task<GetLoyaltyPointsDto> GetLoyaltyPoints(ClaimsPrincipal user);
    }
}
