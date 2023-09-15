using Microsoft.AspNetCore.Mvc;
using RunningWebApp.Interfaces;
using RunningWebApp.ViewModels;

namespace RunningWebApp.Controllers
{
    public class UserController : Controller
    {
		private readonly IUserRepository _userRepository;

		public UserController(IUserRepository userRepository)
        {
			this._userRepository = userRepository;
		}
        [HttpGet("users")] // Change route name to users
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsers();
            List<UserViewModel> result = new List<UserViewModel>(); // Create list of user view model
            foreach (var user in users)
            {
                var userVM = new UserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Pace = user.Pace,
                    Mileage = user.Mileage,
                    ProfileImageUrl = user.ProfileImageUrl
                };
                result.Add(userVM);
            }
            return View(result);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userRepository.GetUserById(id);
            var userDetailVM = new UserDetailViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Pace = user.Pace,
                Mileage = user.Mileage
            };
            return View(userDetailVM);
        }
    }
}
