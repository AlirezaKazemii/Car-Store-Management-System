using Application.Commands.Car;
using Application.Commands.User;
using Application.Interfaces;
using Application.Shared;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Car_Store_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carHandler;

        public CarController(ICarService carHandler)
        {
            _carHandler = carHandler;
        }


        [HttpPost]
        [Route("Create")]
        [Authorize(Roles = "Manager,Seller")]
        public async Task<IActionResult> Create([FromForm] CreateCarCommand command)
        {
            var carId = await _carHandler.CreateCarAsync(command, User);

            return Ok(new Result("The car was registered.",carId));
        }


        [HttpPut("Update/{id}")]
        [Authorize(Roles = "Manager,Seller")]
        public async Task<IActionResult> Update([FromBody] CarUpdateCommand command)
        {
            var car = await _carHandler.UpdateCarAsync(command);

            return Ok(new Result("User information updated.",car));
        }


        [HttpGet("GetAllForSeller")]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> GetAllForSeller()
        {
            var carList = await _carHandler.GetAllForSeller(User);

            return (carList.Count > 0) ? Ok(new Result("Car List: ",carList)) : BadRequest("No cars found for this user.");
        }


        [HttpGet]
        [Route("GetAllForManager")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetAllForManager()
        {
            var carList = await _carHandler.GetAllForManager();

            return (carList.Count > 0) ? Ok(new Result("Car List: ",carList)) : BadRequest("No car found!");
        }


        //[HttpGet]
        //[Route("GetAllForBuyer")]
        //[Authorize(Roles = "Buyer")]
        //public async Task<IActionResult> GetAllForBuyer()
        //{

        //}


        [HttpGet("GetById")]
        [Authorize(Roles = "Manager,Buyer,Seller")]
        public async Task<IActionResult> GetById([FromQuery] GetCarByIdCommand command)
        {
            var carResult = await _carHandler.GetByIdAsync(command, User);

            if (carResult is null)
                return NotFound();

            if (carResult == "Forbidden")
                return Forbid();

            return Ok(new Result("Car Information: ",carResult));
        }


        [HttpDelete("Delete")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete(DeleteCarByIdCommand command)
        {
            await _carHandler.DeleteAsync(command);

            return Ok(new Result("The car was successfully deleted."));
        }


        [HttpPut("ChangeCarStatus")]
        [Authorize(Roles = "Manager,Seller")]
        public async Task<IActionResult> ChangeCarStatus([FromBody] ChangeCarStatusCommand command)
        {
            var result = await _carHandler.ChangeCarStatusAsync(command);

            return result ? Ok(new Result("Car status changed.")) : BadRequest("Car not found!");
        }


        [HttpGet("SearchCars")]
        [Authorize(Roles = "Manager,Buyer,Seller")]
        public async Task<IActionResult> SearchCars([FromQuery] CarSearchFilterCommand command)
        {
            var result = await _carHandler.SearchCarsAsync(command, User);

            return Ok(new Result("",result));
        }

    }
}
