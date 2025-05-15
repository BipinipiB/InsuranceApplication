using Insurance.DataAccess.Repository.IRepository;
using Insurance.Models.Models;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Services
{
    public class ClaimsService : IClaimService
    {
       

        public ClaimsService()
        { 


        }

        public bool SubmitClaim(ReviewAndSumitDto submittedClaimInfo)
        {
            List<QuestionDto> ClaimInfo = submittedClaimInfo.ClaimInfo;
            List<QuestionDto> PaymentInfo= submittedClaimInfo.PaymentInfo;

            //Note: for now only saving ClaimInfo
            //will need to revamp paymentInfo later





            return true;
        }
    }
}
