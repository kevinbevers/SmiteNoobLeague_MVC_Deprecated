using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using SNL_InterfaceLayer.DateTransferObjects;
using SNL_PersistenceLayer.Interfaces;
using SNL_InterfaceLayer.CustomExceptions;

namespace SNL_PersistenceLayer.Contexts
{
   
    public class TeamContext : IContext<TeamDTO>
    {
        private readonly ConnectionContext _con;
        public TeamContext(ConnectionContext con)
        {
            _con = con;
        }
        public IEnumerable<TeamDTO> GetAll()
        {
            try
            {
                List<TeamDTO> list = new List<TeamDTO>();

                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("select TeamID,TeamName,TeamLogo,TeamDivisionID,TeamCaptainID," +
                                                        "TeamMember2ID,TeamMember3ID,TeamMember4ID,TeamMember5ID  from teams", conn);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TeamDTO team = new TeamDTO
                            {
                                TeamID = reader.GetInt16(0),
                                TeamName = reader.GetString(1),
                                TeamLogo = (byte[])reader.GetValue(2),
                                TeamDivisionID = reader.GetInt16(3),
                                TeamCaptainID = reader.GetInt16(4),
                                TeamMember2ID = reader.GetInt16(5),
                                TeamMember3ID = reader.GetInt16(6),
                                TeamMember4ID = reader.GetInt16(7),
                                TeamMember5ID = reader.GetInt16(8),
                            };

                            list.Add(team);
                        }
                    }
                }
                return list;
            }
            catch(Exception ex)
            {
                throw new ContextErrorException("TeamContext.GetAll", ex.InnerException);
            }
        }
        public TeamDTO GetByID(int id)
        {
            try
            {
                TeamDTO team = new TeamDTO();

                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand($"select * from team where TeamID = ?id", conn);
                    cmd.Parameters.AddWithValue("id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            team.TeamID = reader["TeamID"] as int? ?? default;
                            team.TeamName = reader["TeamName"] as string ?? default;
                            team.TeamLogo = reader["TeamLogo"] as byte[] ?? default;
                            team.TeamDivisionID = reader["TeamDivisionID"] as int? ?? default;
                        }
                    }
                }
                return team;
            }
            catch(Exception ex)
            {
                throw new ContextErrorException("TeamContext.GetByID", ex.InnerException);
            }
        }
        public void Add(TeamDTO entity)
        {
            try 
            {
                throw new NotImplementedException();
            }
            catch(Exception ex)
            {
                throw new ContextErrorException("TeamContext.Add", ex.InnerException);
            }
        }
        public void Remove(TeamDTO entity)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                throw new ContextErrorException("TeamContext.Add", ex.InnerException);
            }
            throw new NotImplementedException();
        }

        public void Update(TeamDTO entity)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                throw new ContextErrorException("TeamContext.Add", ex.InnerException);
            }
        }
    }
}
