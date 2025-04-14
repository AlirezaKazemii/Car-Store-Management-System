using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
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

        public async Task<bool> RegisterAsync(User user, string password)
        {

            var userResult = await _userManager.CreateAsync(user, password);

            //if (!userResult.Succeeded)
            //    return false;

            if (user.Role != null)
                await _userManager.AddToRoleAsync(user, user.Role);

            return true;
        }
    }
}
