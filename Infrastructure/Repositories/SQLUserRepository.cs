using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class SQLUserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;

        public SQLUserRepository(UserManager<User> userManager)
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

            if(userResult is null)
                return null;

            var chekPass = await _userManager.CheckPasswordAsync(userResult,password);

            if(!chekPass) 
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

            if(!userResult.Succeeded) 
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
    }
}
