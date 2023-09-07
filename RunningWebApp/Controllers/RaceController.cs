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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RaceController(IRaceRepository raceRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor) 
        {
			this._raceRepository = raceRepository;
			this._photoService = photoService;
            this._httpContextAccessor = httpContextAccessor;
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
            var currentUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var createRaceViewModel = new CreateRaceViewModel { AppUserId = currentUserId };
            return View(createRaceViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRaceViewModel raceVM)
        {
            if (ModelState.IsValid)
            {
				var result = await _photoService.AddPhotoAsync(raceVM.Image);

				var race = new Race
				{
					Title = raceVM.Title,
					Description = raceVM.Description,
					Image = result.Url.ToString(),
                    AppUserId = raceVM.AppUserId,
					Address = new Address
					{
						Street = raceVM.Address.Street,
						City = raceVM.Address.City,
						State = raceVM.Address.State,
					}
				};
				_raceRepository.Add(race);
				return RedirectToAction("Index");
			}
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }

            return View(raceVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var race = await _raceRepository.GetByIdAsync(id);

            if (race == null) return View("Error");

            var raceVM = new EditRaceViewModel
            {
                Id = race.Id,
                Title = race.Title,
                Description = race.Description,
                AddressId = race.AddressId,
                Address = race.Address,
                URL = race.Image
            };

            return View(raceVM);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditRaceViewModel raceVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit the race");
                return View("Edit", raceVM);
            }

            var userRace = await _raceRepository.GetByIdAsyncNoTracking(id);

            if (raceVM != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(userRace.Image);
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError("", "Error deleting image");
                    return View(raceVM);
                }

                var photoResult = await _photoService.AddPhotoAsync(raceVM.Image);

                var race = new Race
                {
                    Id = raceVM.Id,
                    Title = raceVM.Title,
                    Description = raceVM.Description,
                    AddressId = raceVM.AddressId,
                    Address = raceVM.Address,
                    Image = photoResult.Url.ToString(),
                };

                _raceRepository.Update(race);

                return RedirectToAction("Index");
            }
            else
            {
                return View(raceVM);
            }
        }

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			var race = await _raceRepository.GetByIdAsync(id);

			if (race == null)
			{
				return View("Error");
			}

			return View(race);
		}

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteRace(int id)
        {
            var race = await _raceRepository.GetByIdAsync(id);

            Console.WriteLine("In delete race function");

            if (race == null)
            {
                return View("Error");
            }

            _raceRepository.Delete(race);
            return RedirectToAction("Index");
        }
    }
}
