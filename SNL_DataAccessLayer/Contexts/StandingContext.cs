using System;
using System.Collections.Generic;
using System.Text;
using SNL_InterfaceLayer.Interfaces;
using SNL_InterfaceLayer.CustomExceptions;
using SNL_InterfaceLayer.DateTransferObjects;
using MySql.Data.MySqlClient;
using System.Linq;

namespace SNL_PersistenceLayer.Contexts
{
    public class StandingContext : IContext<StandingDTO>
    {
        private readonly ConnectionContext _con;
        public StandingContext(ConnectionContext con)
        {
            _con = con;
        }

        //Create
        public void Add(StandingDTO entity)
        {
            try
            {
                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO standing (DivisionID,TeamID,Score) VALUES(?DivisionID,?TeamID,?Score)", conn);
                    //values
                    cmd.Parameters.AddWithValue("DivisionID", entity.DivisionID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("TeamID", entity.TeamID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("Score", entity.Score ?? (object)DBNull.Value);
                    //execute command
                    int rowsAffected = cmd.ExecuteNonQuery();
                    //should return if a row is affected or not
                }
            }
            catch (Exception ex)
            {
                throw new ContextErrorException(ex);
            }
        }
        //Read
        public IEnumerable<StandingDTO> GetAll()
        {
            try
            {
                List<StandingDTO> standingList = new List<StandingDTO>();

                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT StandingID,DivisionID,TeamID,Score FROM standing", conn);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            StandingDTO standing = new StandingDTO
                            {
                                StandingID = reader[0] as int? ?? default,
                                DivisionID = reader[1] as int? ?? default,
                                TeamID = reader[2] as int? ?? default,
                                Score = reader[3] as int? ?? default,                              
                            };
                            standingList.Add(standing);
                        }
                    }
                }
                return standingList;
            }
            catch (Exception ex)
            {
                throw new ContextErrorException(ex);
            }
        }
        public StandingDTO GetByID(int? id)
        {
            try
            {
                StandingDTO standing = new StandingDTO();

                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT StandingID,DivisionID,TeamID,Score FROM standing WHERE StandingID = ?id", conn);
                    cmd.Parameters.AddWithValue("id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            standing.StandingID = reader[0] as int? ?? default;
                            standing.DivisionID = reader[1] as int? ?? default;
                            standing.TeamID = reader[2] as int? ?? default;
                            standing.Score = reader[3] as int? ?? default;
                        }
                    }
                }
                return standing;
            }
            catch (Exception ex)
            {
                throw new ContextErrorException(ex);
            }
        }
        public IEnumerable<StandingDTO> GetByDivisionID(int? id)
        {
            try
            {
                List<StandingDTO> standingList = new List<StandingDTO>();

                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT StandingID,DivisionID,TeamID,Score FROM standing WHERE DivisionID = ?id", conn);
                    cmd.Parameters.AddWithValue("id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            StandingDTO standing = new StandingDTO
                            {
                                StandingID = reader[0] as int? ?? default,
                                DivisionID = reader[1] as int? ?? default,
                                TeamID = reader[2] as int? ?? default,
                                Score = reader[3] as int? ?? default,
                            };
                            standingList.Add(standing);
                        }
                    }
                }
                return standingList;
            }
            catch (Exception ex)
            {
                throw new ContextErrorException(ex);
            }
        }
        public IEnumerable<StandingDTO> GetByTeamID(int? id)
        {
            try
            {
                List<StandingDTO> standingList = new List<StandingDTO>();

                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT StandingID,DivisionID,TeamID,Score FROM standing WHERE TeamID = ?id", conn);
                    cmd.Parameters.AddWithValue("id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            StandingDTO standing = new StandingDTO
                            {
                                StandingID = reader[0] as int? ?? default,
                                DivisionID = reader[1] as int? ?? default,
                                TeamID = reader[2] as int? ?? default,
                                Score = reader[3] as int? ?? default,
                            };
                            standingList.Add(standing);
                        }
                    }
                }
                return standingList;
            }
            catch (Exception ex)
            {
                throw new ContextErrorException(ex);
            }
        }
        //Update
        public void Update(StandingDTO entity)
        {
            try
            {
                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE standing SET(DivisionID = ?DivisionID, TeamID = ?TeamID, Score = ?Score) WHERE StandingID = ?id", conn);
                    //where id is
                    cmd.Parameters.AddWithValue("id", entity.StandingID);
                    //values
                    cmd.Parameters.AddWithValue("DivisionID", entity.DivisionID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("TeamID", entity.TeamID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("Score", entity.Score ?? (object)DBNull.Value);
                    //execute command
                    int rowsAffected = cmd.ExecuteNonQuery();
                    //should return if a row is affected or not
                }
            }
            catch (Exception ex)
            {
                throw new ContextErrorException(ex);
            }
        }
        //Delete
        public void Remove(StandingDTO entity)
        {
            try
            {
                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM standing WHERE StandingID = ?id", conn);
                    //where id =
                    cmd.Parameters.AddWithValue("id", entity.StandingID);
                    //execute command
                    int rowsAffected = cmd.ExecuteNonQuery();
                    //should return if a row is affected or not
                }
            }
            catch (Exception ex)
            {
                throw new ContextErrorException(ex);
            }
        }
    }
}
