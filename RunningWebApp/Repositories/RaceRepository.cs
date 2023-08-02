using Microsoft.EntityFrameworkCore;
using RunningWebApp.Data;
using RunningWebApp.Interfaces;
using RunningWebApp.Models;

namespace RunningWebApp.Repositories
{
	public class RaceRepository : IRaceRepository
	{
		private readonly ApplicationDbContext _context;

		public RaceRepository(ApplicationDbContext context)
		{
			this._context = context;
		}
		public bool Add(Race race)
		{
			_context.Add(race);
			return Save();
		}

		public bool Delete(Race race)
		{
			_context.Remove(race);
			return Save();
		}

		public async Task<IEnumerable<Race>> GetAll()
		{
			return await _context.Races.ToListAsync();
		}

		public async Task<IEnumerable<Race>> GetAllRacesByCity(string city)
		{
			return await _context.Races.Where(r => r.Address.City.Contains(city)).ToListAsync();
		}

		public async Task<Race> GetByIdAsync(int id)
		{
			return await _context.Races.Include(i => i.Address).FirstOrDefaultAsync(i => i.Id == id);
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}

		public bool update(Race race)
		{
			_context.Update(race);
			return Save();
		}
	}
}
