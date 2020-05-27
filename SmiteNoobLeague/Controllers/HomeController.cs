using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SNL_LogicLayer;
using SmiteNoobLeague.Models;

namespace SmiteNoobLeague.Controllers
{
    public class HomeController : Controller
    {
        private readonly LogicFactory _logicFactory;

        public HomeController(LogicFactory logicFactory)
        {
            _logicFactory = logicFactory;
        }

        public IActionResult Index()
        {
            var TeamCol = _logicFactory.GetTeamCollection();

            var t = TeamCol.GetByID(1);

            ViewBag.test = t.TeamName;

            return View();
        }
    }
}
