
using Insurance.DataAccess.Data;
using Insurance.DataAccess.Repository;
using Insurance.DataAccess.Repository.IRepository;
using Insurance.DataAccess.Services;
using Insurance.Models;
using Insurance.Models.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace InsuranceApp.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPolicyService _policyService;

        public EmployeeController(IUserRepository userRepository,
                 UserManager<ApplicationUser> userManager, IPolicyRepository policyRepo,
                 IUserService userService, IUnitOfWork unitOfWork, IPolicyService policyService)
        {
            _userManager = userManager;
            _userService = userService;
            _unitOfWork = unitOfWork;
            _policyService = policyService;
        }

        public IActionResult Index()
        {
            return View();
        }

        //this action triggers when User wants to register new policy holder
        //and clicks "Register Policy Holder" button in Index/HomePage
        [HttpGet]
        public async Task<IActionResult> RegisterCustomer()
        {
            var policyTypes = _unitOfWork.policyRepo.GetAllPolicyTypes();
            var model = new RegisterCustomerModel
            {
                // Convert to list
                PolicyTypeList = policyTypes.Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.Id.ToString() // Assuming Id is the unique identifier
                }).ToList(),
                //startdate should be atleast tomorrow or 12 am of next day
                StartDate = DateTime.Now.AddDays(1).Date
            };

            // Pass min date as ViewBag property
            ViewBag.MinStartDate = model.StartDate.ToString("yyyy-MM-dd");

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CustomerPolicyInformation()
        {
            ViewData["Title"] = "PolicyInformation";

            var policyTypes = _unitOfWork.policyRepo.GetAllPolicyTypes();

            var model = new PolicyVM
            {
                Policy = new Policy(),
                PolicyTypeList = policyTypes.Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.Id.ToString() // Assuming Id is the unique identifier
                }).ToList() // Convert to list
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer(RegisterCustomerInputModel customerInfo)
        {
            if (ModelState.IsValid && customerInfo != null)
            {
                //Save customer personal information in TempData
                TempData["CustomerInfo"] = JsonConvert.SerializeObject(customerInfo);
                return RedirectToAction("ReviewAndSubmit");
            }
            else
            {
                return View(customerInfo);
            }
        }

        [HttpPost]
        public IActionResult AddPolicyInformation(PolicyVM policyInfo)
        {
            ///if(ModelState.IsValid && policyInfo != null)
            if (policyInfo != null)
            {
                //Save policy information in TempData
                /* var testt = _dbContext.PolicyType.FirstOrDefault(pt => pt.Id == policyInfo.Policy.PolicyTypeId);
                 TempData["PolicyType"] = testt.Name;
                 TempData["PolicyStartDate"] = policyInfo.Policy.StartDate;
                 TempData["PolicyPremium"] = policyInfo.Policy.Premium;
 */
                TempData["PolicyInfo"] = JsonConvert.SerializeObject(policyInfo);

                return RedirectToAction("ReviewAndSubmit");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        //Review And Submit
        [HttpGet]
        public IActionResult ReviewAndSubmit()
        {
            var customerInfoJson = TempData["CustomerInfo"] as string;

            //var policyInfoJson = TempData["PolicyInfo"] as string;

            // Keep TempData for the next request
            TempData.Keep("CustomerInfo");
            //TempData.Keep("PolicyInfo");

            var customerInfo = customerInfoJson != null
                ? JsonConvert.DeserializeObject<RegisterCustomerInputModel>(customerInfoJson)
                : new RegisterCustomerInputModel();

            /****************************************Custom verification starts  *******************************************/


            if (customerInfo.StartDate <= DateTime.Now.AddDays(1) && customerInfo.PolicyNumber == null)
            {
                ModelState.AddModelError("StartDate", "Start Date should be greater than today");
                return RedirectToAction("RegisterCustomer");
            } /****************************************Custom verification Ends  *******************************************/

            else
            {
                var model = new ReviewAndSubmitViewModel
                {
                    FirstName = customerInfo.FirstName,
                    LastName = customerInfo.LastName,
                    Email = customerInfo.Email,
                    Premium = customerInfo.Premium,
                    StartDate = customerInfo.StartDate
                };

                //model.SelectedPolicyType = await _dbContext.PolicyType.FirstOrDefaultAsync(pt => pt.Id == customerInfo.PolicyTypeId);

                if (customerInfo.PolicyTypeId != null)
                {
                    model.SelectedPolicyType = _unitOfWork.policyRepo.GetPolicyType(customerInfo.PolicyTypeId);

                }
                else
                {
                    model.SelectedPolicyType = null;
                }

                return View(model);
            }

        }

        //Handles Submit button to create PolicyHolder
        [HttpPost]
        public async Task<IActionResult> ConfirmAndSubmit()
        {
            // Retrieve data from TempData
            var customerInfoJson = TempData["CustomerInfo"] as string;
            //var policyInfoJson = TempData["PolicyInfo"] as string;

            // Ensure data is available in TempData for the next request
            // TempData["CustomerInfo"] = customerInfoJson;
            //TempData["PolicyInfo"] = policyInfoJson;

            var customerInfo = customerInfoJson != null
               ? JsonConvert.DeserializeObject<RegisterCustomerInputModel>(customerInfoJson)
               : new RegisterCustomerInputModel();

            if (customerInfo == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                
                var user1 = await _userManager.FindByEmailAsync(customerInfo.Email);

                //new user??
                if(user1 == null)
                {

                    //Step2: Create new user with PolicyHolder role
                    var user = new ApplicationUser
                    {
                        FirstName = customerInfo.FirstName,
                        LastName = customerInfo.LastName,
                        UserName = customerInfo.Email,
                        Email = customerInfo.Email,
                        LockoutEnabled = false
                    };

                    // Try and add new user to database using UserRepository
                    //var result = await _userRepository.CreateUserAsync(user, "Outsystems@123");
                    var result = await _unitOfWork.userRepo.CreateUserAsync(user, "Outsystems@123");

                    if (result.Succeeded)
                    {
                        //Step3: Create new policy
                        // Create new policy
                        Policy PolicyModel = new Policy
                        {
                            StartDate = customerInfo.StartDate,
                            Premium = customerInfo.Premium,
                            PolicyTypeId = customerInfo.PolicyTypeId,
                            UserId = user.Id
                        };

                        //Note some custom validations can be done in here
                        PolicyModel.CreatedOn = DateTime.Now;
                        PolicyModel.CreatedById = _userManager.GetUserId(User);
                        PolicyModel.UpdatedById = _userManager.GetUserId(User);
                        PolicyModel.UpdatedOn = DateTime.Now;

                        //get latest policy number in DB , add plus one and assign new policyNumber
                        //int latestPolicyNumber = _policyRepo.GetLatestPolicyNumber();
                        PolicyModel.PolicyNumber = "1";

                        PolicyModel.IsActive = true;

                        //Add the New policy to the DbContext
                        //_policyRepo.Add(PolicyModel);
                        _unitOfWork.policyRepo.Add(PolicyModel);

                        // Save changes to the database
                        _unitOfWork.policyRepo.Save();

                        // Step2: Assign PolicyHolder role to user using UserRepository
                        //await _userRepository.AddToRoleAsync(user, "PolicyHolder");
                        await _unitOfWork.userRepo.AddToRoleAsync(user, "PolicyHolder");

                       
                        return RedirectToAction("Index");
                    }

                }
                else
                {
                    var result = await _unitOfWork.userRepo.UpdateUserAsync(user1);

                    if (result.Succeeded)
                    {
                        Policy PolicyModel = new Policy
                        {
                            StartDate = customerInfo.StartDate,
                            Premium = customerInfo.Premium,
                            PolicyTypeId = customerInfo.PolicyTypeId,
                            UserId = user1.Id
                        };

                        //Note some custom validations can be done in here
                        PolicyModel.UpdatedById = _userManager.GetUserId(User);
                        PolicyModel.UpdatedOn = DateTime.Now;

                        _unitOfWork.policyRepo.Update(PolicyModel);
                    }


                }
            }

            return RedirectToAction("Index");
            /*// Keep TempData for the next request
            TempData.Keep("CustomerInfo");
            TempData.Keep("PolicyInfo");*/
            //return RedirectToAction("CreatePolicyHolderAndPolicy", "Account");
        }

        [HttpGet]
        public async Task<IActionResult> ViewAllPolicyHolders()
        {

            List<PolicyHolderViewModel> policyHoldersList = await _userService.GetAllPolicyHolders();

            return View(policyHoldersList);
        }

        [HttpGet]
        public async Task<IActionResult> ViewAllPolicies()
        {

            List<PolicyListVM> policyHoldersList = await _policyService.GetAllActivelPolicyList();

            return View(policyHoldersList);
        }


        [HttpGet]
        public async Task<IActionResult> EditPolicy(int PolicyId)
        {

            var model = await _policyService.GetPolicyForEditByPolicyId(PolicyId);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditCustomerPolicyNext(RegisterCustomerModel customerInfo)
        {
            if (ModelState.IsValid && customerInfo != null)
            {
                //Save customer personal information in TempData
                TempData["CustomerInfo"] = JsonConvert.SerializeObject(customerInfo);
                return RedirectToAction("ReviewAndSubmit");
            }
            else
            {
                return View(customerInfo);
            }

        }


        [HttpPost]
        public  async Task<IActionResult> DeletePolicy(int PolicyId)
        {
            
          var result = await _policyService.DeactivatePolicyByPolicyId(PolicyId);

            if(result)
            {
                return RedirectToAction("ViewAllPolicies");
            }
            else
            {
                return RedirectToAction("ViewAllPolicies");
            }

           
        }
    }
}

