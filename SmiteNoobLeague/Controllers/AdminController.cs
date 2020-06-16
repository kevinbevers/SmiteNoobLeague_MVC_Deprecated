using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SNL_LogicLayer;

namespace SmiteNoobLeague.Controllers
{
    public class AdminController : Controller
    {
        private readonly LogicFactory _logicFactory;

        public AdminController(LogicFactory logicFactory)
        {
            _logicFactory = logicFactory;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}