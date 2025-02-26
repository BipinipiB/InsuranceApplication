using Insurance.Models;
using Insurance.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.DataAccess.Repository.IRepository
{
    public interface IUserService
    {
        Task<List<PolicyHolderViewModel>> GetAllPolicyHolders();
        Task<ApplicationUser> GetCurrentLoggedInUserAsync();

    }
}
