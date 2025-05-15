using Insurance.DataAccess.Repository.IRepository;
using Insurance.Models;
using Insurance.Models.Models;
using Insurance.Services.Security;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace InsuranceApp.Controllers
{
    public class PolicyHolderController : Controller
    {
        private static IUserService _userService;
        private static IPolicyRepository _policyRepository;
        private static IQuestionRepository _questionRepo;
        private readonly SessionDataProtector _Protector;
        private static IClaimRepository _claimRepository;
        private static IClaimService _claimService;

        public PolicyHolderController(IUserService userService, IPolicyRepository policyRepository,
                                       IQuestionRepository questionRepo, SessionDataProtector Protector,
                                        IClaimRepository claimRepository, IClaimService claimService)
        {
            _userService = userService;
            _policyRepository = policyRepository;
            _questionRepo = questionRepo;
            _Protector = Protector;
            _claimRepository = claimRepository;
            _claimService = claimService;
        }


        public async Task<IActionResult> Index()
        {

            var currentUser = await _userService.GetCurrentLoggedInUserAsync();


            if (currentUser == null)
            {
                return RedirectToAction("Login", "AccountController");
            }
            else
            {
                var policyInfo = _policyRepository.GetPolicyByUserId(currentUser.Id);

                PolicyHolderInfoModel userInfo = new PolicyHolderInfoModel
                {
                    Id = currentUser.Id,
                    FirstName = currentUser.FirstName,
                    LastName = currentUser.LastName,
                    Email = currentUser.Email,
                    PolicyType = _policyRepository.GetPolicyType(policyInfo.PolicyTypeId).Name,
                    PolicyNumber = policyInfo.PolicyNumber,
                    StartDate = policyInfo.StartDate.ToString()
                };

                return View(userInfo);
            }

        }

        [HttpGet]
        public IActionResult MakeAClaim()
        {
            var allActiveQuestion = _questionRepo.GetAllActiveQuestions();

            List<QuestionDto> step1Questions = new();

            foreach (var question in allActiveQuestion)
            {
                if (question.Step == "MakeAClaim")
                {
                    step1Questions.Add(question);
                }
            }

            return View(step1Questions);
        }



        // Is triggered when Next button is clicked on the MakeAClaim page
        [HttpPost]
        public IActionResult MakeAClaimOnNext(List<QuestionDto> questions)
        {

            //seralize the list of questionAnswers
            var serializedQuestionAnswer = JsonConvert.SerializeObject(questions);

            //protect serealized data
            var ProtectorData = _Protector.Protect(serializedQuestionAnswer);

            //store the protected data in session
            HttpContext.Session.SetString("MakeAClaimInforamtion", ProtectorData);

            return RedirectToAction("PaymentInfo", "PolicyHolder");

        }

        [HttpGet]
        public IActionResult PaymentInfo()
        {

            var allActiveQuestion = _questionRepo.GetAllActiveQuestions();

            List<QuestionDto> step2Questions = new();

            foreach (var question in allActiveQuestion)
            {
                if (question.Step == "PaymentInfo")
                {
                    step2Questions.Add(question);
                }
            }

            return View(step2Questions);
        }

        [HttpPost]
        public IActionResult PaymentInformationOnNext(List<QuestionDto> questions)
        {

            //seralize the list of questionAnswers
            var serializedQuestionAnswer = JsonConvert.SerializeObject(questions);

            //protect serealized data
            var ProtectorData = _Protector.Protect(serializedQuestionAnswer);

            //store the protected data in session
            HttpContext.Session.SetString("PaymentInformation", ProtectorData);

            return RedirectToAction("ReviewClaimInformation", "PolicyHolder");
        }


        [HttpGet]
        public IActionResult ReviewClaimInformation()
        {

            // Retrieve and unprotect claim info and payment info from session
            var protectedClaimInfo = HttpContext.Session.GetString("MakeAClaimInforamtion");
            var protectedPaymentInfo = HttpContext.Session.GetString("PaymentInformation");

            var serializedClaimInfo = _Protector.Unprotect(protectedClaimInfo);
            var serializedPaymentInfo = _Protector.Unprotect(protectedPaymentInfo);

            var claimInfo = JsonConvert.DeserializeObject<List<QuestionDto>>(serializedClaimInfo);
            var paymentInfo = JsonConvert.DeserializeObject<List<QuestionDto>>(serializedPaymentInfo);

            var reviewAndSubmitDto = new ReviewAndSumitDto
            {
                ClaimInfo = claimInfo,
                PaymentInfo = paymentInfo
            };

            return View(reviewAndSubmitDto);
        }


        [HttpPost]
        public IActionResult SubmitBtnOnClick()
        {

            var protectedClaimInfo = HttpContext.Session.GetString("MakeAClaimInforamtion");
            var protectedPaymentInfo = HttpContext.Session.GetString("PaymentInformation");


            var serializedClaimInfo = _Protector.Unprotect(protectedClaimInfo);
            var serializedPaymentInfo = _Protector.Unprotect(protectedPaymentInfo);

            var claimInfo = JsonConvert.DeserializeObject<List<QuestionDto>>(serializedClaimInfo);
            var paymentInfo = JsonConvert.DeserializeObject<List<QuestionDto>>(serializedPaymentInfo);

            var reviewAndSubmitDto = new ReviewAndSumitDto
            {
                ClaimInfo = claimInfo,
                PaymentInfo = paymentInfo
            };

            var isSuccess = _claimService.SubmitClaim(reviewAndSubmitDto);

            

            return RedirectToAction("Index", "PolicyHolder");

        }

    }
}
