using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunningWebApp.Data;
using RunningWebApp.Interfaces;
using RunningWebApp.Models;
using RunningWebApp.ViewModels;

namespace RunningWebApp.Controllers
{
    public class ClubController : Controller
    {

		private readonly IClubRepository _clubRepository;
		private readonly IPhotoService _photoService;

		public ClubController(IClubRepository clubRepository, IPhotoService photoService)
        {
			this._clubRepository = clubRepository;
			this._photoService = photoService;
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClubViewModel clubVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(clubVM.Image);

                var club = new Club
                {
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Image = result.Url.ToString(),
                    Address = new Address
                    {
						Street = clubVM.Address.Street,
						City = clubVM.Address.City,
                        State = clubVM.Address.State,
                    }
                };
				_clubRepository.Add(club);
				return RedirectToAction("Index");
			}
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }

            return View(clubVM);   
        }
    }
}
