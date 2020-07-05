using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmiteNoobLeague.Models;
//Architecture usings, factory, service inteface, logic models
using SNL_FactoryLayer;
using SNL_LogicLayer.ServiceInterfaces;
using SNL_LogicLayer.Models;
using SNL_InterfaceLayer.CustomExceptions;

namespace SmiteNoobLeague.Controllers
{
    public class HomeController : Controller
    {
        private readonly LogicFactory _logicFactory;
        private readonly ILogger<HomeController> _logger;

        public HomeController(LogicFactory logicFactory, ILogger<HomeController> logger)
        {
            _logicFactory = logicFactory;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
