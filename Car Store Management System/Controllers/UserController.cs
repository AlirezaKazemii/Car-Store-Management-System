using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Domain.Models;
using Application.Mapping;
using AutoMapper;
using Application.Interfaces;
using Microsoft.AspNetCore.Identity.Data;

namespace Car_Store_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _autoMapper;
        private readonly IUserRepository _userRepository;
        private readonly ITokenRepository _tokenRepository;

        public UserController(IMapper mapper, IUserRepository userRepository, ITokenRepository tokenRepository)
        {
            _autoMapper = mapper;
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            // Map Dto to Model
            var user = _autoMapper.Map<User>(registerRequestDto);            

            var result = await _userRepository.RegisterAsync(user, registerRequestDto.Password);

            if(!result)
                return BadRequest("Invalid Input");

            return Ok("User added successfully");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _autoMapper.Map<User>(loginRequestDto);

            var userResult = await _userRepository.LoginAsync(user, loginRequestDto.Password);

            if(userResult is null)
                return NotFound("User not found Or Password is incorrect!");

            var jwtToken =  _tokenRepository.CreateJwtToken(userResult);

            return Ok(jwtToken);

        }
    }   
}
    