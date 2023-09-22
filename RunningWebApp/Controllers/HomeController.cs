using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RunningWebApp.Helper;
using RunningWebApp.Interfaces;
using RunningWebApp.Models;
using RunningWebApp.ViewModels;
using System.Diagnostics;
using System.Globalization;
using System.Net;

namespace RunningWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IClubRepository _clubRepository;

        public HomeController(ILogger<HomeController> logger, IClubRepository clubRepository)
        {
            _logger = logger;
            this._clubRepository = clubRepository;
        }

        public async Task<IActionResult> Index()
        {
            var ipInfo = new IPinfo();
            var homeViewModel = new HomeViewModel();

            // Get users location
            // Take the location and ping our database, bring into view
            try
            {
                string url = "https://ipinfo.io?token=b9e0fafe45318e";
                var info = new WebClient().DownloadString(url);
                ipInfo = JsonConvert.DeserializeObject<IPinfo>(info); // Turn Json into object
                RegionInfo myRI1 = new RegionInfo(ipInfo.Country);
                ipInfo.Country = myRI1.EnglishName;
                homeViewModel.City = ipInfo.City;
                homeViewModel.State = ipInfo.Region;
                if (homeViewModel.City != null)
                {
                    homeViewModel.Clubs = await _clubRepository.GetClubByCity(homeViewModel.City);
                }
                else
                {
                    homeViewModel.Clubs = null;
                }
                return View(homeViewModel);
            }
            catch (Exception ex)
            {

                homeViewModel.Clubs = null;
            }
            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}