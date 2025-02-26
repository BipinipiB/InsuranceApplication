using Insurance.DataAccess.Repository.IRepository;
using Insurance.Models;
using Insurance.Models.Models;
using Microsoft.AspNetCore.Identity;

namespace Insurance.DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;


        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;

        }

        public async Task<bool> ActivateUser(string userId)
        {

            var user = await _userManager.FindByIdAsync(userId);

            //allow the user to log in again
            user.LockoutEnabled = false;

            //remove any lockout expiration date
            user.LockoutEnd = null;
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<IdentityResult> CreateUserAsync(ApplicationUser user, string? password)
        {
            if (!string.IsNullOrEmpty(password))
            {
                return await _userManager.CreateAsync(user, password);

            }
            else
            {
                return await _userManager.CreateAsync(user);
            }
        }

        public async Task<bool> DeactivateUser(string? userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            //allow the user to log in again
            user.LockoutEnabled = true;

            //remove any lockout expiration date
            user.LockoutEnd = null;
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public Task<ApplicationUser> FindUserById(string? userId)
        {
            return _userManager.FindByIdAsync(userId);
        }

        public async Task<List<UserViewModel>> GetAllUsers()
        {
            var users = _userManager.Users.ToList();
            var usersList = new List<UserViewModel>();

            foreach (var user in users)
            {
                //Get all roles for the user
                var roles = await _userManager.GetRolesAsync(user);

                //Add user data to the view model

                // Add user data to the view model
                usersList.Add(new UserViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    IsActive = !user.LockoutEnabled,
                    // Assign the roles to the model
                    Roles = roles.ToList()
                });
            }

            return usersList;
        }

        public async Task<IdentityResult> UpdateUserAsync(ApplicationUser user)
        {
          
                return await _userManager.UpdateAsync(user);
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {

            return await _userManager.FindByEmailAsync(email);

        }

       
    }
}