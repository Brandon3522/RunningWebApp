using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunningWebApp.Data;
using RunningWebApp.Models;

namespace RunningWebApp.Controllers
{
    public class ClubController : Controller
    {

        // Context == database
        private readonly ApplicationDbContext _context;

        public ClubController(ApplicationDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index() // Controller
        {
            List<Club> clubs = _context.Clubs.ToList(); // Model
            return View(clubs); // View
        }

        public IActionResult Detail(int id) 
        {
            Club club =  _context.Clubs.Include(a => a.Address).FirstOrDefault(c => c.Id == id); // Return the first club that has given ID
            return View(club);
        }
    }
}
