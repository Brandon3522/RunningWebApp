using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using RunningWebApp.Data;
using RunningWebApp.Interfaces;
using RunningWebApp.Models;
using RunningWebApp.Repositories;
using RunningWebApp.Services;
using RunningWebApp.ViewModels;

namespace RunningWebApp.Controllers
{
    public class RaceController : Controller
    {
		private readonly IRaceRepository _raceRepository;
		private readonly IPhotoService _photoService;

		public RaceController(IRaceRepository raceRepository, IPhotoService photoService) 
        {
			this._raceRepository = raceRepository;
			this._photoService = photoService;
		}
        public async Task<IActionResult> Index()
        {
            IEnumerable<Race> races = await _raceRepository.GetAll();
            return View(races);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Race race = await _raceRepository.GetByIdAsync(id);
            return View(race);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRaceViewModel RaceVM)
        {
            if (ModelState.IsValid)
            {
				var result = await _photoService.AddPhotoAsync(RaceVM.Image);

				var race = new Race
				{
					Title = RaceVM.Title,
					Description = RaceVM.Description,
					Image = result.Url.ToString(),
					Address = new Address
					{
						Street = RaceVM.Address.Street,
						City = RaceVM.Address.City,
						State = RaceVM.Address.State,
					}
				};
				_raceRepository.Add(race);
				return RedirectToAction("Index");
			}
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }

            return View(RaceVM);
        }
    }
}
