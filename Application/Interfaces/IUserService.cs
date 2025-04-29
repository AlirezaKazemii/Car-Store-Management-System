using Application.Commands.User;
using Application.DTOs.User;
using Domain.Models;
using System.Security.Claims;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task Register(RegisterUserCommand command);
        Task<string> Login(LoginUserCommand command, string OtpCode);
        Task<string> SendOtp(OtpCodeCommand command);
        Task ResetPassword(ResetPasswordCommand command, ClaimsPrincipal user);
        Task ForgotPassword(ForgotPasswrodCommand command, string OtpCode);
        Task UpdateUserInfo(UpdateUserInfoCommand command, ClaimsPrincipal user);
        Task SoftDelete(SoftDeleteCommand command);
        Task<GetUserByIdDto> GetById(GetUserByIdCommand command);
        Task<List<GetUserByIdDto>> GetAllUsers();
    }
}
