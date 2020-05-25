using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using SNL_LogicLayer.Interfaces;
using SNL_LogicLayer.Models;
using SNL_PersistenceLayer.Repo;

namespace SNL_LogicLayer.WorkClasses
{
    public class Team : IDataObject<TeamModel>
    {
        private readonly TeamRepo _teamRepo;

        public Team(TeamRepo teamRepo)
        {
            _teamRepo = teamRepo;
        }

        public IEnumerable<TeamModel>  GetAll()
        {
            List<TeamModel> tl = new List<TeamModel>();
            var result = _teamRepo.GetAll();
            foreach (var data in result)
            {
                //recreate DTO
                TeamModel tm = new TeamModel
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
        public TeamModel GetByID(int id)
        {
            TeamModel tm = new TeamModel();        
            var data = _teamRepo.GetByID(id);
            //recreate DTO
            tm = new TeamModel {
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

        public IEnumerable<TeamModel> Find(Expression<Func<TeamModel, bool>> exp)
        {
            var result = GetAll();

            return result.AsQueryable().Where(exp);
        }
    }
}
