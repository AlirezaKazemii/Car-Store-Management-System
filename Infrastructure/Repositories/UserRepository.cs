using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> SendOtpCode(string phoneNumber)
        {
            var existUser = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);

            if (existUser is null)
                return null;

            var otpCode = existUser.GenerateOtp();

            await _userManager.UpdateAsync(existUser);

            return otpCode;

        }

        public async Task<User> LoginAsync(User user, string password)
        {
            var userResult = await _userManager.FindByNameAsync(user.UserName);

            if (userResult is null)
                return null;

            var chekPass = await _userManager.CheckPasswordAsync(userResult, password);

            if (!chekPass)
                return null;

            return userResult;

        }

        public async Task<bool> RegisterAsync(User user, string password)
        {

            var userResult = await _userManager.CreateAsync(user, password);

            if (!userResult.Succeeded)
                return false;

            if (user.Role != null)
                userResult = await _userManager.AddToRoleAsync(user, user.Role);

            if (!userResult.Succeeded)
                return false;

            user.SetCreatedAt(DateTime.Now);

            return true;
        }

        public async Task<bool> VerifyOtpCode(User user, string otpCode)
        {
            var existUser = await _userManager.FindByNameAsync(user.UserName);

            // expired
            if (existUser is null || existUser.OtpCode != otpCode)
            {
                existUser.ClearOtp();
                await _userManager.UpdateAsync(existUser);
                return false;
            }

            // expired
            if (existUser.OtpGeneratedAt == null || (DateTime.Now - existUser.OtpGeneratedAt.Value).TotalMinutes > 2)
            {
                existUser.ClearOtp();
                await _userManager.UpdateAsync(existUser);
                return false;
            }


            existUser.ClearOtp();
            existUser.SetLastLogin(DateTime.Now);
            await _userManager.UpdateAsync(existUser);


            return true;
        }

        public async Task<bool> ResetPasswordAsync(string id, string CurrentPassword, string NewPassword)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
                return false;

            var checkPassword = await _userManager.CheckPasswordAsync(user, CurrentPassword);

            if (!checkPassword)
                return false;

            var result = await _userManager.ChangePasswordAsync(user, CurrentPassword, NewPassword);

            if (!result.Succeeded)
                return false;

            return true;
        }

        public async Task<bool> ForgotPasswordAsync(string userName, string newPassword, string otpCode)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user is null)
                return false;

            if (user.OtpCode != otpCode)
                return false;

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            var resetPassword = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);

            return true;
        }

        public async Task<User?> FindByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task UpdateUser(User user)
        {
            await _userManager.UpdateAsync(user);
        }
    }
}
