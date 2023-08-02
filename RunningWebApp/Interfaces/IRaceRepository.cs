﻿using RunningWebApp.Models;

namespace RunningWebApp.Interfaces
{
	public interface IRaceRepository
	{
		Task<IEnumerable<Race>> GetAll();
		Task<Race> GetByIdAsync(int id);
		Task<IEnumerable<Race>> GetAllRacesByCity(string city);
		bool Add(Race race);
		bool update(Race race);
		bool Delete(Race race);
		bool Save();
	}
}
