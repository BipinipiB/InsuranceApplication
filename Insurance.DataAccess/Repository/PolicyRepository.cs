using Insurance.DataAccess.Data;
using Insurance.DataAccess.Repository.IRepository;
using Insurance.Models.Models;
using System.Data.Entity;

namespace Insurance.DataAccess.Repository
{

    //here we are implmeneting Repository<Policy> and IPolicyRepository 
    // We added Repository<Policy> because we already have implementation of base functionality such as
    //add,get,getall etc in base interface
    //this helps prevent duplication
    public class PolicyRepository : Repository<Policy>, IPolicyRepository
    {
        private ApplicationDbContext _db;

        // :base(db) is to pass implementaion of ApplicationDBContext to all the base class
        //that is the dbcontext we get here we will pass to the base class i.e. Repository.CS
        public PolicyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public int GetLatestPolicyNumber()
        {

            return int.Parse(_db.Policies.OrderByDescending(m => m.PolicyNumber) //Order by Policy Number Descending
                                             .Select(m => m.PolicyNumber) //Select first or top policy numer
                                             .FirstOrDefault());
        }

        public PolicyType GetPolicyType(int? i)
        {

            return _db.PolicyType.Find(i);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Policy policy)
        {
            _db.Update(policy);
        }

        public void Add(Policy policy)
        {
            _db.Add(policy);
        }

        public Policy GetPolicyByUserId(string userId)
        {
            Policy currentPolicy = _db.Policies.FirstOrDefault(p => p.UserId == userId);
            currentPolicy.PolicyType = _db.PolicyType.FirstOrDefault(pt => pt.Id == currentPolicy.PolicyTypeId);

            return currentPolicy;

        }

        IQueryable<Policy> IPolicyRepository.GetAll()
        {
            return _db.Policies;
        }

        public IEnumerable<PolicyType> GetAllPolicyTypes()
        {
            return  _db.PolicyType.ToList();

        }

        public Policy GetPolicyById(int inPolicyId)
        {
            Policy currentPolicy = _db.Policies.FirstOrDefault(p => p.PolicyId == inPolicyId);
            currentPolicy.PolicyType = _db.PolicyType.FirstOrDefault(pt => pt.Id == currentPolicy.PolicyTypeId);

            return currentPolicy;

        }
    }
}
