using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Insurance.DataAccess.Data
{
    public static class SeedData
    {
        //async => asynchronous means the operation does not block operations
        public static async Task Initialize(IServiceProvider serviceProvider)
        {

            //retrive the RoleManager<IdentityRole> service from the service provider
            //rolemanager is a registered service  which is responsible for managing roles.
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "Admin", "Policyholder","Employee" };   
            IdentityResult roleResult;

            foreach (var rolename in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(rolename);

                if (!roleExist) 
                {
                    roleResult = await roleManager .CreateAsync(new IdentityRole(rolename));
                }

            }


        }
    }
}
