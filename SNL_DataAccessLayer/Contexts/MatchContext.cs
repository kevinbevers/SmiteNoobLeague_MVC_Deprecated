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
    public class MatchContext : IContext<MatchDTO>
    {
        private readonly ConnectionContext _con;
        public MatchContext(ConnectionContext con)
        {
            _con = con;
        }

        //Create
        public void Add(MatchDTO entity)
        {
            try
            {
                List<int?> GodBanList = entity.GodIDBanList.ToList();

                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO match (ApiMatchID,MatchDate,MatchLength,PatchNumber,WinningTeamID,HomeTeamID,AwayTeamID,GodBan1ID,GodBan2ID,GodBan3ID,GodBan4ID,GodBan5ID,GodBan6ID,GodBan7ID,GodBan8ID,GodBan9ID,GodBan10ID) " +
                        "VALUES(?ApiMatchID,?MatchDate,?MatchLength,?PatchNumber,?WinningTeamID,?HomeTeamID,?AwayTeamID,?GodBan1ID,?GodBan2ID,?GodBan3ID,?GodBan4ID,?GodBan5ID,?GodBan6ID,?GodBan7ID,?GodBan8ID,?GodBan9ID,?GodBan10ID)", conn);
                    //values
                    cmd.Parameters.AddWithValue("ApiMatchID", entity.ApiMatchID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("MatchDate", entity.MatchDate);
                    cmd.Parameters.AddWithValue("MatchLength", entity.MatchLength);
                    cmd.Parameters.AddWithValue("PatchNumber", entity.PatchNumber ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("WinningTeamID", entity.WinningTeamID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("HomeTeamID", entity.HomeTeamID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("AwayTeamID", entity.AwayTeamID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("GodBan1ID", GodBanList[0] ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("GodBan2ID", GodBanList[1] ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("GodBan3ID", GodBanList[2] ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("GodBan4ID", GodBanList[3] ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("GodBan5ID", GodBanList[4] ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("GodBan6ID", GodBanList[5] ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("GodBan7ID", GodBanList[6] ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("GodBan8ID", GodBanList[7] ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("GodBan9ID", GodBanList[8] ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("GodBan10ID", GodBanList[9] ?? (object)DBNull.Value);
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
        public IEnumerable<MatchDTO> GetAll()
        {
            try
            {
                List<MatchDTO> matchList = new List<MatchDTO>();

                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT MatchID, ApiMatchID, MatchDate, MatchLength, PatchNumber, WinningTeamID, HomeTeamID, AwayTeamID" +
                                                        "GodBan1ID, GodBan2ID, GodBan3ID, GodBan4ID, GodBan5ID, GodBan6ID, GodBan7ID, GodBan8ID, GodBan9ID, GodBan10ID FROM match", conn);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MatchDTO match = new MatchDTO
                            {
                                MatchID = reader[0] as int? ?? default,
                                ApiMatchID = reader[1] as int? ?? default,
                                MatchDate = reader[2] as DateTime? ?? default,
                                MatchLength = reader[3] as double? ?? default,
                                PatchNumber = reader[4] as string ?? default,
                                WinningTeamID = reader[5] as int? ?? default,
                                HomeTeamID = reader[6] as int? ?? default,
                                AwayTeamID = reader[7] as int? ?? default,
                                GodIDBanList = new List<int?>
                                        {
                                            reader[8] as int? ?? default,
                                            reader[9] as int? ?? default,
                                            reader[10] as int? ?? default,
                                            reader[11] as int? ?? default,
                                            reader[12] as int? ?? default,
                                            reader[13] as int? ?? default,
                                            reader[14] as int? ?? default,
                                            reader[15] as int? ?? default,
                                            reader[16] as int? ?? default,
                                            reader[17] as int? ?? default,
                                        },
                        };
                            matchList.Add(match);
                        }
                    }
                }
                return matchList;
            }
            catch (Exception ex)
            {
                throw new ContextErrorException(ex);
            }
        }
        public MatchDTO GetByID(int? id)
        {
            try
            {
                MatchDTO match = new MatchDTO();

                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT MatchID, ApiMatchID, MatchDate, MatchLength, PatchNumber, WinningTeamID, HomeTeamID, AwayTeamID" +
                                                        "GodBan1ID, GodBan2ID, GodBan3ID, GodBan4ID, GodBan5ID, GodBan6ID, GodBan7ID, GodBan8ID, GodBan9ID, GodBan10ID FROM match WHERE MatchID = ?id", conn);
                    cmd.Parameters.AddWithValue("id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            match.MatchID = reader[0] as int? ?? default;
                            match.ApiMatchID = reader[1] as int? ?? default;
                            match.MatchDate = reader[2] as DateTime? ?? default;
                            match.MatchLength = reader[3] as double? ?? default;
                            match.PatchNumber = reader[4] as string ?? default;
                            match.WinningTeamID = reader[5] as int? ?? default;
                            match.HomeTeamID = reader[6] as int? ?? default;
                            match.AwayTeamID = reader[7] as int? ?? default;
                            match.GodIDBanList = new List<int?>
                            {
                                reader[8] as int? ?? default,
                                reader[9] as int? ?? default,
                                reader[10] as int? ?? default,
                                reader[11] as int? ?? default,
                                reader[12] as int? ?? default,
                                reader[13] as int? ?? default,
                                reader[14] as int? ?? default,
                                reader[15] as int? ?? default,
                                reader[16] as int? ?? default,
                                reader[17] as int? ?? default,
                            };
                        }
                    }
                }
                return match;
            }
            catch (Exception ex)
            {
                throw new ContextErrorException(ex);
            }
        }
        //Update
        public void Update(MatchDTO entity)
        {
            try
            {
                List<int?> GodBanList = entity.GodIDBanList.ToList();

                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE match SET(ApiMatchID = ?ApiMatchID,MatchDate = ?MatchDate, MatchLength = ?MatchLength," +
                                                        "PatchNumber = ?PatchNumber, WinningTeamID = ?WinningTeamID, HomeTeamID = ?HomeTeamID, AwayTeamID = ?AwayTeamID" +
                                                        "GodBan1ID = ?GodBan1ID, GodBan2ID = ?GodBan2ID, GodBan3ID = ?GodBan3ID, GodBan4ID = ?GodBan4ID, GodBan5ID = ?GodBan5ID, GodBan6ID = ?GodBan6ID, GodBan7ID = ?GodBan7ID, GodBan8ID = ?GodBan8ID, GodBan9ID = ?GodBan9ID, GodBan10ID = ?GodBan10ID) WHERE MatchID = ?id", conn);
                    //where id is
                    cmd.Parameters.AddWithValue("id", entity.MatchID);
                    //values
                    cmd.Parameters.AddWithValue("ApiMatchID", entity.ApiMatchID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("MatchDate", entity.MatchDate);
                    cmd.Parameters.AddWithValue("MatchLength", entity.MatchLength);
                    cmd.Parameters.AddWithValue("PatchNumber", entity.PatchNumber ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("WinningTeamID", entity.WinningTeamID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("HomeTeamID", entity.HomeTeamID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("AwayTeamID", entity.AwayTeamID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("GodBan1ID", GodBanList[0] ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("GodBan2ID", GodBanList[1] ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("GodBan3ID", GodBanList[2] ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("GodBan4ID", GodBanList[3] ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("GodBan5ID", GodBanList[4] ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("GodBan6ID", GodBanList[5] ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("GodBan7ID", GodBanList[6] ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("GodBan8ID", GodBanList[7] ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("GodBan9ID", GodBanList[8] ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("GodBan10ID", GodBanList[9] ?? (object)DBNull.Value);

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
        public void Remove(MatchDTO entity)
        {
            try
            {
                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM match WHERE MatchID = ?id", conn);
                    //where id =
                    cmd.Parameters.AddWithValue("id", entity.MatchID);
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
