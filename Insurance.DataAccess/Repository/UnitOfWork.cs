using Insurance.DataAccess.Data;
using Insurance.DataAccess.Repository.IRepository;
using Insurance.Models;
using Insurance.Models.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace Insurance.DataAccess.Repository.IRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public IUserRepository userRepo { get; private set; }

        public IPolicyRepository policyRepo { get; private set; }

        public IQuestionTypeRepository questionTypeRepo { get; private set; }

        //SignInManager is also class of asp.net core Identity Framework
        //This handles all aspects of user sign-in and authenticatrion
        private readonly SignInManager<ApplicationUser> _signInManager;


        public UnitOfWork(ApplicationDbContext db, UserManager<ApplicationUser> userManager,
                            SignInManager<ApplicationUser> signInManager)
        {
            _db = db;
            _userManager = userManager;
            userRepo = new UserRepository(_userManager);
            policyRepo = new PolicyRepository(_db);
            _signInManager = signInManager;
            questionTypeRepo = new QuestionTypeRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public async Task<string> GetUserRoleAsync(ApplicationUser user)
        {
            if (await _userManager.IsInRoleAsync(user, "Admin"))
            {
                return "Admin";
            }

            if (await _userManager.IsInRoleAsync(user, "Employee"))
            {
                return "Employee";
            }

            return string.Empty;
        }


        public QuestionTypeDto GetQuestionType(int i)
        {

            return questionTypeRepo.GetQuestionTypeById(i);
        }

    }
}
