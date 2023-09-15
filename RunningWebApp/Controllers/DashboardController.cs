﻿using Microsoft.AspNetCore.Mvc;
using RunningWebApp.Data;
using RunningWebApp.Interfaces;
using RunningWebApp.ViewModels;
using RunningWebApp.Models;
using CloudinaryDotNet.Actions;

namespace RunningWebApp.Controllers
{
    public class DashboardController : Controller
	{
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPhotoService _photoService;

        public DashboardController(IDashboardRepository dashboardRepository, IHttpContextAccessor httpContextAccessor, IPhotoService photoService)
		{
            this._dashboardRepository = dashboardRepository;
            this._httpContextAccessor = httpContextAccessor;
            this._photoService = photoService;
        }

		private void MapUserEdit(AppUser user, EditUserDashboardViewModel editVM, ImageUploadResult photoResult)
		{
			user.Id = editVM.Id;
			user.Pace = editVM.Pace;
			user.Mileage = editVM.Mileage;
			user.ProfileImageUrl= photoResult.Url.ToString();
			user.City = editVM.City;
			user.State = editVM.State;
		}

		public async Task<IActionResult> Index()
		{
			var userRaces = await _dashboardRepository.GetAllUserRaces();
			var userClubs = await _dashboardRepository.GetAllUserClubs();
			var dashboardViewModel = new DashboardViewModel()
			{
				Races = userRaces,
				Clubs = userClubs
			};
			return View(dashboardViewModel);
		}

		public async Task<IActionResult> EditUserProfile()
		{
			var currentUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
			AppUser user = await _dashboardRepository.GetUserById(currentUserId);

			if (user == null)
			{
				return View("Error");
			}

			var editUserVM = new EditUserDashboardViewModel()
			{
				Id = currentUserId,
				Pace = user.Pace,
				Mileage = user.Mileage,
				ProfileImageUrl = user.ProfileImageUrl,
				City = user.City,
				State = user.State
			};
			return View(editUserVM);
		}

		[HttpPost]
		public async Task<IActionResult> EditUserProfile(EditUserDashboardViewModel editVM)
		{
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("", "Failed to edit profile");
				return View("EditUserProfile",editVM);
			}

			var user = await _dashboardRepository.GetByUserIdNoTracking(editVM.Id); // App user object, enables direct property manipulation

			// User does not have a photo, add photo
			if (user.ProfileImageUrl == "" || user.ProfileImageUrl == null)
			{
				var photoresult = await _photoService.AddPhotoAsync(editVM.Image);

				// Optimistic concurrency - "tracking error"
				// Use no tracking

				MapUserEdit(user, editVM, photoresult);

				_dashboardRepository.Update(user);

				return RedirectToAction("Index");
			}
			else // User already has a photo, delete then add photo
			{
				try
				{
					await _photoService.DeletePhotoAsync(user.ProfileImageUrl);
				}
				catch (Exception ex)
				{

					ModelState.AddModelError("", "Unable to delete photo");
					return View(editVM);
				}

                var photoresult = await _photoService.AddPhotoAsync(editVM.Image);

                // Optimistic concurrency - "tracking error"
                // Use no tracking

                MapUserEdit(user, editVM, photoresult);

                _dashboardRepository.Update(user);

                return RedirectToAction("Index");
            }
		}
	}
}
