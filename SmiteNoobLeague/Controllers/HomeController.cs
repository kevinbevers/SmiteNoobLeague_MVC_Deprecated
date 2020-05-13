using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmiteNoobLeague.Models;

namespace SmiteNoobLeague.Controllers
{
    public class HomeController : Controller
    {
        private readonly SmiteNoobLeagueContext _context;
        public HomeController(SmiteNoobLeagueContext context)
        {
            _context = context;           
        }

        public IActionResult Index()
        {
            ViewBag.teams = _context.GetAllTeams();

            return View();
        }
    }
}
