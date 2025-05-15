using Insurance.DataAccess.Data;
using Insurance.DataAccess.Repository.IRepository;
using Insurance.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.DataAccess.Repository
{

    public class ClaimRepository : IClaimRepository
    {
        private readonly ApplicationDbContext _db;

        public ClaimRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Boolean AddClaim(Claims claim)
        {
            if(claim == null)
            {
                return false;
            }
            else { 
                  
                _db.Claims.Add(claim);
                return true;
            }
   
        }

        public Task<Claims?> GetClaimById(int claimId)
        {
            throw new NotImplementedException();
        }

        public Boolean UpdateClaim(Claims claim)
        {
            return true;
        }

       
    }
}
