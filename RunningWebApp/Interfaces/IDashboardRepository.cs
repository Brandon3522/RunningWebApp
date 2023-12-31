﻿using Microsoft.AspNetCore.Mvc;
using RunningWebApp.Models;

namespace RunningWebApp.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Race>> GetAllUserRaces();
        Task<List<Club>> GetAllUserClubs();
        Task<AppUser> GetUserById(string id);
        Task<AppUser> GetByUserIdNoTracking(string id);
        bool Update(AppUser user);
        bool Save();
    }
}
