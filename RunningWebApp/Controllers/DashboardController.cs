﻿using Microsoft.AspNetCore.Mvc;
using RunningWebApp.Data;
using RunningWebApp.Repositories;
using RunningWebApp.ViewModels;

namespace RunningWebApp.Controllers
{
	public class DashboardController : Controller
	{
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardController(IDashboardRepository dashboardRepository)
		{
            this._dashboardRepository = dashboardRepository;
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
	}
}