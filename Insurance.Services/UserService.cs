using Insurance.DataAccess.Repository.IRepository;
using Insurance.Models;
using Insurance.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.DataAccess.Repository
{
    public class UserService : IUserService
    {
        private readonly IPolicyRepository _policyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UserService( IPolicyRepository policyRepository, IUnitOfWork unitOfWork, 
                    UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _policyRepository = policyRepository;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<PolicyHolderViewModel>> GetAllPolicyHolders()
        {
            var policyHolderList = new List<PolicyHolderViewModel>();
            var usersList = await _unitOfWork.userRepo.GetAllUsers();


                foreach (var user in usersList)
                {
                    if (user.Roles.Contains("Policyholder"))
                    {
                        var policy = _policyRepository.GetPolicyByUserId(user.Id);

                        var policyHolder = new PolicyHolderViewModel
                        {
                            Users = user,
                            PolicyName = policy.PolicyType.Name

                        };
                        policyHolderList.Add(policyHolder);
                    }
                }
            
            return policyHolderList;


        }

        public async Task<ApplicationUser> GetCurrentLoggedInUserAsync()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            return user;
        }
    }
}
