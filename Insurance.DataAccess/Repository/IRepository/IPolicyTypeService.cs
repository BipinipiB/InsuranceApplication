using Insurance.Models.Models;

namespace Insurance.DataAccess.Repository.IRepository
{
    public interface IPolicyTypeService
    {

        Task<IEnumerable<PolicyType>> GetPolicyTypesAsync();

    }
}
