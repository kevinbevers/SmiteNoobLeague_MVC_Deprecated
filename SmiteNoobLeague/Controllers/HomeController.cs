﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmiteNoobLeague.Models;
using SNL_FactoryLayer;
using SNL_LogicLayer.ServiceInterfaces;

namespace SmiteNoobLeague.Controllers
{
    public class HomeController : Controller
    {
        private readonly  LogicFactory _logicFactory;

        public HomeController(LogicFactory logicFactory)
        {
            _logicFactory = logicFactory;
        }

        public IActionResult Index()
        {
            ITeamService TeamCol = _logicFactory.GetTeamService();

            var t = TeamCol.GetByID(1);
            var t2 = TeamCol.GetByID(2);

            t2.TeamName = "This is a new name";

            TeamCol.Update(t2);


            ViewBag.test = t.TeamName;

            return View();
        }
    }
}
