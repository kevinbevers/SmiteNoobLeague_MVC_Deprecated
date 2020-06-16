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
    public class DivisionContext : IDivisionContext
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
                    MySqlCommand GetDivisionTeamsCmd = new MySqlCommand("SELECT TeamID FROM team WHERE TeamDivisionID = ?id", conn);
                    GetDivisionTeamsCmd.Parameters.AddWithValue("id", id);
                    List<int?> teamList = new List<int?>();

                    using (var reader = GetDivisionTeamsCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            teamList.Add(reader[0] as int? ?? default);
                        }
                    }
                    division.DivisionTeamIDs = teamList;
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
            foreach (var team in entity.DivisionTeamIDs)
            {
                Rows.Add(string.Format("('{0}','{1}')", entity.DivisionID, team));
            }
            sCommand.Append(string.Join(",", Rows));
            sCommand.Append(";");

            MySqlCommand addTeamsToDivisionCmd = new MySqlCommand(sCommand.ToString(), conn);
            addTeamsToDivisionCmd.ExecuteNonQuery();
        }
    }
}
