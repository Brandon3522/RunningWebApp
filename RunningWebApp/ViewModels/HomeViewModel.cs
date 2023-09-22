﻿using RunningWebApp.Models;

namespace RunningWebApp.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Club> Clubs { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        //public IEnumerable<Race> Races { get; set; }
    }
}
