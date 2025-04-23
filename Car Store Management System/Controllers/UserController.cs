using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.IRepostories;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Car_Store_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _autoMapper;
        private readonly IUserRepository _userRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly ICustomerRepository _customerRepository;

        public UserController(IMapper mapper, IUserRepository userRepository, ITokenRepository tokenRepository,ICustomerRepository customerRepository)
        {
            _autoMapper = mapper;
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
            _customerRepository = customerRepository;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Map Dto to Model
            var user = _autoMapper.Map<User>(registerRequestDto);

            var result = await _userRepository.RegisterAsync(user, registerRequestDto.Password);

            if (!result)
                return BadRequest("Invalid Input");

            if(user.Role == "Buyer")
            {
                var customer = new Customer(user.Id, user.UserName, user.Email, user.PhoneNumber, "");
                _customerRepository.Add(customer);
            }

            return Ok("User added successfully");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto, string otpCode)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _autoMapper.Map<User>(loginRequestDto);

            var userResult = await _userRepository.LoginAsync(user, loginRequestDto.Password);

            if (userResult is null)
                return NotFound("User not found Or Password is incorrect!");

            var Otp = await _userRepository.VerifyOtpCode(user, otpCode);

            if (!Otp)
                return BadRequest("The Otp code entered is incorrect OR expired Otp code");

            var jwtToken = _tokenRepository.CreateJwtToken(userResult);

            return Ok(jwtToken);

        }

        [HttpPost]
        [Route("SendOtp")]
        public async Task<IActionResult> SendOtp([FromBody] OtpRequestDto otpRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var otpCode = await _userRepository.SendOtpCode(otpRequestDto.PhoneNumber);

            if (otpCode is null)
                return NotFound("User not found!");

            return Ok(otpCode);
        }

        [Authorize(Roles = "Buyer,Seller,Manager")]
        [HttpPut]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User identity not found.");

            var Result = await _userRepository.ResetPasswordAsync(userId, request.CurrentPassword, request.NewPassword);

            if (!Result)
                return BadRequest("User is not found OR New password is incorrect.");

            return Ok("Password updated.");
        }

        [HttpPut]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword
            ([FromBody] ForgotPasswrodRequestDto request, string otpCode)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userResult = await _userRepository.ForgotPasswordAsync(request.UserName, request.NewPassword, otpCode);

            return userResult ?
                Ok("Password updated.") :
                BadRequest("invalid input (user is not found or otp code is incorrect");
            //if (!userResult) ;
            //    return BadRequest("invalid input (user is not found or otp code is incorrect");

            //return Ok("Password updated.");
        }


    }
}
