using Application.Commands.User;
using Application.Interfaces;
using Application.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Car_Store_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand Command)
        {
            await _userService.Register(Command);
            return Ok(new Result("User added successfully"));
        }


        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command, string otpCode)
        {
            var result = await _userService.Login(command, otpCode);
            return Ok(new Result("Your login was successful.", result));
        }


        [HttpPost]
        [Route("SendOtp")]
        [AllowAnonymous]
        public async Task<IActionResult> SendOtp([FromBody] OtpCodeCommand command)
        {
            var result = await _userService.SendOtp(command);
            return Ok(new Result("Otp Code: ",result));
        }


        [HttpPut]
        [Route("ResetPassword")]
        [Authorize(Roles = "Buyer,Seller,Manager")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            await _userService.ResetPassword(command, User);
            return Ok(new Result("Password updated."));
        }


        [HttpPut]
        [Route("ForgotPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword
            ([FromBody] ForgotPasswrodCommand command, string otpCode)
        {
            await _userService.ForgotPassword(command, otpCode);
            return Ok(new Result("Password updated."));
        }


        [HttpPut]
        [Route("Edit-Informations")]
        [Authorize(Roles = "Buyer,Seller,Manager")]
        public async Task<IActionResult> Update(UpdateUserInfoCommand command)
        {
            await _userService.UpdateUserInfo(command, User);
            return Ok(new Result("User profile updated successfully."));
        }


        [HttpDelete]
        [Route("Soft-Delete")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> SoftDelete([FromQuery] SoftDeleteCommand command)
        {
            await _userService.SoftDelete(command);
            return Ok(new Result("User successfully deleted."));
        }


        [HttpGet]
        [Route("GetUser-ById")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetById([FromQuery] GetUserByIdCommand command)
        {
            var result = await _userService.GetById(command);
            return Ok(new Result("User information: ", result));
        }

        [HttpGet]
        [Route("Get-All_Users")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userService.GetAllUsers();
            return Ok(new Result("List of users: ", result));
        }

    }
}
