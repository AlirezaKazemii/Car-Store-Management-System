using Application.CommandHandlers;
using Application.Commands.Car;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace Car_Store_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarHandler _carHandler;

        public CarController(ICarHandler carHandler)
        {
            _carHandler = carHandler;
        }


        [HttpPost]
        [Route("Create")]
        [Authorize(Roles = "Manager,Seller")]
        public async Task<IActionResult> Create([FromForm] CreateCarCommand command)
        {
            // دریافت SellerId از Claims (احراز هویت)
            var sellerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(sellerId))
            {
                throw new UnauthorizedAccessException("Seller must be logged in.");
            }

            var carId = await _carHandler.CreateCarAsync(command, sellerId);

            return Ok("The car was registered.\n\n" + carId);
        }


        [HttpPut("Update/{id}")]
        [Authorize(Roles = "Manager,Seller")]
        public async Task<IActionResult> Update(string id, [FromBody] CarUpdateCommand command)
        {
            var car = await _carHandler.UpdateCarAsync(command,id);

            return Ok("User information updated.\n\n" + car);
        }

        [HttpGet("GetAllForSeller")]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> GetAllForSeller()
        {
            // دریافت SellerId از Claims (احراز هویت)
            var sellerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(sellerId))
            {
                throw new UnauthorizedAccessException("Seller must be logged in.");
            }

            var carList = await _carHandler.GetAllForSeller(sellerId);
            
            return (carList.Count > 0) ? Ok(carList) : BadRequest("No cars found for this user.");
        }

        [HttpGet]
        [Route("GetAllForManager")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetAllForManager()
        {
            var carList = await _carHandler.GetAllForManager();

            return (carList.Count > 0) ? Ok(carList) : BadRequest("No car found!");
        }

        //[HttpGet]
        //[Route("GetAllForBuyer")]
        //[Authorize(Roles = "Buyer")]
        //public async Task<IActionResult> GetAllForBuyer()
        //{

        //}

        [HttpGet("GetById")]
        [Authorize(Roles = "Manager,Buyer,Seller")]
        public async Task<IActionResult> GetById(string carId)
        {
            var carResult = await _carHandler.GetByIdAsync(carId, User);

            if(carResult is null)
                return NotFound();

            if(carResult == "Forbidden")
                return Forbid();

            return Ok(carResult);
        }

        [HttpDelete("Delete")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete(string carId)
        {
            await _carHandler.DeleteAsync(carId);

            return Ok("The car was successfully deleted.");
        }

    }
}
    