using System;
using System.Collections.Generic;
using System.Text;
using SNL_PersistenceLayer.Interfaces;
using SNL_InterfaceLayer.CustomExceptions;
using SNL_InterfaceLayer.DateTransferObjects;
using MySql.Data.MySqlClient;
using System.Linq;

namespace SNL_PersistenceLayer.Contexts
{
    public class DivisionContext : IContext<DivisionDTO>
    {
        private readonly ConnectionContext _con;
        public DivisionContext(ConnectionContext con)
        {
            _con = con;
        }
        //Create
        public void Add(DivisionDTO entity)
        {
            try
            {
                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO division VALUES(DivisionName = ?DivisionName, " +
                        "DivisionDescription = ?DivisionDescription)", conn);
                    //values
                    //cmd.Parameters.AddWithValue("DivisionID", entity.DivisionID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("DivisionName", entity.DivisionName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("DivisionDescription", entity.DivisionDescription ?? (object)DBNull.Value);
                    //execute command
                    int rowsAffected = cmd.ExecuteNonQuery();

                    AddTeamsToDivision(entity, conn);

                    //should return if a row is affected or not
                }
            }
            catch (Exception ex)
            {
                throw new ContextErrorException(ex);
            }
        }
        //Read
        public IEnumerable<DivisionDTO> GetAll()
        {
            try
            {
                List<DivisionDTO> divisionList = new List<DivisionDTO>();

                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT DivisionID,DivisionName,DivisionDescription FROM division", conn);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DivisionDTO division = new DivisionDTO
                            {
                                DivisionID = reader[0] as int? ?? default,
                                DivisionName = reader[1] as string ?? default,
                                DivisionDescription = reader[2] as string ?? default,
                            };
                            divisionList.Add(division);
                        }
                    }
                }
                return divisionList;
            }
            catch (Exception ex)
            {
                throw new ContextErrorException(ex);
            }
        }
        public DivisionDTO GetByID(int? id)
        {
            try
            {
                DivisionDTO division = new DivisionDTO();

                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT DivisionID,DivisionName,DivisionDescription FROM division WHERE DivisionID = ?id", conn);
                    cmd.Parameters.AddWithValue("id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            division.DivisionID = reader[0] as int? ?? default;
                            division.DivisionName = reader[1] as string ?? default;
                            division.DivisionDescription = reader[2] as string ?? default;
                        }
                    }
                    //get the teams that are in the division and add them in a list
                    MySqlCommand GetDivisionTeamsCmd = new MySqlCommand("SELECT TeamID,TeamName,TeamLogo,TeamDivisionID,TeamCaptainID," +
                                                        "TeamMember2ID,TeamMember3ID,TeamMember4ID,TeamMember5ID FROM team WHERE TeamDivisionID = ?id", conn);
                    GetDivisionTeamsCmd.Parameters.AddWithValue("id", id);
                    List<TeamDTO> teamList = new List<TeamDTO>();

                    using (var reader = GetDivisionTeamsCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            teamList.Add(
                                new TeamDTO
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
                                });
                        }
                    }
                    division.DivisionTeams = teamList;
                }

                return division;
            }
            catch (Exception ex)
            {
                throw new ContextErrorException(ex);
            }
        }
        //Update
        public void Update(DivisionDTO entity)
        {
            try
            {
                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE account SET(DivisionName = ?DivisionName," +
                                                        "DivisionDescription = ?DivisionDescription) WHERE DivisionID = ?id", conn);
                    //where id is
                    cmd.Parameters.AddWithValue("id", entity.DivisionID);
                    //values
                    cmd.Parameters.AddWithValue("DivisionName", entity.DivisionName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("DivisionDescription", entity.DivisionDescription ?? (object)DBNull.Value);
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
        public void Remove(DivisionDTO entity)
        {
            try
            {
                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM divsion WHERE DivisionID = ?id", conn);
                    //where id =
                    cmd.Parameters.AddWithValue("id", entity.DivisionID);
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
        //Methods for division teams
        private static void AddTeamsToDivision(DivisionDTO entity, MySqlConnection conn)
        {
            //build a string with all the values in it. no mysql.escapestring needed because integers cannot be used for SQL injection
            StringBuilder sCommand = new StringBuilder("INSERT INTO divisionteam (DivisionID, TeamID) VALUES ");

            List<string> Rows = new List<string>();
            foreach (var team in entity.DivisionTeams)
            {
                Rows.Add(string.Format("('{0}','{1}')", entity.DivisionID, team.TeamID));
            }
            sCommand.Append(string.Join(",", Rows));
            sCommand.Append(";");

            MySqlCommand addTeamsToDivisionCmd = new MySqlCommand(sCommand.ToString(), conn);
            addTeamsToDivisionCmd.ExecuteNonQuery();
        }
    }
}
