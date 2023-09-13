using Microsoft.EntityFrameworkCore;
using RunningWebApp.Data;
using RunningWebApp.Interfaces;
using RunningWebApp.Models;

namespace RunningWebApp.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly ApplicationDbContext _context;

		public UserRepository(ApplicationDbContext context)
		{
			this._context = context;
		}
		public bool Add(AppUser user)
		{
			throw new NotImplementedException();
		}

		public bool Delete(AppUser user)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<AppUser>> GetAllUsers()
		{
			return await _context.Users.ToListAsync(); // List of users
		}

		public async Task<AppUser> GetUserById(string id)
		{
			return await _context.Users.FindAsync(id); // Individual user
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}

		public bool Update(AppUser user)
		{
			_context.Update(user);
			return Save();
		}
	}
}
