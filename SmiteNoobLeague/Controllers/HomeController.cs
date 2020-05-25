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
        private readonly UnitOfWork _unitofwork;

        public HomeController(UnitOfWork unitOfWork)
        {
            _unitofwork = unitOfWork;
        }

        public IActionResult Index()
        {
            var test = _unitofwork.Team.GetByID(1);

            ViewBag.test = test.TeamName;

            return View();
        }
    }
}
