﻿using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using RunningWebApp.Data;
using RunningWebApp.Data.Enums;
using RunningWebApp.Models;
using RunningWebApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningWebApp.tests.RepositoryTests
{
    public class ClubRepositoryTests
    {
        private async Task<ApplicationDbContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new ApplicationDbContext(options);

            databaseContext.Database.EnsureCreated();

            if (await databaseContext.Clubs.CountAsync() > 0)
            {
                // Add seed data to the database
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.Clubs.Add(
                    new Club()
                    {
                        Title = "Running Club 1",
                        Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                        Description = "This is the description of the first cinema",
                        ClubCategory = ClubCategory.City,
                        Address = new Address()
                        {
                            Street = "123 Main St",
                            City = "Portland",
                            State = "OR"
                        }
                    });

                    await databaseContext.SaveChangesAsync();
                }
                
            }
            return databaseContext;
        }

        [Fact]
        public async void ClubRepository_Add_ReturnsBool()
        {
            // Arrange
            var club = new Club()
            {
                Title = "Running Club 1",
                Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                Description = "This is the description of the first cinema",
                ClubCategory = ClubCategory.City,
                Address = new Address()
                {
                    Street = "123 Main St",
                    City = "Portland",
                    State = "OR"
                }
            };

            var dbContext = await GetDbContext();
            var clubRepository = new ClubRepository(dbContext); // Should be initialized in constructor

            // Act
            var result = clubRepository.Add(club);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void ClubRepository_GetByIdAsync_ReturnsClub()
        {
            // Arrange
            var id = 1;
            var dbContext = await GetDbContext();
            var clubRepository = new ClubRepository(dbContext);

            // Act
            var result = clubRepository.GetByIdAsync(1);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<Club>>();
        }
    }
}
