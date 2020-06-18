using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SNL_FactoryLayer;
using SmiteNoobLeague.Models.AdminPageViews.TeamViewModels;
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

        #region CreateTeam
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

                    //basic info about team. this is always filled in when adding
                    Team t = new Team();
                    t.TeamName = model.basic.TeamName;
                    //t.TeamCaptain = new Player {PlayerID = model.basic.TeamCaptainID };

                    //team members can be chosen to be toggled and not filled in in the view
                    if (model.members != null)
                    {
                        t.TeamMembers = new List<Player> { new Player { PlayerID = model.members.TeamMember2ID },
                                                       new Player { PlayerID = model.members.TeamMember3ID },
                                                       new Player { PlayerID = model.members.TeamMember4ID },
                                                       new Player { PlayerID = model.members.TeamMember5ID }};
                    }

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
        #endregion CreateTeam

        #region ManageTeam
        public IActionResult ManageTeam()
        {
            try
            {
                var teamservice = _logicFactory.GetTeamService();
                var allTeams = teamservice.GetAll();
                AdminManageTeamListView model = new AdminManageTeamListView();
                model.TeamList = new List<TeamListView>();
                foreach (Team team in allTeams)
                {
                    model.TeamList.Add(new TeamListView
                    {
                        Teamname = team.TeamName,
                        TeamcaptainName = team.TeamCaptain?.PlayerName,
                        TeamID = team.TeamID
                    });
                }
                return PartialView("_ManageTeamListPartial", model);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong trying to get all teams for the manage modal. |Message: {ex.Message} |Stacktrace: {ex.StackTrace}");
                //notfound will result in a ajax error result. this will show a message to the user.
                return NotFound();
            }
        }

        public IActionResult DeleteTeam(int id)
        {
            try
            {
                var teamservice = _logicFactory.GetTeamService();
                //delete the team
                teamservice.Remove(new Team { TeamID = id });
                //get all the teams that remain to update the view
                var allTeams = teamservice.GetAll();
                AdminManageTeamListView model = new AdminManageTeamListView();
                model.TeamList = new List<TeamListView>();
                foreach (Team team in allTeams)
                {
                    model.TeamList.Add(new TeamListView
                    {
                        Teamname = team.TeamName,
                        TeamcaptainName = team.TeamCaptain?.PlayerName,
                        TeamID = team.TeamID
                    });
                }

                return PartialView("_ManageTeamListPartial", model);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong trying to delete a team to the database. |Message: {ex.Message} |Stacktrace: {ex.StackTrace}");
                //notfound will result in a ajax error result. this will show a message to the user. 
                //could also return a partial view with a script that runs to show the messagebox. this feels cleaner. 
                //i couldn't find a way to send a Json result with partial view and success yes or no
                return NotFound();
            }
        }
        #endregion
        public IActionResult CheckTeamNameTaken(string teamname)
        {

            //teamservice.NameAvailable(); function

            //return taken or not taken, javascript will handle accordingly
            return Json(new { success = true });
        }

    }
}