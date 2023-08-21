using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RunningWebApp.Data;
using RunningWebApp.Models;
using RunningWebApp.ViewModels;

namespace RunningWebApp.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly ApplicationDbContext _context;

		// Dependency injection
		public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext context)
		{
			_context = context;
			_signInManager= signInManager;
			_userManager= userManager;
		}

		// Functions as HTTP GET request
		public IActionResult Login()
		{
			// Keep values on page refresh
			var response = new LoginViewModel();
			return View(response);
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel loginVM)
		{
			if (!ModelState.IsValid)
			{
				return View(loginVM);
			}

			var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);

			if (user != null)
			{
				// User exists, check password
				var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
				if (passwordCheck)
				{
					// Password found
					var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
					if (result.Succeeded)
					{
						return RedirectToAction("Index", "Race");
					}
				}
				// Password incorrect
				TempData["Error"] = "Wrong password or email"; // Not good practice

				return View(loginVM);
			}
			// User not found
			TempData["Error"] = "Wrong password or email"; // Not good practice
			return View(loginVM);
		}
	}
}
