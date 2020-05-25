using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using SNL_LogicLayer.Interfaces;
using SNL_LogicLayer.Models;
using SNL_PersistenceLayer.Repo;

namespace SNL_LogicLayer.Collection
{
    public class TeamCollection : IDataObject<Team>
    {
        private readonly TeamRepo _teamRepo;

        public TeamCollection(TeamRepo teamRepo)
        {
            _teamRepo = teamRepo;
        }
        public IEnumerable<Team> GetAll()
        {
            List<Team> tl = new List<Team>();
            var result = _teamRepo.GetAll();
            foreach (var data in result)
            {
                //recreate DTO
                Team tm = new Team
                {
                    TeamID = data.TeamID,
                    TeamName = data.TeamName,
                    TeamLogo = data.TeamLogo,
                    TeamCaptainID = data.TeamCaptainID,
                    TeamDivisionID = data.TeamDivisionID,
                    TeamMember2ID = data.TeamMember2ID,
                    TeamMember3ID = data.TeamMember3ID,
                    TeamMember4ID = data.TeamMember4ID,
                    TeamMember5ID = data.TeamMember5ID,
                };
                tl.Add(tm);
            }
            return tl;
        }
        public Team GetByID(int id)
        {
            Team tm = new Team();        
            var data = _teamRepo.GetByID(id);
            //recreate DTO
            tm = new Team {
                TeamID = data.TeamID,
                TeamName = data.TeamName,
                TeamLogo = data.TeamLogo,
                TeamCaptainID = data.TeamCaptainID,
                TeamDivisionID = data.TeamDivisionID,
                TeamMember2ID = data.TeamMember2ID,
                TeamMember3ID = data.TeamMember3ID,
                TeamMember4ID = data.TeamMember4ID,
                TeamMember5ID = data.TeamMember5ID,
            };
            return tm;
        }
    }
}
