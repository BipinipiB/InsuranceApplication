using Insurance.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.DataAccess.Repository.IRepository
{

    //this is final Policy Repository Interface that will implement the 
    //base functionality that we have for all the repository
    //plus we will have update and Save for the PolicyRepository
    public interface IPolicyRepository : IRepository<Policy>
    {
        void Update(Policy policy);
        void Save();
        int GetLatestPolicyNumber();
        PolicyType GetPolicyType(int? i);

        Policy GetPolicyByUserId(string userid);

        IQueryable<Policy> GetAll();

        IEnumerable<PolicyType> GetAllPolicyTypes();

        Policy GetPolicyById (int policyId);


    }
}
