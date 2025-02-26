using Insurance.DataAccess.Data;
using Insurance.DataAccess.Repository.IRepository;
using Insurance.Models;
using Insurance.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace InsuranceApp.Controllers
{
    public class AccountController : Controller
    {
        //Usermanager is a class part of ASP.NEt core Identity Framework
        //It provides a set of APIs for managing users in an application
        private readonly UserManager<ApplicationUser> _userManager;
        //SignInManager is also class of asp.net core Identity Framework
        //This handles all aspects of user sign-in and authenticatrion
        private readonly SignInManager<ApplicationUser> _signInManager;


        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        //login GET
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //login POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                //try loggin in
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    model.Email = Regex.Replace(model.Email, @"[\u200B-\u200D\uFEFF\s]", "");

                    //await waits for the task to complete before moving on to next line
                    /* ************************************************************/
                    /* ************************************************************/
                    /* ********************** NOTE TO SELF *************************/
                    /* ************************************************************/
                    /* ************************************************************/
                    //NOTE TO SELF: This line is returning userx as null so 
                    //added usery as alternative now.
                    var userx = await _userManager.FindByEmailAsync(model.Email);
                    var usery = await _userManager.Users
                            .Where(u => u.Email == model.Email)
                                 .FirstOrDefaultAsync();

                    var roles = await _userManager.GetRolesAsync(usery);

                    // directs user to admin portal or Policyholder portal based on their role
                    if (roles.Contains("Admin"))
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (roles.Contains("Policyholder"))
                    {
                        return RedirectToAction("Index", "Policyholder");
                    }
                    else
                    {
                        // Default fallback action if no specific role is found return RedirectToAction("Index", "Home");
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid Login attempt.");
            }
            return View(model);

        }


    }
}