using Insurance.DataAccess.Repository.IRepository;
using Insurance.Models.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Services
{
    public class PolicyService : IPolicyService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IPolicyTypeService _policyTypeService;
        private readonly IUserService _userService;

        public PolicyService(IUnitOfWork unitOfWork, IPolicyTypeService policyTypeService, IUserService userService)
        {

            _unitOfWork = unitOfWork;
            _policyTypeService = policyTypeService;
            _userService = userService;
        }

        public async Task<List<PolicyListVM>> GetAllPolicyList()
        {
           List<Policy> PolicyList = _unitOfWork.policyRepo.GetAll().ToList();

           List<PolicyListVM> policyListVMList = new List<PolicyListVM>();
         

            foreach (var policy in PolicyList)
            {
                var user = await _unitOfWork.userRepo.FindUserById(policy.UserId);

                policyListVMList.Add(new PolicyListVM
                {
                    PolicyId = policy.PolicyId,
                    PolicyNumber = policy.PolicyNumber,
                    PolicyType =  _unitOfWork.policyRepo.GetPolicyType(policy.PolicyTypeId).Name,
                    Premium = policy.Premium,
                    StartDate = policy.StartDate,
                    IsActive = policy.IsActive,
                    UserId = policy.UserId,
                    PolicyHolderName = user.FirstName + " " + user.LastName,
                    PolicyHolderEmail = user.Email
                });

            }

            return policyListVMList;

        }

        //returns all active policies
        public async Task<List<PolicyListVM>> GetAllActivelPolicyList()
        {
            //get all policies
            var allPolicies = await GetAllPolicyList();
            //return all active policies 
            return allPolicies.Where(p => p.IsActive == true).ToList();
        }


        public async Task<RegisterCustomerModel> GetPolicyForEditByPolicyId(int PolicyId)
        {

            Policy policy = _unitOfWork.policyRepo.GetPolicyById(PolicyId);

            var user = await _unitOfWork.userRepo.FindUserById(policy.UserId);


            var ListOfPolicyTypes = await _policyTypeService.GetPolicyTypesAsync();

            RegisterCustomerModel model = new RegisterCustomerModel
            {
                 

                FirstName = user.FirstName,
                LastName = user.LastName,
                Email   = user.Email,
                PolicyNumber = policy.PolicyNumber,
                PolicyId = policy.PolicyId,
                PolicyTypeId = policy.PolicyTypeId,
                PolicyType = policy.PolicyType,
                Premium = policy.Premium,
                StartDate = policy.StartDate,
                PolicyTypeList = ListOfPolicyTypes.Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.Id.ToString() // Assuming Id is the unique identifier
                }).ToList()
            };

            
            return model;
        }





        /*work in progress */
        public Policy CreatePolicyModel(RegisterCustomerModel customerInfo, string userId, 
            string createdById, string updatedById,DateTime createdOn)
        {
            return new Policy
            {
                StartDate = customerInfo.StartDate,
                Premium = customerInfo.Premium,
                PolicyTypeId = customerInfo.PolicyTypeId,
                UserId = userId,
                CreatedOn = createdOn,
                CreatedById = createdById, 
                UpdatedById = updatedById, 
                UpdatedOn = DateTime.Now,
                PolicyNumber = "1", /****NEED TO CHANGE THIS ***/
                IsActive = true
            };
        }


        public Boolean DeletePolicyByPolicyId(int PolicyId)
        {
            //Step 1: Deactivate Policy

            var result =  DeactivatePolicyByPolicyId(PolicyId);


            //Step 2: Deactivate User

           

            return true;
        }

        public async Task<bool> DeactivatePolicyByPolicyId(int PolicyId)
        {
            

            //Deactivate Policy

             Policy _policy = _unitOfWork.policyRepo.GetPolicyById(PolicyId);

             string CurrentPolicyHolderId = _policy.UserId;

            //set deactive 
            _policy.IsActive = false;
            _policy.UpdatedById = (await _userService.GetCurrentLoggedInUserAsync()).Id;
            _policy.UpdatedOn = DateTime.Now;
            _unitOfWork.policyRepo.Update(_policy);
            _unitOfWork.Save();


            //Deactivate PolicyHolder user 
             
            _unitOfWork.userRepo.DeactivateUser(CurrentPolicyHolderId);
            _unitOfWork.Save();

            return true;
        }

      
    }
}
