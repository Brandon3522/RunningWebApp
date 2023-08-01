using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using RunningWebApp.Data;
using RunningWebApp.Models;

namespace RunningWebApp.Controllers
{
    public class RaceController : Controller
    {
		private readonly ApplicationDbContext _context;

		public RaceController(ApplicationDbContext context) 
        {
			this._context = context;
		}
        public IActionResult Index()
        {
            List<Race> races = _context.Races.ToList();
            return View(races);
        }

        public IActionResult Detail(int id)
        {
            Race race = _context.Races.Include(a => a.Address).FirstOrDefault(c => c.Id == id);
            return View(race);
        }
    }
}
