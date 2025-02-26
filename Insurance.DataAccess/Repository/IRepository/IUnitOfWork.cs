

namespace Insurance.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IUserRepository userRepo { get; }
        IPolicyRepository policyRepo { get; }

        void Save();
    }
}
