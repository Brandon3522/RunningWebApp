using Microsoft.AspNetCore.Mvc;
using RunningWebApp.Data;
using RunningWebApp.Models;

namespace RunningWebApp.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            this._context = context;
            this._httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Club>> GetAllUserClubs()
        {
            var currentUser = _httpContextAccessor.HttpContext?.User;
            var userClubs = _context.Clubs.Where(r => r.AppUser.Id == currentUser.ToString());
            return userClubs.ToList();
        }

        public async Task<List<Race>> GetAllUserRaces()
        {
            var currentUser = _httpContextAccessor.HttpContext?.User;
            var userRaces = _context.Races.Where(r => r.AppUser.Id == currentUser.ToString());
            return userRaces.ToList();
        }
    }
}
