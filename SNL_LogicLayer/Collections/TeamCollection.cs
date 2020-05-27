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

        public TeamCollection(TeamContext teamContext)
        {
            _teamContext = teamContext;
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
            TeamDTO tDTO = _teamContext.GetByID(id);

            Team t = new Team
            {
               TeamID = tDTO.TeamID,
               TeamName = tDTO.TeamName,
               TeamLogo = tDTO.TeamLogo,

            };

            return t;
        }

        public void Remove(Team entity)
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

            _teamContext.Remove(tDTO);
        }

        public void GetTeamRecentMatches()
        {

        }

        public void Update(Team entity)
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

            _teamContext.Update(tDTO);
        }
    }
}
