using Insurance.Models;
using Insurance.Models.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.DataAccess.Repository
{
    public class PolicyHolderRepository : UserRepository
    {
        public PolicyHolderRepository(UserManager<ApplicationUser> userManager) : base(userManager)
        {

        }

        public void GetAllPolicyHolders()
        {

            GetAllUsers();

        }
    }
}
