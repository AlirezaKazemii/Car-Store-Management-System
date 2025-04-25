using Application.Commands.Customer;
using Application.DTOs.Customer;
using Application.Interfaces;
using Domain.IRepostories;
using Domain.Models;
using System.Security.Claims;

namespace Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserRepository _userRepository;

        public CustomerService(ICustomerRepository customerRepository, IUserRepository userRepository)
        {
            _customerRepository = customerRepository;
            _userRepository = userRepository;
        }


        public async Task RegisterOrUpdateCustomer(RegisterOrUpdateCustomerCommand command, ClaimsPrincipal CalimUser)
        {

            var userId = CalimUser.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException();
            }

            var customer = await _customerRepository.GetByUserId(userId);

            if(customer is null)
            {
                customer = new Customer(userId,command.FullName,command.Email,command.PhoneNumber,command.Address);
                await _customerRepository.Add(customer);
            }
            else
            {
                customer.UpdateCustomer(command.FullName,command.Email,command.PhoneNumber,command.Address);    
                await _customerRepository.Update(customer);

                var user = await _userRepository.FindByIdAsync(userId);
                if (user != null)
                {
                    user.UpdateProfile(command.Email, command.PhoneNumber);
                    await _userRepository.UpdateUser(user);
                }
            }
        }

        public async Task<List<OrderHistoryDto>> GetOrderHistories(ClaimsPrincipal user)
        {

            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException();
            }

            var customer = await _customerRepository.GetByUserId(userId);

            var ordersHistory = await _customerRepository.GetOrdersWithCarsByCustomerId(customer.Id);

            var orderHistoryDto = new List<OrderHistoryDto>();

            foreach (var order in ordersHistory)
            {
                orderHistoryDto.Add(
                    new OrderHistoryDto
                    {
                        OrderId = order.Id,
                        OrderDate = order.OrderDate,
                        CarBrand = order.car.Brand,
                        CarModel = order.car.Model,
                        CarYear = order.car.Year,
                        CarPrice = order.car.Price,
                        CarTotalAmount = order.TotalAmount,
                        Status = order.Status.ToString(),
                    });
            }

            foreach (var order in orderHistoryDto)
            {
                switch (order.Status)
                {
                    case "0":
                        order.Status = "Pending";
                        break;
                    case "1":
                        order.Status = "Approved";
                        break;
                    case "2":
                        order.Status = "Rejected";
                        break;
                    case "3":
                        order.Status = "AwaitingPayment";
                        break;
                    case "4":
                        order.Status = "PaymentUploaded";
                        break;
                    case "5":
                        order.Status = "PaymentConfirmed";
                        break;
                    case "6":
                        order.Status = "Canceled";
                        break;
                }
            }

            return orderHistoryDto;
        }

        public async Task AddLoyaltyPoints(string customerId, decimal orderAmount)
        {
            var customer = await _customerRepository.GetById(customerId);

            if (customer is null)
                throw new Exception("Customer not found!");

            int point = (int)(orderAmount / 100000);
            customer.AddLoyaltyPoints(point);

            await _customerRepository.Update(customer);
        }

        public async Task<GetLoyaltyPointsDto> GetLoyaltyPoints(ClaimsPrincipal user)
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException();
            }

            var customer = await _customerRepository.GetByUserId(userId);

            if (customer is null)
                throw new Exception("Customer not found!");

            var loyaltyPointDto = new GetLoyaltyPointsDto
            {
                LoyaltyPoint = customer.LoyaltyPoints
            };

            return loyaltyPointDto;
        }
    }
}
