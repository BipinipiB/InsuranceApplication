using Insurance.DataAccess.Data;
using Insurance.DataAccess.Repository.IRepository;
using Insurance.Models;
using Insurance.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InsuranceApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _userManager = userManager;
            _unitOfWork = unitOfWork;   
        }

        public async Task<IActionResult> Index()
        {
           
            //if user is already logged in 
            if (User.Identity.IsAuthenticated)
            {
                //Get Logged in user info
                var currentUser = await _userManager.GetUserAsync(User);
               // var currentUser = await _unitOfWork.userRepo.Ge

                //check if logged in user is admin
                var isAdmin = await _userManager.IsInRoleAsync(currentUser, "Admin");

                var isEmployee = await _userManager.IsInRoleAsync(currentUser, "Employee");

                if (isAdmin)
                {
                    return RedirectToAction("Index", "Admin");
                }

                if (isEmployee)
                {
                    return RedirectToAction("Index", "Employee"); ;
                }

            }
            else
            {
                return View();
            }
            return View();

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
