using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Domain.Models;
using Application.Mapping;
using AutoMapper;
using Application.Interfaces;

namespace Car_Store_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _autoMapper;
        private readonly IUserRepository _userRepository;

        public UserController(IMapper mapper, IUserRepository userRepository)
        {
            _autoMapper = mapper;
            _userRepository = userRepository;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterRequestDto registerRequestDto)
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
    }
}
