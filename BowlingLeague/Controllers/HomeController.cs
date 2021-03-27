using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BowlingLeague.Models;
using Microsoft.EntityFrameworkCore;
using BowlingLeague.Models.ViewModels;

namespace BowlingLeague.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //Sets up the context to work with the database
        private BowlingLeagueContext _context { get; set; }

        public HomeController(ILogger<HomeController> logger, BowlingLeagueContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(long? teamId, string teamName, int pageNum)
        {
            //Sets the page size to 5
            int pageSize = 5;

            //Uses the Index view model to output the bowlers, page numbering, and team name info
            return View(new IndexViewModel
            {
                Bowlers = (_context.Bowlers
                    .Where(x => x.TeamId == teamId || teamId == null)
                    .OrderBy(x => x.BowlerFirstName)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize)
                    .ToList()),
                PageNumbering = new PageNumbering
                {
                    NumItemsPerPage = pageSize,
                    CurrentPage = pageNum,
                    TotalItems = (teamId == null ? _context.Bowlers.Count() :
                        _context.Bowlers.Where(x => x.TeamId == teamId).Count())
                },

                TeamName = teamName
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
