using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SNL_FactoryLayer;
using SmiteNoobLeague.Models.AdminPageViews;
using SNL_LogicLayer.Models;
using Microsoft.Extensions.Logging;

namespace SmiteNoobLeague.Controllers
{
    public class AdminController : Controller
    {
        private readonly LogicFactory _logicFactory;
        private readonly ILogger<AdminController> _logger;

        public AdminController(LogicFactory logicFactory, ILogger<AdminController> logger)
        {
            _logicFactory = logicFactory;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateTeam()
        {
            return PartialView("_CreateTeamFormPartial");
        }

        public IActionResult CreateTeamModel(AdminTeamCreateView model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    //before adding should check if player is already in a team
                    //check teamname
                    var teamService = _logicFactory.GetTeamService();
                    Team t = new Team();
                    t.TeamName = model.TeamName;
                    t.TeamCaptain = new Player {PlayerID = model.TeamCaptainID };
                    t.TeamMembers = new List<Player> { new Player { PlayerID = model.TeamMember2ID },
                                                       new Player { PlayerID = model.TeamMember3ID },
                                                       new Player { PlayerID = model.TeamMember4ID },
                                                       new Player { PlayerID = model.TeamMember5ID }};


                    teamService.Add(t);

                    return PartialView("_Success");
                }
                catch(Exception ex)
                {
                    _logger.LogError($"Something went wrong trying to add a new team to the database. |Message: {ex.Message} |Stacktrace: {ex.StackTrace}");
                    return PartialView("_Failed");
                }



            }
            else
            {
                return PartialView("_CreateTeamFormPartial", model);              
            }

            
        }
    }
}