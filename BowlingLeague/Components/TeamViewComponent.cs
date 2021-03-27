using System;
using System.Linq;
using BowlingLeague.Models;
using Microsoft.AspNetCore.Mvc;

namespace BowlingLeague.Components
{
    //Allows you to work with the teams and the bowlers
    public class TeamViewComponent : ViewComponent
    {
        private BowlingLeagueContext _context;

        public TeamViewComponent (BowlingLeagueContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedTeamName = RouteData?.Values["TeamName"];

            return View(_context.Teams
                .Distinct()
                .OrderBy(x => x));
        }
    }
}
