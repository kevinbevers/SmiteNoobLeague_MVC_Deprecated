using System;
using System.Collections.Generic;
using System.Text;
using SNL_PersistenceLayer.Interfaces;
using SNL_PersistenceLayer.Entities;
using System.Linq.Expressions;
using System.Linq;

namespace SNL_PersistenceLayer.Repo
{
    public class TeamRepo : IRepository<TeamEntity>
    {
        private readonly SmiteNoobLeagueContext _context;
        public TeamRepo(SmiteNoobLeagueContext context)
        {
            _context = context; 
        }

        public void Add(TeamEntity entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TeamEntity> Find(Expression<Func<TeamEntity, bool>> predicate)
        {
            //get all the teams
            IEnumerable<TeamEntity> teamEntities = GetAll();  
            //peform the linq statement on it
            return teamEntities.AsQueryable().Where(predicate);
        }

        public IEnumerable<TeamEntity> GetAll()
        {
            List<TeamEntity> tl = new List<TeamEntity>();
            var data = _context.GetAll("team");
            var reader = data.CreateDataReader();
            //get the data from the dataset
            while (reader.Read())
            {
                TeamEntity team = new TeamEntity
                {
                    TeamID = reader["TeamID"] as int? ?? default,
                    TeamName = reader["TeamName"] as string ?? default,
                    TeamLogo = reader["TeamLogo"] as byte[] ?? default,
                    TeamDivisionID = reader["TeamDivisionID"] as int? ?? default,
                    TeamCaptainID = reader["TeamCaptainID"] as int? ?? default,
                    TeamMember2ID = reader["TeamMember2ID"] as int? ?? default,
                    TeamMember3ID = reader["TeamMember3ID"] as int? ?? default,
                    TeamMember4ID = reader["TeamMember4ID"] as int? ?? default,
                    TeamMember5ID = reader["TeamMember5ID"] as int? ?? default,
                };

                tl.Add(team);
            }

            return tl;
        }

        public TeamEntity GetByID(int id)
        {
            TeamEntity te = new TeamEntity();
            var data = _context.GetByID(id, "team");
            var reader = data.CreateDataReader();
            //get the data from the dataset
            while(reader.Read())
            {
                te = new TeamEntity
                {
                    TeamID = reader["TeamID"] as int? ?? default,
                    TeamName = reader["TeamName"] as string ?? default,
                    TeamLogo = reader["TeamLogo"] as byte[] ?? default,
                    TeamDivisionID = reader["TeamDivisionID"] as int? ?? default,
                    TeamCaptainID = reader["TeamCaptainID"] as int? ?? default,
                    TeamMember2ID = reader["TeamMember2ID"] as int? ?? default,
                    TeamMember3ID = reader["TeamMember3ID"] as int? ?? default,
                    TeamMember4ID = reader["TeamMember4ID"] as int? ?? default,
                    TeamMember5ID = reader["TeamMember5ID"] as int? ?? default,
                };
            }

            return te;
        }

        public void Remove(TeamEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
