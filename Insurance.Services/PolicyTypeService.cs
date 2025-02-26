
using Microsoft.EntityFrameworkCore;
using Insurance.DataAccess.Data;
using Insurance.Models.Models;
using Insurance.DataAccess.Repository.IRepository;


namespace Insurance.DataAccess.Services
{
    public class PolicyTypeService : IPolicyTypeService
    {
        private readonly ApplicationDbContext _dbContext;

        public PolicyTypeService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;            

        }

        public async Task<IEnumerable<PolicyType>> GetPolicyTypesAsync()
        {
            return await _dbContext.PolicyType.ToListAsync();
        }

       
    }
}
