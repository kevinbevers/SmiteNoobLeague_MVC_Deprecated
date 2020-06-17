using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using SNL_LogicLayer;
using SNL_InterfaceLayer.DateTransferObjects;
using SNL_LogicLayer.Models;
using SNL_InterfaceLayer.Interfaces;
using SNL_LogicLayer.ServiceInterfaces;

namespace SNL_LogicLayer.Services
{
    public class TeamService : ITeamService
    {
        //bovenste gebruiken inteface
        private readonly ITeamContext _teamContext;
        private readonly IPlayerContext _playerContext;
        private readonly IRoleContext _roleContext;
        private readonly IDivisionContext _divisionContext;

        public TeamService(ITeamContext teamContext, IPlayerContext playerContext, IRoleContext roleContext, IDivisionContext divisionContext)
        {
            _teamContext = teamContext;
            _playerContext = playerContext;
            _roleContext = roleContext;
            _divisionContext = divisionContext;
        }

        //CRUD
        public void Add(Team entity)
        {
            TeamDTO tDTO = new TeamDTO
            {
                TeamID = entity.TeamID,
                TeamName = entity.TeamName,
                TeamCaptainID = entity.TeamCaptain?.PlayerID, //null propagation
                TeamDivisionID = entity.TeamDivision?.DivisionID, // null propagation
                TeamLogo = entity.TeamLogo,
                TeamMember2ID = entity.TeamMembers?.Count > 0 ? entity.TeamMembers[0].PlayerID : null,
                TeamMember3ID = entity.TeamMembers?.Count > 1 ? entity.TeamMembers[1].PlayerID : null,
                TeamMember4ID = entity.TeamMembers?.Count > 2 ? entity.TeamMembers[2].PlayerID : null,
                TeamMember5ID = entity.TeamMembers?.Count > 3 ? entity.TeamMembers[3].PlayerID : null,
            };
                _teamContext.Add(tDTO);
        }
        public IEnumerable<Team> GetAll()
        {
            IEnumerable<TeamDTO> tDTOList = _teamContext.GetAll();
            List<Team> teamList = new List<Team>();

            foreach(var tDTO in tDTOList)
            {
                Team t = new Team
                {
                    TeamID = tDTO.TeamID,
                    TeamName = tDTO.TeamName,
                    TeamLogo = tDTO.TeamLogo,
                };

                teamList.Add(t);
            }


            return teamList;
        }
        public Team GetByID(int id)
        {
            //team
            TeamDTO tDTO = _teamContext.GetByID(id);
            //team members
            PlayerDTO CaptainDTO = _playerContext.GetByID(tDTO.TeamCaptainID);
            PlayerDTO tm2DTO = _playerContext.GetByID(tDTO.TeamMember2ID);
            PlayerDTO tm3DTO = _playerContext.GetByID(tDTO.TeamMember3ID);
            PlayerDTO tm4DTO = _playerContext.GetByID(tDTO.TeamMember4ID);
            PlayerDTO tm5DTO = _playerContext.GetByID(tDTO.TeamMember5ID);
            //get player role with the role context
            var roles = _roleContext.GetAll();
            var cRole = roles.Where(i => i.RoleID == CaptainDTO.PlayerRoleID).FirstOrDefault() ?? new RoleDTO();
            var t2Role = roles.Where(i => i.RoleID == tm2DTO.PlayerRoleID).FirstOrDefault() ?? new RoleDTO();
            var t3Role = roles.Where(i => i.RoleID == tm3DTO.PlayerRoleID).FirstOrDefault() ?? new RoleDTO();
            var t4Role = roles.Where(i => i.RoleID == tm4DTO.PlayerRoleID).FirstOrDefault() ?? new RoleDTO();
            var t5Role = roles.Where(i => i.RoleID == tm5DTO.PlayerRoleID).FirstOrDefault() ?? new RoleDTO();
            //create list of team members excluding the captain
            List<Player> teamMembers = new List<Player>
            {
                new Player { PlayerID = tm2DTO.PlayerID, PlayerName = tm2DTO.PlayerName, PlayerRole = new Role {RoleID = t2Role.RoleID, RoleName = t2Role.RoleName, RoleDescription = t2Role.RoleDescription } },
                new Player {  PlayerID = tm3DTO.PlayerID, PlayerName = tm3DTO.PlayerName, PlayerRole = new Role {RoleID = t3Role.RoleID, RoleName = t3Role.RoleName, RoleDescription = t3Role.RoleDescription }},
                new Player {  PlayerID = tm4DTO.PlayerID, PlayerName = tm4DTO.PlayerName, PlayerRole = new Role {RoleID = t4Role.RoleID, RoleName = t4Role.RoleName, RoleDescription = t4Role.RoleDescription }},
                new Player {  PlayerID = tm5DTO.PlayerID, PlayerName = tm5DTO.PlayerName, PlayerRole = new Role {RoleID = t5Role.RoleID, RoleName = t5Role.RoleName, RoleDescription = t5Role.RoleDescription }},

            };
            //TeamDivision
            DivisionDTO div = _divisionContext.GetByID(tDTO.TeamDivisionID); //could do something with the division
           

            Team t = new Team
            {
                TeamID = tDTO.TeamID,
                TeamName = tDTO.TeamName,
                TeamLogo = tDTO.TeamLogo,
                TeamCaptain = new Player { PlayerID = CaptainDTO.PlayerID, PlayerName = CaptainDTO.PlayerName, PlayerRole = new Role { RoleID = cRole.RoleID, RoleName = cRole.RoleName, RoleDescription = cRole.RoleDescription }},
                TeamMembers = teamMembers,
                TeamDivision = new Division { DivisionID = div.DivisionID, DivisionName = div.DivisionName },
            };

            return t;
        }
        public void Remove(Team entity)
        {
            //only need primary key id to remove
            TeamDTO tDTO = new TeamDTO
            {
                TeamID = entity.TeamID,
            };

            _teamContext.Remove(tDTO);
        }
        public void Update(Team entity)
        {
            TeamDTO tDTO = new TeamDTO
            {
                TeamID = entity.TeamID, //normal values are nullable
                TeamName = entity.TeamName,
                TeamCaptainID = entity.TeamCaptain?.PlayerID, //null propagation because this comes from an object
                TeamDivisionID = entity.TeamDivision?.DivisionID, // null propagation
                TeamLogo = entity.TeamLogo,
                TeamMember2ID = entity.TeamMembers?.Count > 0 ? entity.TeamMembers[0].PlayerID : null,
                TeamMember3ID = entity.TeamMembers?.Count > 1 ? entity.TeamMembers[1].PlayerID : null,
                TeamMember4ID = entity.TeamMembers?.Count > 2 ? entity.TeamMembers[2].PlayerID : null,
                TeamMember5ID = entity.TeamMembers?.Count > 3 ? entity.TeamMembers[3].PlayerID : null,
            };

            _teamContext.Update(tDTO);
        }
        //Extra business logic
        public void GetTeamRecentMatches()
        {

        }
    }
}
