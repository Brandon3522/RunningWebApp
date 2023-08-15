using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using RunningWebApp.Data;
using RunningWebApp.Interfaces;
using RunningWebApp.Models;
using System.Diagnostics;

namespace RunningWebApp.Repositories
{
	public class ClubRepository : IClubRepository
	{
		private readonly ApplicationDbContext _context;

		// ApplicationDbContext == database 
		public ClubRepository(ApplicationDbContext context)
		{
			this._context = context;
		}
		public bool Add(Club club)
		{
			_context.Add(club); // Generates SQL
			return Save(); // Sends SQL to database and creates entity
		}

		public bool Delete(Club club)
		{
			_context.Remove(club);
			return Save();
		}

		public async Task<IEnumerable<Club>> GetAll()
		{
			return await _context.Clubs.ToListAsync();
		}

		public async Task<Club> GetByIdAsync(int id)
		{
			return await _context.Clubs.Include(i => i.Address).FirstOrDefaultAsync(i => i.Id == id);
		}

		public async Task<Club> GetByIdAsyncNoTracking(int id)
		{
			return await _context.Clubs.Include(i => i.Address).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
		}

		public async Task<IEnumerable<Club>> GetClubByCity(string city)
		{
			// Search 
			return await _context.Clubs.Where(c => c.Address.City.Contains(city)).ToListAsync();
		}

		public bool Save()
		{
			var saved = _context.SaveChanges(); // Returns 0 or 1
			return saved > 0 ? true : false; // Change return to boolean
		}

		public bool Update(Club club)
		{
			_context.Update(club);
			return Save();
		}
	}
}
