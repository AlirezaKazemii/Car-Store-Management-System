using Application.Commands.User;
using Application.DTOs.User;
using Application.Interfaces;
using AutoMapper;
using Domain.IRepostories;
using Domain.Models;
using FluentValidation;
using System.Security.Claims;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly ITokenRepository _tokenRepository;
        private readonly IValidator<RegisterUserCommand> _RegisterValidator;
        private readonly IValidator<LoginUserCommand> _loginValidator;
        private readonly IValidator<OtpCodeCommand> _otpValidator;
        private readonly IValidator<ResetPasswordCommand> _resetPasswordValidator;
        private readonly IValidator<ForgotPasswrodCommand> _forgotPasswrodValidator;
        private readonly IValidator<SoftDeleteCommand> _softDeleteValidator;
        private readonly IValidator<GetUserByIdCommand> _getByIdValidator;

        public UserService(IUserRepository userRepository,
            ICustomerRepository customerRepository,
            IMapper mapper,
            ITokenRepository tokenRepository,
            IValidator<RegisterUserCommand> RegisterValidator,
            IValidator<LoginUserCommand> loginValidator,
            IValidator<OtpCodeCommand> OtpValidator,
            IValidator<ResetPasswordCommand> ResetPasswordValidator,
            IValidator<ForgotPasswrodCommand> forgotPasswrodValidator,
            IValidator<SoftDeleteCommand> softDeleteValidator,
            IValidator<GetUserByIdCommand> GetByIdValidator)
        {
            _userRepository = userRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
            _tokenRepository = tokenRepository;
            _RegisterValidator = RegisterValidator;
            _loginValidator = loginValidator;
            _otpValidator = OtpValidator;
            _resetPasswordValidator = ResetPasswordValidator;
            _forgotPasswrodValidator = forgotPasswrodValidator;
            _softDeleteValidator = softDeleteValidator;
            _getByIdValidator = GetByIdValidator;
        }


        public async Task Register(RegisterUserCommand command)
        {
            var validationResult = await _RegisterValidator.ValidateAsync(command);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var user = _mapper.Map<User>(command);

            var result = await _userRepository.RegisterAsync(user, command.Password);

            if (!result)
                throw new Exception("Invalid Input");

            if (user.Role == "Buyer")
            {
                var customer = new Customer(user.Id, user.UserName, user.Email, user.PhoneNumber, "");
                _customerRepository.Add(customer);
            }

        }

        public async Task<string> Login(LoginUserCommand command, string OtpCode)
        {
            var validationResult = await _loginValidator.ValidateAsync(command);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var user = _mapper.Map<User>(command);

            var userResult = await _userRepository.LoginAsync(user, command.Password);

            if (userResult is null)
                throw new Exception("User not found Or Password is incorrect!");

            var Otp = await _userRepository.VerifyOtpCode(user, OtpCode);

            if (!Otp)
                throw new Exception("The Otp code entered is incorrect OR expired Otp code");

            var jwtToken = _tokenRepository.CreateJwtToken(userResult);

            return jwtToken;
        }

        public async Task<string> SendOtp(OtpCodeCommand command)
        {

            var validationResult = await _otpValidator.ValidateAsync(command);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var user = await _userRepository.SendOtpCode(command.PhoneNumber);

            if (user is null)
                throw new Exception("User not found!");

            var otpCode = user.GenerateOtp();

            await _userRepository.UpdateUser(user);

            return otpCode;
        }

        public async Task ResetPassword(ResetPasswordCommand command, ClaimsPrincipal user)
        {
            var resultValidation = await _resetPasswordValidator.ValidateAsync(command);
            if (!resultValidation.IsValid)
                throw new ValidationException(resultValidation.Errors);

            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException();

            var Result = await _userRepository.ResetPasswordAsync(userId, command.CurrentPassword, command.NewPassword);

            if (!Result)
                throw new Exception("invalid input (user is not found OR New password is incorrect.)");
        }

        public async Task ForgotPassword(ForgotPasswrodCommand command, string OtpCode)
        {

            var resultValidation = await _forgotPasswrodValidator.ValidateAsync(command);

            if (!resultValidation.IsValid)
                throw new ValidationException(resultValidation.Errors);

            var userResult = await _userRepository.ForgotPasswordAsync(command.UserName, command.NewPassword, OtpCode);

            if (!userResult)
                throw new Exception("invalid input (user is not found or otp code is incorrect)");
        }

        public async Task UpdateUserInfo(UpdateUserInfoCommand command, ClaimsPrincipal user)
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

            if(string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException();

            var ExistUser = await _userRepository.FindByIdAsync(userId);

            ExistUser.UpdateProfile(command.Email, command.PhoneNumber);

            await _userRepository.UpdateUser(ExistUser);
        }

        public async Task SoftDelete(SoftDeleteCommand command)
        {
            var validation = await _softDeleteValidator.ValidateAsync(command);

            if (!validation.IsValid)
                throw new UnauthorizedAccessException();

            var user = await _userRepository.FindByIdAsync(command.UserId);

            if (user is null)
                throw new Exception("user not found!");

            user.SoftDeleteMethod();
            
            await _userRepository.UpdateUser(user);
        }

        public async Task<GetUserByIdDto> GetById(GetUserByIdCommand command)
        {

            var resultValidation = await _getByIdValidator.ValidateAsync(command);

            if (!resultValidation.IsValid)
                throw new UnauthorizedAccessException();

            var user = await _userRepository.FindByIdAsync(command.UserId);

            if (user is null)
                throw new Exception("user not found!");

            var userDto = _mapper.Map<GetUserByIdDto>(user);

            return userDto;
        }

        public async Task<List<GetUserByIdDto>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();

            var usersDto = new List<GetUserByIdDto>();

            foreach (var user in users)
            {
                usersDto.Add(new GetUserByIdDto()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Role = user.Role,
                    CreateAt = user.CreatedAt
                });
            }

            return usersDto;
        }
    }
}
