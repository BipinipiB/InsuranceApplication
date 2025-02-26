using Insurance.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.DataAccess.Repository.IRepository
{
    public interface IPolicyService
    {

        Task<List<PolicyListVM>>? GetAllPolicyList();
        Task<List<PolicyListVM>>? GetAllActivelPolicyList();
        Task<RegisterCustomerModel> GetPolicyForEditByPolicyId(int PolicyId);

        Task<bool> DeactivatePolicyByPolicyId(int PolicyId);

    }
}
