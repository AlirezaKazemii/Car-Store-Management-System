using Application.Commands.Customer;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Car_Store_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }


        [HttpPost("Register-Or-Update")]
        [Authorize(Roles = "Buyer")]
        public async Task<IActionResult> RegisterOrUpdate(RegisterOrUpdateCustomerCommand command)
        {
            await _customerService.RegisterOrUpdateCustomer(command,User);

            return Ok("Customer saved successfully.");
        }


        [HttpGet("Customer-History")]
        [Authorize(Roles = "Buyer")]
        public async Task<IActionResult> GetCustomerOrderHistory()
        {
            var result = await _customerService.GetOrderHistories(User);

            if(result.Count == 0)
                return NotFound("No orders have been placed for this customer.");  

            return Ok(result);
        }


        [HttpGet("Get-LoyaltyPoints")]
        [Authorize(Roles = "Buyer")]
        public async Task<IActionResult> GetLoyaltyPoints()
        {
            var result = await _customerService.GetLoyaltyPoints(User);

            return Ok(result);
        }
    }
}
