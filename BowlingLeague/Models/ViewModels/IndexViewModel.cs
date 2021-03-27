using System;
using System.Collections;
using System.Collections.Generic;

namespace BowlingLeague.Models.ViewModels
{
    //This view model allows us to use multiple models in views
    public class IndexViewModel
    {
        public List<Bowler> Bowlers { get; set; }

        public PageNumbering PageNumbering { get; set; }

        public string TeamName { get; set; }
    }
}
