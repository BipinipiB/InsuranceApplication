using Insurance.DataAccess.Repository.IRepository;
using Insurance.Models;
using Insurance.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceApp.Controllers
{
    public class PolicyHolderController : Controller
    {
        private static IUserService  _userService;
        private static IPolicyRepository _policyRepository;

        public PolicyHolderController(IUserService userService, IPolicyRepository policyRepository)
        {
            _userService = userService;
            _policyRepository = policyRepository;
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

                PolicyHolderInfoModel userInfo =new PolicyHolderInfoModel
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
    }
}
