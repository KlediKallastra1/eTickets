using eTickets.Data;
using eTickets.Data.Static;
using eTickets.Entities;
using eTickets.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        public async Task<IActionResult> UserProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> UserProfile([Bind("Id,FirstName,LastName,Email")] ApplicationUser userUpdate)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["Error"] = "An Error Occurred. Please contact an administrator.";
                return View(userUpdate);
            }

            var checkEmail = await _userManager.FindByEmailAsync(userUpdate.Email);
            if(checkEmail != null)
            {
                TempData["Error"] = "This email address already exists";
                return View(userUpdate);
            }

            user.Email = userUpdate.Email;
            user.FirstName = userUpdate.FirstName;
            user.LastName = userUpdate.LastName;

            await _userManager.UpdateAsync(user);

            return RedirectToAction("Index", "Movies");
        }

        public IActionResult Login() => View(new LoginViewModel());

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var userEmail = await _userManager.FindByEmailAsync(model.Email);
            if (userEmail == null)
            {
                TempData["Error"] = "Wrong Email Address. Please, try again";
                return View(model);
            }
            var passwordCheck = await _userManager.CheckPasswordAsync(userEmail, model.Password);
            if (!passwordCheck)
            {
                TempData["Error"] = "Wrong Password. Please, try again";
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(userEmail, model.Password, false, false);
            if (!result.Succeeded)
            {
                TempData["Error"] = "An Error Occurred. Please contact an administrator.";
                return View(model);
            }
            return RedirectToAction("Index", "Movies");

        }

        [HttpPost]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Movies");
        }

        public IActionResult Register() => View(new RegisterViewModel());

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(model);
            }

            var newUser = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.FirstName + model.LastName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DOB = model.DOB
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, model.Password);
            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                return View("RegisterCompleted");
            }
            return View(model);
        }

        public IActionResult AccessDenied(string returnUrl)
        {
            return View();
        }
    }
}
