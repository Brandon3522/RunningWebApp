using RunningWebApp.Models;

namespace RunningWebApp.Interfaces
{
	public interface IClubRepository
	{
		Task<IEnumerable<Club>> GetAll();
		Task<Club> GetByIdAsync(int id);
		Task<IEnumerable<Club>> GetClubByCity(string city);
		bool Add(Club club);
		bool update(Club club);
		bool Delete(Club club);
		bool Save();
	}
}
