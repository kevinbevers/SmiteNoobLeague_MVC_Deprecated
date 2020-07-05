using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SNL_FactoryLayer;
using SmiteNoobLeague.Models.AdminPageViews.TeamViewModels;
using SNL_LogicLayer.Models;
using Microsoft.Extensions.Logging;
using SmiteNoobLeague.HelperClasses;
using Microsoft.AspNetCore.Hosting;
using System.Drawing;
using SmiteNoobLeague.Models.AdminPageViews;

namespace SmiteNoobLeague.Controllers
{
    public class AdminController : Controller
    {
        private readonly LogicFactory _logicFactory;
        private readonly ILogger<AdminController> _logger;
        private readonly IWebHostEnvironment _env;

        public AdminController(LogicFactory logicFactory, ILogger<AdminController> logger, IWebHostEnvironment env)
        {
            _logicFactory = logicFactory;
            _logger = logger;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }
        #region CreateAccount 
        [HttpGet]
        [AjaxOnly]
        public IActionResult CreateAccount()
        {
            //return PartialView("_CreateTeamFormPartial");
            return PartialView("Account/_CreateAccountPartial");
        }
        [HttpPost]
        [AjaxOnly]
        public IActionResult CreateAccount(AdminAccountCreateView model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    //save account to DB
                    var accountService = _logicFactory.GetAccountService();

                    accountService.Add(new Account
                    {
                        AccountName = model.AccountName,
                        AccountEmail = model.AccountEmail,
                        AccountPassword = model.AccountPassword,
                        AccountPlayer = new Player {PlayerID = model.PlayerID, PlayerName = model.PlayerName, PlayerPlatformID = model.PlayerPlatformID }
                    });
                    //return success
                    return PartialView("Account/_AccountSuccess");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Something went wrong trying to add a new account to the database. |Message: {ex.Message} |Stacktrace: {ex.StackTrace}");
                    return NotFound();
                    //return errorpage
                }
            }
            else
            {
                return PartialView("Account/_CreateAccountPartial", model);
            }
        }
        #region TakenValidationAccount
        public IActionResult UserNameTaken(string AccountName)
        {
            var accountService = _logicFactory.GetAccountService();

            if (!accountService.UserNameTaken(AccountName))
            {
                return Json($"Accountname '{AccountName}' is already taken.");
            }

            return Json(true);
        }
        public IActionResult EmailTaken(string AccountEmail)
        {
            var accountService = _logicFactory.GetAccountService();

            if (!accountService.EmailTaken(AccountEmail))
            {
                return Json($"e-mailaddress '{AccountEmail}' is already taken.");
            }

            return Json(true);
        }
        #endregion
        #endregion

        #region CreateTeam
        #region viewCreation
        [HttpGet]
        [AjaxOnly]
        public IActionResult CreateTeam()
        {
            //return PartialView("_CreateTeamFormPartial");
            return PartialView("Team/_CreateBasicTeamFormPartial");
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        [AjaxOnly]
        public IActionResult CreateTeamWithMembers(AdminTeamBasicCreateView model)
        {
            AdminTeamCreateView newModel = new AdminTeamCreateView { TeamID = model.TeamID, TeamCaptainID = model.TeamCaptainID, TeamLogoFile = model.TeamLogoFile, TeamName = model.TeamName };
            //return PartialView("_CreateTeamFormPartial");
            return PartialView("Team/_CreateTeamFormPartial", newModel);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        [AjaxOnly]
        public IActionResult CreateTeamWithoutMembers(AdminTeamCreateView model)
        {
            AdminTeamBasicCreateView newModel = model;
            //return PartialView("_CreateTeamFormPartial");
            return PartialView("Team/_CreateBasicTeamFormPartial", newModel);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        [AjaxOnly]
        #endregion
        public async Task<IActionResult> CreateTeamModel(AdminTeamCreateView model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var path = _env.WebRootFileProvider.GetFileInfo("/Images/teamBadge.png")?.PhysicalPath;
                    //before adding should check if player is already in a team
                    //check teamname
                    var teamService = _logicFactory.GetTeamService();
                    //basic info about team. this is always filled in when adding
                    Team t = new Team
                    {
                        TeamName = model.TeamName,
                        TeamCaptain = new Player { PlayerID = model.TeamCaptainID },
                        TeamMembers = new List<Player> { new Player { PlayerID = model.TeamMember2ID },
                                                    new Player { PlayerID = model.TeamMember3ID },
                                                    new Player { PlayerID = model.TeamMember4ID },
                                                    new Player { PlayerID = model.TeamMember5ID }},
                        TeamLogo = model.TeamLogoFile != null ? await ImageProcessing.FormFileToResizedByteArrayAsync(model.TeamLogoFile) : ImageProcessing.ImageToByteArray(Image.FromFile(path))
                    };
                    teamService.Add(t);

                    return PartialView("Team/_Success");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Something went wrong trying to add a new team to the database. |Message: {ex.Message} |Stacktrace: {ex.StackTrace}");
                    return PartialView("_Failed");
                }
            }
            else
            {
                return PartialView("Team/_CreateTeamFormPartial", model);
            }
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> CreateTeamBasicModel(AdminTeamBasicCreateView model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var path = _env.WebRootFileProvider.GetFileInfo("/Images/teamBadge.png")?.PhysicalPath;
                    //before adding should check if player is already in a team
                    //check teamname
                    var teamService = _logicFactory.GetTeamService();
                    //basic info about team. this is always filled in when adding
                    Team t = new Team();
                    t.TeamName = model.TeamName;
                    t.TeamCaptain = new Player { PlayerID = model.TeamCaptainID, PlayerName = "test captain" };
                    t.TeamLogo = model.TeamLogoFile != null ? await ImageProcessing.FormFileToResizedByteArrayAsync(model.TeamLogoFile) : ImageProcessing.ImageToByteArray(Image.FromFile(path));

                    teamService.Add(t);

                    return PartialView("Team/_Success");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Something went wrong trying to add a new team to the database. |Message: {ex.Message} |Stacktrace: {ex.StackTrace}");
                    return PartialView("Team/_Failed");
                }
            }
            else
            {
                return PartialView("Team/_CreateTeamFormPartial", model);
            }
        }
        #endregion CreateTeam
        #region ManageTeam
        [HttpGet]
        //custom ajax only extension
        [AjaxOnly]
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
                return PartialView("Team/_ManageTeamListPartial", model);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong trying to get all teams for the manage modal. |Message: {ex.Message} |Stacktrace: {ex.StackTrace}");
                //notfound will result in a ajax error result. this will show a message to the user.
                return NotFound();
            }
        }
        [HttpPost]
        [AjaxOnly]
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

                return PartialView("Team/_ManageTeamListPartial", model);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong trying to delete a team to the database. |Message: {ex.Message} |Stacktrace: {ex.StackTrace}");
                //notfound will result in a ajax error result. this will show a message to the user. 
                //could also return a partial view with a script that runs to show the messagebox. this feels cleaner. 
                //i couldn't find a way to send a Json result with partial view and success yes or no
                return NotFound();
            }
        }
        [HttpGet]
        [AjaxOnly]
        public IActionResult EditGetTeam(int id)
        {
            try
            {
                var teamService = _logicFactory.GetTeamService();
                //get the team
                Team t = teamService.GetByID(id);
                //create the view model
                AdminBasicTeamEditView editTeamView = new AdminBasicTeamEditView
                {
                    TeamID = t.TeamID,
                    TeamName = t.TeamName,
                    TeamCaptainID = t.TeamCaptain?.PlayerID,
                    //convert image
                    TeamLogoString64 = ImageProcessing.ByteArrayToString64(t.TeamLogo),
                };



                return PartialView("Team/_EditBasicTeamFormPartial", editTeamView);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong trying to get a team to edit. |Message: {ex.Message} |Stacktrace: {ex.StackTrace}");
                //notfound will result in a ajax error result. this will show a message to the user. 
                return NotFound();
            }
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> EditGetDetailedTeam(AdminBasicTeamEditView model)
        {
            try
            {
                var teamService = _logicFactory.GetTeamService();
                //get the team
                Team t = teamService.GetByID((int)model.TeamID);
                //create the view model
                AdminTeamEditView editTeamView = new AdminTeamEditView
                {
                    TeamID = model.TeamID,
                    TeamName = model.TeamName,
                    TeamCaptainID = model.TeamCaptainID,
                    TeamLogoString64 = model.TeamLogoFile != null ? ImageProcessing.ByteArrayToString64(await ImageProcessing.FormFileToResizedByteArrayAsync(model.TeamLogoFile)) : model.TeamLogoString64,
                    TeamLogoFile = model.TeamLogoFile,
                    TeamMember2ID = t.TeamMembers[0].PlayerID,
                    TeamMember2Name = t.TeamMembers[0].PlayerName,
                    TeamMember3ID = t.TeamMembers[1].PlayerID,
                    TeamMember3Name = t.TeamMembers[1].PlayerName,
                    TeamMember4ID = t.TeamMembers[2].PlayerID,
                    TeamMember4Name = t.TeamMembers[2].PlayerName,
                    TeamMember5ID = t.TeamMembers[3].PlayerID,
                    TeamMember5Name = t.TeamMembers[3].PlayerName,
                };

                return PartialView("Team/_EditTeamFormPartial", editTeamView);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong trying to get a team to edit. |Message: {ex.Message} |Stacktrace: {ex.StackTrace}");
                //notfound will result in a ajax error result. this will show a message to the user. 
                return NotFound();
            }
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> EditGetBasicTeam(AdminTeamEditView model)
        {
            try
            {
                //create the view model
                AdminBasicTeamEditView editTeamView = new AdminBasicTeamEditView
                {
                    TeamID = model.TeamID,
                    TeamName = model.TeamName,
                    TeamCaptainID = model.TeamCaptainID,
                    //convert image
                    TeamLogoString64 = model.TeamLogoFile != null ? ImageProcessing.ByteArrayToString64(await ImageProcessing.FormFileToResizedByteArrayAsync(model.TeamLogoFile)) : model.TeamLogoString64,
                    TeamLogoFile = model.TeamLogoFile,
                };



                return PartialView("Team/_EditBasicTeamFormPartial", editTeamView);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong trying to get a team to edit. |Message: {ex.Message} |Stacktrace: {ex.StackTrace}");
                //notfound will result in a ajax error result. this will show a message to the user. 
                return NotFound();
            }
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> EditTeam(AdminTeamEditView model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var teamService = _logicFactory.GetTeamService();

                    Team teamToEdit = new Team
                    {
                        TeamID = model.TeamID,
                        TeamName = model.TeamName,
                        TeamCaptain = new Player { PlayerID = model.TeamCaptainID },
                        TeamMembers = new List<Player> { new Player { PlayerID = model.TeamMember2ID, PlayerName = model.TeamMember2Name },
                                                       new Player { PlayerID = model.TeamMember3ID, PlayerName = model.TeamMember3Name },
                                                       new Player { PlayerID = model.TeamMember4ID, PlayerName = model.TeamMember4Name },
                                                       new Player { PlayerID = model.TeamMember5ID, PlayerName = model.TeamMember5Name }},
                    };
                    byte[] img = await ImageProcessing.FormFileToResizedByteArrayAsync(model.TeamLogoFile);
                    teamToEdit.TeamLogo = img == default ? teamService.GetByID((int)model.TeamID).TeamLogo : teamToEdit.TeamLogo = img;

                    teamService.Update(teamToEdit);

                    return PartialView("Team/_EditSuccess"); //success
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Something went wrong trying to get a team to edit. |Message: {ex.Message} |Stacktrace: {ex.StackTrace}");
                    //notfound will result in a ajax error result. this will show a message to the user. 
                    return NotFound();
                }
            }
            else
            {
                return PartialView("_EditTeamFormPartial", model);
            }

        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        [AjaxOnly]
        public async Task<IActionResult> EditBasicTeam(AdminBasicTeamEditView model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var teamService = _logicFactory.GetTeamService();
                    byte[] img = await ImageProcessing.FormFileToResizedByteArrayAsync(model.TeamLogoFile);

                    teamService.Update(new Team
                    {
                        TeamID = model.TeamID,
                        TeamName = model.TeamName,
                        TeamCaptain = new Player { PlayerID = model.TeamCaptainID, },
                        TeamLogo = img == default ? teamService.GetByID((int)model.TeamID).TeamLogo : img,
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Something went wrong trying to get a team to edit. |Message: {ex.Message} |Stacktrace: {ex.StackTrace}");
                    //notfound will result in a ajax error result. this will show a message to the user. 
                    return NotFound();
                }

                return PartialView("Team/_EditSuccess"); //success
            }
            else
            {
                return PartialView("Team/_EditTeamFormPartial", model);
            }

        }
        #endregion
        #region TakenValidationTeams
        public IActionResult CheckTeamNameTaken(string teamname)
        {
            var teamService = _logicFactory.GetTeamService();

            //return taken or not taken, javascript will handle accordingly
            return Json(new { success = teamService.TeamNameAvailable(teamname) });
        }
        public IActionResult TeamNameTakenEdit(string TeamName, int? TeamID)
        {
            var teamService = _logicFactory.GetTeamService();

            if (TeamID != null)
            {
                if (teamService.GetByID((int)TeamID)?.TeamName == TeamName)
                {
                    return Json(true);
                }

            }

            if (!teamService.TeamNameAvailable(TeamName))
            {
                return Json($"Teamname '{TeamName}' is already in use.");
            }

            return Json(true);
        }
        public IActionResult TeamNameTaken(string TeamName)
        {
            var teamService = _logicFactory.GetTeamService();

            if (!teamService.TeamNameAvailable(TeamName))
            {
                return Json($"Teamname '{TeamName}' is already in use.");
            }

            return Json(true);
        }
        public IActionResult CaptainTakenEdit(int TeamCaptainID, int? TeamID)
        {
            var teamService = _logicFactory.GetTeamService();


            if (TeamID != null)
            {
                if (teamService.GetByID((int)TeamID)?.TeamCaptain?.PlayerID == TeamCaptainID)
                {
                    return Json(true);
                }

            }


            if (!teamService.CaptainAvailable(TeamCaptainID))
            {
                return Json($"This Captain is already linked to a team.");
            }

            return Json(true);
        }
        public IActionResult CaptainTaken(int TeamCaptainID)
        {
            var teamService = _logicFactory.GetTeamService();

            if (!teamService.CaptainAvailable(TeamCaptainID))
            {
                return Json($"This Captain is already linked to a team.");
            }

            return Json(true);
        }
        #endregion

    }
}