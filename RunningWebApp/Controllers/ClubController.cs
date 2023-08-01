using Microsoft.AspNetCore.Mvc;
using RunningWebApp.Data;

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
            var clubs = _context.Clubs.ToList(); // Model
            return View(clubs); // View
        }
    }
}
