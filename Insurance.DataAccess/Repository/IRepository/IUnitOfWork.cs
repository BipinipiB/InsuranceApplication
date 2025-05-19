

using Insurance.Models.Models;

namespace Insurance.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IUserRepository userRepo { get; }
        IPolicyRepository policyRepo { get; }

        IQuestionTypeRepository questionTypeRepo { get; }
        void Save();

        int CreateClaim(Claims claim);

        Boolean createClaimQuestionAnswer(ClaimQuestionAnswer claimquestionasnwer);
    }
}
