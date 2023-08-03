﻿using Microsoft.EntityFrameworkCore;
using RunningWebApp.Models;

namespace RunningWebApp.Data
{
    // Allows interaction with database
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
            
        }

        // Database tables
        public DbSet<Race> Races { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}