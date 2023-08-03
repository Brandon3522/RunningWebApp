using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunningWebApp.Data;
using RunningWebApp.Interfaces;
using RunningWebApp.Models;

namespace RunningWebApp.Controllers
{
    public class ClubController : Controller
    {

		private readonly IClubRepository _clubRepository;

		public ClubController(IClubRepository clubRepository)
        {
			this._clubRepository = clubRepository;
		}
        public async Task<IActionResult> Index() // Controller
        {
            IEnumerable<Club> clubs = await _clubRepository.GetAll(); // Model
            return View(clubs); // View
        }

        public async Task<IActionResult> Detail(int id) 
        {
            Club club = await _clubRepository.GetByIdAsync(id); // Return the first club that has given ID
            return View(club);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Club club)
        {
            if (!ModelState.IsValid)
            {
                return View(club);
            }

            _clubRepository.Add(club);
            return RedirectToAction("Index");
        }
    }
}
