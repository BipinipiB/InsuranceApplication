using Insurance.DataAccess.Repository;
using Insurance.DataAccess.Repository.IRepository;
using Insurance.Models;
using Insurance.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceApp.Controllers
{

    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IUserRepository _unitOfWork.userRepo;

        public AdminController(UserManager<ApplicationUser> userManager, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {

            var usersList = await _unitOfWork.userRepo.GetAllUsers();
            //sort users by first name ascending
            usersList = usersList.OrderBy(u => u.FirstName).ToList();

            return View(usersList);

        }

        //Deactivate User/Employee
        [HttpPost]
        public async Task<IActionResult> Deactivate(string? id)
        {
            var user = await _unitOfWork.userRepo.FindUserById(id);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var success = await _unitOfWork.userRepo.DeactivateUser(id);
            if (success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return StatusCode(500, "Failed to Deactivate the user.");
            }

        }

        //Activate User/Employee
        [HttpPost]
        public async Task<IActionResult> Activate(string? id)
        {

            var user = await _unitOfWork.userRepo.FindUserById(id);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var success = await _unitOfWork.userRepo.ActivateUser(id);
            if (success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return StatusCode(500, "Failed to activate the user.");
            }
        }

        //Create New User (Employee)
        public IActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //Ensures request is genuine
        public async Task<IActionResult> AddEmployee(EmployeeModel model)
        {
            //If registration form is filled correctly
            if (ModelState.IsValid)
            {
                // Create a new user based on registration details
                var user = new ApplicationUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.Email,
                    Email = model.Email
                };
                user.LockoutEnabled = false;

                // Try and add new user to database using UserRepository
                var result = await _unitOfWork.userRepo.CreateUserAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // Assign default role to user using UserRepository
                    await _unitOfWork.userRepo.AddToRoleAsync(user, "Employee");
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }


        //Add New Admin
        [HttpGet]
        public IActionResult AddAdmin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //Ensures request is genuine
        public async Task<IActionResult> AddAdmin(AdminModel model)
        {
            //If registration form is filled correctly
            if (ModelState.IsValid)
            {
                //Create new user based on registration detail
                var user = new ApplicationUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.Email,
                    Email = model.Email
                };

                user.UserName = model.Email;
                user.LockoutEnabled = false;

                //try and add new user to database
                var result = await _unitOfWork.userRepo.CreateUserAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Assign default role to user using UserRepository
                    await _unitOfWork.userRepo.AddToRoleAsync(user, "Admin");
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
    }


}