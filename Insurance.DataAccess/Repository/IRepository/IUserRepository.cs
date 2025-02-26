using Insurance.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.DataAccess.Repository
{
    public interface IUserRepository
    {
        Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password);
        Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role);

        Task<bool> DeactivateUser(string userId);

        Task<bool> ActivateUser(string userId);

        Task<ApplicationUser>? FindUserById(string userId);

        Task<List<UserViewModel>>? GetAllUsers();

        Task<IdentityResult> UpdateUserAsync(ApplicationUser user);
        Task<ApplicationUser> GetUserByEmailAsync(string email);


    }
}
