using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using SNL_LogicLayer.Interfaces;
using SNL_LogicLayer;
using SNL_PersistenceLayer.Contexts;
using SNL_InterfaceLayer.DateTransferObjects;
using SNL_LogicLayer.Models;

namespace SNL_LogicLayer.Collection
{
    public class TeamCollection : IDataObject<Team>
    {
        private readonly TeamContext _teamContext;
        private readonly PlayerContext _playerContext;

        public TeamCollection(TeamContext teamContext, PlayerContext playerContext)
        {
            _teamContext = teamContext;
            _playerContext = playerContext;
        }

        public void Add(Team entity)
        {
            TeamDTO tDTO = new TeamDTO
            {
                TeamID = entity.TeamID,
                TeamCaptainID = entity.TeamCaptain.PlayerID,
                TeamDivisionID = entity.TeamDivision.DivisionID,
                TeamLogo = entity.TeamLogo,
                TeamMember2ID = entity.TeamMembers[0].PlayerID,
                TeamMember3ID = entity.TeamMembers[1].PlayerID,
                TeamMember4ID = entity.TeamMembers[2].PlayerID,
                TeamMember5ID = entity.TeamMembers[3].PlayerID,
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
            //create list of team members excluding the captain
            List<Player> teamMembers = new List<Player>
            {
                new Player { PlayerID = tm2DTO.PlayerID, PlayerName = tm2DTO.PlayerName },
                new Player {  PlayerID = tm3DTO.PlayerID, PlayerName = tm3DTO.PlayerName },
                new Player {  PlayerID = tm4DTO.PlayerID, PlayerName = tm4DTO.PlayerName },
                new Player {  PlayerID = tm5DTO.PlayerID, PlayerName = tm5DTO.PlayerName },

            };
            //get player role with the role context

            Team t = new Team
            {
               TeamID = tDTO.TeamID,
               TeamName = tDTO.TeamName,
               TeamLogo = tDTO.TeamLogo,
               TeamCaptain = new Player {PlayerID = CaptainDTO.PlayerID, PlayerName = CaptainDTO.PlayerName},
               TeamMembers = teamMembers,
            };

            return t;
        }

        public void Remove(Team entity)
        {
            //only need primary key id to remove
            TeamDTO tDTO = new TeamDTO
            {
                TeamID = entity.TeamID,
                //TeamCaptainID = entity.TeamCaptain.PlayerID,
                //TeamDivisionID = entity.TeamDivision.DivisionID,
                //TeamLogo = entity.TeamLogo,
                //TeamMember2ID = entity.TeamMembers[0].PlayerID,
                //TeamMember3ID = entity.TeamMembers[1].PlayerID,
                //TeamMember4ID = entity.TeamMembers[2].PlayerID,
                //TeamMember5ID = entity.TeamMembers[3].PlayerID,
            };

            _teamContext.Remove(tDTO);
        }

        public void GetTeamRecentMatches()
        {

        }

        public void Update(Team entity)
        {
            TeamDTO tDTO = new TeamDTO
            {
                TeamID = entity.TeamID as int ? ?? default,
                TeamName = entity.TeamName as string ?? default,
                TeamCaptainID = entity.TeamCaptain.PlayerID as int? ?? default,
                TeamDivisionID = entity.TeamDivision.DivisionID as int? ?? default,
                TeamLogo = entity.TeamLogo as byte[] ?? default,
                TeamMember2ID = entity.TeamMembers[0].PlayerID as int? ?? default,
                TeamMember3ID = entity.TeamMembers[1].PlayerID as int? ?? default,
                TeamMember4ID = entity.TeamMembers[2].PlayerID as int? ?? default,
                TeamMember5ID = entity.TeamMembers[3].PlayerID as int? ?? default,
            };

            _teamContext.Update(tDTO);
        }
    }
}
