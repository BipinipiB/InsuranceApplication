using Insurance.DataAccess.Repository.IRepository;
using Insurance.Models;
using Insurance.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Services
{
    public class ClaimsService : IClaimService
    {
       IUnitOfWork _unitOfWork;
       IUserService _userService;


        public ClaimsService(IUnitOfWork unitOfWork,IUserService userService)
        { 
            _unitOfWork = unitOfWork;
            _userService = userService; 

        }

        public bool SubmitClaim(ReviewAndSumitDto submittedClaimInfo)
        {
            List<QuestionDto> ClaimInfo = submittedClaimInfo.ClaimInfo;
            List<QuestionDto> PaymentInfo= submittedClaimInfo.PaymentInfo;

            //Note: for now only saving ClaimInfo
            //will need to revamp paymentInfo later


            Claims C = PrepareClaimData(ClaimInfo);

            int claimId = _unitOfWork.CreateClaim(C);

            foreach (var questionAnswer in ClaimInfo)
            {
                // Assuming you want to create a new ClaimQuestionAnswer for each question
                CreateClaimQuestionAnswer(questionAnswer, claimId);
            }

            return true;
        }

        //Used to assemble data object of type Claims
        private Claims PrepareClaimData(List<QuestionDto> ClaimInfo)
        {
            var currentUserId = _userService.GetCurrentLoggedInUserAsync().Result.Id;

            Claims c = new Claims
            {

                ClaimNumber = GenerateClaimNumber(),
                ClaimStatusId = GetDefaultStatus(),
                SubmittedOn = DateTime.Now,
                SubmittedBy = currentUserId,
                UpdatedOn = DateTime.Now,
                UpdatedBy = currentUserId
            };

            

            return c;
        }

       
       

        private string GenerateClaimNumber()
        {
            return Guid.NewGuid().ToString();
        }

        private int GetDefaultStatus()
        {
            // Assuming 4 is Sumitted in DB atm
            return 4; 
        }

        private void CreateClaimQuestionAnswer(QuestionDto questionAnswer, int claimId)
        {

            // Assuming you want to create a new ClaimQuestionAnswer for each question
            ClaimQuestionAnswer cqa = new ClaimQuestionAnswer
            {
                ClaimId = claimId,
                Questionid = questionAnswer.Id,
                AnswerJson = JsonConvert.SerializeObject(questionAnswer)
            };
            _unitOfWork.createClaimQuestionAnswer(cqa);

        }
    }
}
