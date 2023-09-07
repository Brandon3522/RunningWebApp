using RunningWebApp.Interfaces;
using RunningWebApp.Models;

namespace RunningWebApp.Repositories
{
    public interface IDashboardRepository
    {
        Task<List<Race>> GetAllUserRaces();
        Task<List<Club>> GetAllUserClubs();
    }
}
