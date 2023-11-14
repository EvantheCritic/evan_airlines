using evan_airlines.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;

namespace evan_airlines.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly AppDbContext context;
        private readonly UserManager<UserModel> userManager;
        private readonly SignInManager<UserModel> signInManager;

        public HomeController(ILogger<HomeController> _logger, AppDbContext _context, UserManager<UserModel> _userManager, SignInManager<UserModel> _signInManager)
        {
            logger = _logger;
            context = _context;
            userManager = _userManager;
            signInManager = _signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserModel _user, string password)
        {
            var user = await userManager.FindByEmailAsync(_user.Email);

            if (user != null)
            {
                var passwordCheck = await userManager.CheckPasswordAsync(user, password);

                if (passwordCheck)
                {
                    var result = await signInManager.CheckPasswordSignInAsync(user, password, false);
                    if (result.Succeeded)
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    TempData["Error"] = "Invalid Credentials. Please try again";
                    return View();
                }
            }
            else
            {
                // Handle user not found
                TempData["Error"] = "User not found";
                return View();
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            // Optional: You can also sign the user out of any external authentication providers if they were used.
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            // Redirect to home or any other page after logout
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserModel _user, string password, string confirm_password)
        {
            if (ModelState.IsValid)
            {
                if (userManager.Users.Any(u => u.Email == _user.Email))
                {
                    ModelState.AddModelError(string.Empty, "Email is already in use.");
                    return View();
                }

                if (password != confirm_password)
                {
                    ModelState.AddModelError(string.Empty, "Password and Confirm Password do not match.");
                    return View();
                }

                var user = new UserModel
                {
                    UserName = _user.UserName, // Assuming email is used as the username
                    Email = _user.Email,
                    PasswordHash = password
                };

                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    //Store their Role
                    if (user.Email == "johnsonjevane@hotmail.com")
                    {
                        await userManager.AddToRoleAsync(user, "Admin");
                    }
                    else
                    {
                        await userManager.AddToRoleAsync(user, "User");
                    }

                    // Sign in the user
                    await signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("Home/Error/{statusCode}")]
        public IActionResult Error(int statusCode)
        {
            ViewData["ErrorMessage"] = $"Status code: {statusCode}";
            return View("_Error");
        }
    }
}