using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> RegisterAsync(User user, string password);
        Task<User> LoginAsync(User user, string password);
        Task<string> SendOtpCode(string phoneNumber);
        Task<bool> VerifyOtpCode(User user, string otpCode);

    }
}
