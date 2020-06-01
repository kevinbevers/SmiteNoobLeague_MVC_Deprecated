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
                                TeamID = reader[0] as int? ?? default,
                                TeamName = reader[1] as string ?? default,
                                TeamLogo = reader[2] as byte[] ?? default,
                                TeamDivisionID = reader[3] as int? ?? default,
                                TeamCaptainID = reader[4] as int? ?? default,
                                TeamMember2ID = reader[5] as int? ?? default,
                                TeamMember3ID = reader[6] as int? ?? default,
                                TeamMember4ID = reader[7] as int? ?? default,
                                TeamMember5ID = reader[8] as int? ?? default,
                        };

                            list.Add(team);
                        }
                    }
                }
                return list;
            }
            catch(Exception ex)
            {
                throw new ContextErrorException("TeamContext.GetAll", ex);
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
                    MySqlCommand cmd = new MySqlCommand("select TeamID,TeamName,TeamLogo,TeamDivisionID,TeamCaptainID," +
                                                        "TeamMember2ID,TeamMember3ID,TeamMember4ID,TeamMember5ID from team where TeamID = ?id", conn);
                    cmd.Parameters.AddWithValue("id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            team.TeamID = reader[0] as int? ?? default;
                            team.TeamName = reader[1] as string ?? default;
                            team.TeamLogo = reader[2] as byte[] ?? default;
                            team.TeamDivisionID = reader[3] as int? ?? default;
                            team.TeamCaptainID = reader[4] as int? ?? default;
                            team.TeamMember2ID = reader[5] as int? ?? default;
                            team.TeamMember3ID = reader[6] as int? ?? default;
                            team.TeamMember4ID = reader[7] as int? ?? default;
                            team.TeamMember5ID = reader[8] as int? ?? default;
                        }
                    }
                }
                return team;
            }
            //exception catch. throw custom exception for logging.
            catch(Exception ex)
            {
                throw new ContextErrorException("TeamContext.GetByID", ex);
            }
        }
        public void Add(TeamDTO entity)
        {
            try 
            {
                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO team VALUES(TeamName = ?TeamName,TeamLogo = ?TeamLogo,TeamDivisionID = ?TeamDivisionID,TeamCaptainID = ?TeamCaptainID," +
                                                        "TeamMember2ID = ?TeamMember2ID,TeamMember3ID = ?TeamMember3ID,TeamMember4ID = ?TeamMember4ID,TeamMember5ID = ?TeamMember5ID)", conn);
                    //values
                    cmd.Parameters.AddWithValue("TeamName", entity.TeamName);
                    cmd.Parameters.AddWithValue("TeamLogo", entity.TeamLogo);
                    cmd.Parameters.AddWithValue("TeamDivisionID", entity.TeamDivisionID);
                    cmd.Parameters.AddWithValue("TeamCaptainID", entity.TeamCaptainID);
                    cmd.Parameters.AddWithValue("TeamMember2ID", entity.TeamMember2ID);
                    cmd.Parameters.AddWithValue("TeamMember3ID", entity.TeamMember3ID);
                    cmd.Parameters.AddWithValue("TeamMember4ID", entity.TeamMember4ID);
                    cmd.Parameters.AddWithValue("TeamMember5ID", entity.TeamMember5ID);
                    //execute command
                    int rowsAffected = cmd.ExecuteNonQuery();
                    //should return if a row is affected or not
                }
            }
            catch(Exception ex)
            {
                throw new ContextErrorException("TeamContext.Add", ex);
            }
        }
        public void Remove(TeamDTO entity)
        {
            try
            {
                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM team WHERE TeamID = ?id", conn);
                    //where id =
                    cmd.Parameters.AddWithValue("id", entity.TeamID);
                    //execute command
                    int rowsAffected = cmd.ExecuteNonQuery();
                    //should return if a row is affected or not
                }
            }
            catch (Exception ex)
            {
                throw new ContextErrorException("TeamContext.Remove", ex);
            }
        }

        public void Update(TeamDTO entity)
        {
            try
            {
                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE team SET TeamName = ?TeamName,TeamLogo = ?TeamLogo,TeamDivisionID = ?TeamDivisionID,TeamCaptainID = ?TeamCaptainID," +
                                                        "TeamMember2ID = ?TeamMember2ID,TeamMember3ID = ?TeamMember3ID,TeamMember4ID = ?TeamMember4ID,TeamMember5ID = ?TeamMember5ID WHERE TeamID = ?id", conn);
                    //where ID
                    cmd.Parameters.Add(new MySqlParameter("id", entity.TeamID));
                    //values
                    cmd.Parameters.AddWithValue("TeamName", entity.TeamName);
                    cmd.Parameters.AddWithValue("TeamLogo", entity.TeamLogo);
                    cmd.Parameters.AddWithValue("TeamDivisionID", entity.TeamDivisionID);
                    cmd.Parameters.AddWithValue("TeamCaptainID", entity.TeamCaptainID);
                    cmd.Parameters.AddWithValue("TeamMember2ID", entity.TeamMember2ID);
                    cmd.Parameters.AddWithValue("TeamMember3ID", entity.TeamMember3ID);
                    cmd.Parameters.AddWithValue("TeamMember4ID", entity.TeamMember4ID);
                    cmd.Parameters.AddWithValue("TeamMember5ID", entity.TeamMember5ID);
                    //execute command
                    int rowsAffected = cmd.ExecuteNonQuery();
                    //should return if a row is affected or not
                }
            }
            catch (Exception ex)
            {
                throw new ContextErrorException("TeamContext.Update", ex);
            }
        }
    }
}
