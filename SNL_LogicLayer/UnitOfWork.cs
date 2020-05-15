using System;
using System.Collections.Generic;
using System.Text;
using SNL_PersistenceLayer;
using SNL_LogicLayer.Models;

namespace SNL_LogicLayer
{
    public class UnitOfWork
    {
        private readonly SmiteNoobLeagueContext _context;
        public UnitOfWork(SmiteNoobLeagueContext context)
        {
            _context = context;
        }

        public Team GetTeamByID(int id)
        {
            var data = _context.GetTeam(id);
            Team team = new Team
            {
                TeamID = data.TeamID, 
                TeamName = data.TeamName, 
                TeamLogo = data.TeamLogo, 
                TeamCaptainID = data.TeamCaptainID, 
                TeamDivisionID = data.TeamDivisionID,
                TeamMember2ID = data.TeamMember2ID,
                TeamMember3ID = data.TeamMember3ID,
                TeamMember4ID = data.TeamMember4ID,
                TeamMember5ID = data.TeamMember5ID
            };
            return team;
        }

        public List<Team> GetAllTeams()
        {
            List<Team> teamlist = new List<Team>();
            var dbdata = _context.GetAllTeams();
            foreach(var data in dbdata)
                teamlist.Add(new Team
                {
                    TeamID = data.TeamID,
                    TeamName = data.TeamName,
                    TeamLogo = data.TeamLogo,
                    TeamCaptainID = data.TeamCaptainID,
                    TeamDivisionID = data.TeamDivisionID,
                    TeamMember2ID = data.TeamMember2ID,
                    TeamMember3ID = data.TeamMember3ID,
                    TeamMember4ID = data.TeamMember4ID,
                    TeamMember5ID = data.TeamMember5ID
                });


            return teamlist;
        }
    }
}
