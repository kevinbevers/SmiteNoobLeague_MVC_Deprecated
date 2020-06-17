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
    public class PlayerStatContext : IContext<PlayerStatDTO>
    {
        private readonly ConnectionContext _con;
        public PlayerStatContext(ConnectionContext con)
        {
            _con = con;
        }

        //Create
        public void Add(PlayerStatDTO entity)
        {
            try
            {
                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO god (PlayerID,MatchID,TeamID,GodPlayedID,PlayerLevel," +
                                                       "PlayerKills,PlayerDeaths,PlayerAssists,PlayerDamage,PlayerDamageTaken,PlayerDamageMitigated," +
                                                       "PlayerHealing,PlayerGoldEarned,PlayerGoldPerMinute,PlayerItem1ID,PlayerItem2ID," +
                                                       "PlayerItem3ID,PlayerItem4ID,PlayerItem5ID,PlayerItem6ID," +
                                                       "PlayerRelic1ID,PlayerRelic2ID,PlayerWon,PlayerRoleID,PlayerPickOrder) VALUES(?PlayerID,?MatchID,?TeamID,?GodPlayedID,?PlayerLevel," +
                                                       "?PlayerKills,?PlayerDeaths,?PlayerAssists,?PlayerDamage,?PlayerDamageTaken,?PlayerDamageMitigated," +
                                                       "?PlayerHealing,?PlayerGoldEarned,?PlayerGoldPerMinute,?PlayerItem1ID,?PlayerItem2ID," +
                                                       "?PlayerItem3ID,?PlayerItem4ID,?PlayerItem5ID,?PlayerItem6ID," +
                                                       "?PlayerRelic1ID,?PlayerRelic2ID,?PlayerWon,?PlayerRoleID,?PlayerPickOrder)", conn);
                    //values
                    //values
                    cmd.Parameters.AddWithValue("PlayerID", entity.PlayerID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("MatchID", entity.MatchID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("TeamID", entity.TeamID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("GodPlayedID", entity.GodPlayedID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("PlayerLevel", entity.PlayerLevel);
                    cmd.Parameters.AddWithValue("PlayerKills", entity.PlayerKills);
                    cmd.Parameters.AddWithValue("PlayerDeaths", entity.PlayerDeaths);
                    cmd.Parameters.AddWithValue("PlayerAssists", entity.PlayerAssists);
                    cmd.Parameters.AddWithValue("PlayerDamage", entity.PlayerDamage);
                    cmd.Parameters.AddWithValue("PlayerDamageTaken", entity.PlayerDamageTaken);
                    cmd.Parameters.AddWithValue("PlayerDamageMitigated", entity.PlayerDamageMitigated);
                    cmd.Parameters.AddWithValue("PlayerHealing", entity.PlayerHealing);
                    cmd.Parameters.AddWithValue("PlayerGoldEarned", entity.PlayerGoldEarned);
                    cmd.Parameters.AddWithValue("PlayerGoldPerMinute", entity.PlayerGoldPerMinute);
                    cmd.Parameters.AddWithValue("PlayerItem1ID", entity.PlayerItem1ID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("PlayerItem2ID", entity.PlayerItem2ID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("PlayerItem3ID", entity.PlayerItem3ID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("PlayerItem4ID", entity.PlayerItem4ID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("PlayerItem5ID", entity.PlayerItem5ID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("PlayerItem6ID", entity.PlayerItem6ID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("PlayerRelic1ID", entity.PlayerRelic1ID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("PlayerRelic2ID", entity.PlayerRelic2ID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("PlayerWon", entity.PlayerWon);
                    cmd.Parameters.AddWithValue("PlayerRoleID", entity.PlayerRoleID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("PlayerPickOrder", entity.PlayerPickOrder);
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
        public void AddMultiple(List<PlayerStatDTO> entityList)
        {
            //build a string with all the values in it.
            StringBuilder sCommand = new StringBuilder("INSERT INTO playerstat (PlayerID,MatchID,TeamID,GodPlayedID,PlayerLevel," +
                                                       "PlayerKills,PlayerDeaths,PlayerAssists,PlayerDamage,PlayerDamageTaken,PlayerDamageMitigated," +
                                                       "PlayerHealing,PlayerGoldEarned,PlayerGoldPerMinute,PlayerItem1ID,PlayerItem2ID," +
                                                       "PlayerItem3ID,PlayerItem4ID,PlayerItem5ID,PlayerItem6ID," +
                                                       "PlayerRelic1ID,PlayerRelic2ID,PlayerWon,PlayerRoleID,PlayerPickOrder) VALUES ");

            List<string> Rows = new List<string>();
            foreach (var entity in entityList)
            {
                Rows.Add(string.Format("('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}'," +
                                       "'{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}')", 
                                       entity.PlayerID,entity.MatchID,entity.TeamID,entity.GodPlayedID,entity.PlayerLevel,entity.PlayerKills,
                                       entity.PlayerDeaths,entity.PlayerAssists,entity.PlayerDamage,entity.PlayerDamageTaken,entity.PlayerDamageMitigated,
                                       entity.PlayerHealing,entity.PlayerGoldEarned,entity.PlayerGoldPerMinute,entity.PlayerItem1ID,entity.PlayerItem2ID,
                                       entity.PlayerItem3ID,entity.PlayerItem4ID,entity.PlayerItem5ID,entity.PlayerItem6ID,entity.PlayerRelic1ID,
                                       entity.PlayerRelic2ID,entity.PlayerWon,entity.PlayerRoleID,entity.PlayerPickOrder));
            }
            sCommand.Append(string.Join(",", Rows));
            sCommand.Append(";");

            using (MySqlConnection conn = _con.GetConnection())
            {
                conn.Open();
                MySqlCommand addMultipleCMD = new MySqlCommand(sCommand.ToString(), conn);
                addMultipleCMD.ExecuteNonQuery();
            }
        }
        //Read
        public IEnumerable<PlayerStatDTO> GetAll()
        {
            try
            {
                List<PlayerStatDTO> playerStatsList = new List<PlayerStatDTO>();

                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT PlayerStatID,PlayerID,MatchID,TeamID,GodPlayedID,PlayerLevel,PlayerKills," +
                                                        "PlayerDeaths,PlayerAssists,PlayerDamage,PlayerDamageTaken,PlayerDamageMitigated," +
                                                        "PlayerHealing,PlayerGoldEarned,PlayerGoldPerMinute,PlayerItem1ID,PlayerItem2ID," +
                                                        "PlayerItem3ID,PlayerItem4ID,PlayerItem5ID,PlayerItem6ID,PlayerRelic1ID,PlayerRelic2ID," +
                                                        "PlayerWon,PlayerRoleID,PlayerPickOrder FROM playerstat", conn);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PlayerStatDTO playerStat = new PlayerStatDTO
                            {
                                PlayerStatID = reader[0] as int? ?? default,
                                PlayerID = reader[1] as int? ?? default,
                                MatchID = reader[2] as int? ?? default,
                                TeamID = reader[3] as int? ?? default,
                                GodPlayedID = reader[4] as int? ?? default,
                                PlayerLevel = reader[5] as int? ?? default,
                                PlayerKills = reader[6] as int? ?? default,
                                PlayerDeaths = reader[7] as int? ?? default,
                                PlayerAssists = reader[8] as int? ?? default,
                                PlayerDamage = reader[9] as int? ?? default,
                                PlayerDamageTaken = reader[10] as int? ?? default,
                                PlayerDamageMitigated = reader[11] as int? ?? default,
                                PlayerHealing = reader[12] as int? ?? default,
                                PlayerGoldEarned = reader[13] as int? ?? default,
                                PlayerGoldPerMinute = reader[14] as int? ?? default,
                                PlayerItem1ID = reader[15] as int? ?? default,
                                PlayerItem2ID = reader[16] as int? ?? default,
                                PlayerItem3ID = reader[17] as int? ?? default,
                                PlayerItem4ID = reader[18] as int? ?? default,
                                PlayerItem5ID = reader[19] as int? ?? default,
                                PlayerItem6ID = reader[20] as int? ?? default,
                                PlayerRelic1ID = reader[21] as int? ?? default,
                                PlayerRelic2ID = reader[22] as int? ?? default,
                                PlayerWon = reader[23] as bool? ?? default,
                                PlayerRoleID = reader[24] as int? ?? default,
                                PlayerPickOrder = reader[25] as int? ?? default,
                            };
                            playerStatsList.Add(playerStat);
                        }
                    }
                }
                return playerStatsList;
            }
            catch (Exception ex)
            {
                throw new ContextErrorException(ex);
            }
        }
        public PlayerStatDTO GetByID(int? id)
        {
            try
            {
                PlayerStatDTO playerStat = new PlayerStatDTO();

                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT PlayerStatID,PlayerID,MatchID,TeamID,GodPlayedID,PlayerLevel,PlayerKills," +
                                                        "PlayerDeaths,PlayerAssists,PlayerDamage,PlayerDamageTaken,PlayerDamageMitigated," +
                                                        "PlayerHealing,PlayerGoldEarned,PlayerGoldPerMinute,PlayerItem1ID,PlayerItem2ID," +
                                                        "PlayerItem3ID,PlayerItem4ID,PlayerItem5ID,PlayerItem6ID,PlayerRelic1ID,PlayerRelic2ID," +
                                                        "PlayerWon,PlayerRoleID,PlayerPickOrder FROM playerstat WHERE PlayerStatID = ?id", conn);
                    cmd.Parameters.AddWithValue("id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            playerStat.PlayerStatID = reader[0] as int? ?? default;
                            playerStat.PlayerID = reader[1] as int? ?? default;
                            playerStat.MatchID = reader[2] as int? ?? default;
                            playerStat.TeamID = reader[3] as int? ?? default;
                            playerStat.GodPlayedID = reader[4] as int? ?? default;
                            playerStat.PlayerLevel = reader[5] as int? ?? default;
                            playerStat.PlayerKills = reader[6] as int? ?? default;
                            playerStat.PlayerDeaths = reader[7] as int? ?? default;
                            playerStat.PlayerAssists = reader[8] as int? ?? default;
                            playerStat.PlayerDamage = reader[9] as int? ?? default;
                            playerStat.PlayerDamageTaken = reader[10] as int? ?? default;
                            playerStat.PlayerDamageMitigated = reader[11] as int? ?? default;
                            playerStat.PlayerHealing = reader[12] as int? ?? default;
                            playerStat.PlayerGoldEarned = reader[13] as int? ?? default;
                            playerStat.PlayerGoldPerMinute = reader[14] as int? ?? default;
                            playerStat.PlayerItem1ID = reader[15] as int? ?? default;
                            playerStat.PlayerItem2ID = reader[16] as int? ?? default;
                            playerStat.PlayerItem3ID = reader[17] as int? ?? default;
                            playerStat.PlayerItem4ID = reader[18] as int? ?? default;
                            playerStat.PlayerItem5ID = reader[19] as int? ?? default;
                            playerStat.PlayerItem6ID = reader[20] as int? ?? default;
                            playerStat.PlayerRelic1ID = reader[21] as int? ?? default;
                            playerStat.PlayerRelic2ID = reader[22] as int? ?? default;
                            playerStat.PlayerWon = reader[23] as bool? ?? default;
                            playerStat.PlayerRoleID = reader[24] as int? ?? default;
                            playerStat.PlayerPickOrder = reader[25] as int? ?? default;

                        }
                    }
                }
                return playerStat;
            }
            catch (Exception ex)
            {
                throw new ContextErrorException(ex);
            }
        }
        public IEnumerable<PlayerStatDTO> GetByPlayerID(int? id)
        {
            try
            {
                List<PlayerStatDTO> playerStatsList = new List<PlayerStatDTO>();

                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT PlayerStatID,PlayerID,MatchID,TeamID,GodPlayedID,PlayerLevel,PlayerKills," +
                                                        "PlayerDeaths,PlayerAssists,PlayerDamage,PlayerDamageTaken,PlayerDamageMitigated," +
                                                        "PlayerHealing,PlayerGoldEarned,PlayerGoldPerMinute,PlayerItem1ID,PlayerItem2ID," +
                                                        "PlayerItem3ID,PlayerItem4ID,PlayerItem5ID,PlayerItem6ID,PlayerRelic1ID,PlayerRelic2ID," +
                                                        "PlayerWon,PlayerRoleID,PlayerPickOrder FROM playerstat WHERE PlayerID = ?id", conn);
                    cmd.Parameters.AddWithValue("id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PlayerStatDTO playerStat = new PlayerStatDTO
                            {
                                PlayerStatID = reader[0] as int? ?? default,
                                PlayerID = reader[1] as int? ?? default,
                                MatchID = reader[2] as int? ?? default,
                                TeamID = reader[3] as int? ?? default,
                                GodPlayedID = reader[4] as int? ?? default,
                                PlayerLevel = reader[5] as int? ?? default,
                                PlayerKills = reader[6] as int? ?? default,
                                PlayerDeaths = reader[7] as int? ?? default,
                                PlayerAssists = reader[8] as int? ?? default,
                                PlayerDamage = reader[9] as int? ?? default,
                                PlayerDamageTaken = reader[10] as int? ?? default,
                                PlayerDamageMitigated = reader[11] as int? ?? default,
                                PlayerHealing = reader[12] as int? ?? default,
                                PlayerGoldEarned = reader[13] as int? ?? default,
                                PlayerGoldPerMinute = reader[14] as int? ?? default,
                                PlayerItem1ID = reader[15] as int? ?? default,
                                PlayerItem2ID = reader[16] as int? ?? default,
                                PlayerItem3ID = reader[17] as int? ?? default,
                                PlayerItem4ID = reader[18] as int? ?? default,
                                PlayerItem5ID = reader[19] as int? ?? default,
                                PlayerItem6ID = reader[20] as int? ?? default,
                                PlayerRelic1ID = reader[21] as int? ?? default,
                                PlayerRelic2ID = reader[22] as int? ?? default,
                                PlayerWon = reader[23] as bool? ?? default,
                                PlayerRoleID = reader[24] as int? ?? default,
                                PlayerPickOrder = reader[25] as int? ?? default,
                            };
                            playerStatsList.Add(playerStat);
                        }
                    }
                }
                return playerStatsList;
            }
            catch (Exception ex)
            {
                throw new ContextErrorException(ex);
            }
        }
        public IEnumerable<PlayerStatDTO> GetByGodID(int? id)
        {
            try
            {
                List<PlayerStatDTO> playerStatsList = new List<PlayerStatDTO>();

                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT PlayerStatID,PlayerID,MatchID,TeamID,GodPlayedID,PlayerLevel,PlayerKills," +
                                                        "PlayerDeaths,PlayerAssists,PlayerDamage,PlayerDamageTaken,PlayerDamageMitigated," +
                                                        "PlayerHealing,PlayerGoldEarned,PlayerGoldPerMinute,PlayerItem1ID,PlayerItem2ID," +
                                                        "PlayerItem3ID,PlayerItem4ID,PlayerItem5ID,PlayerItem6ID,PlayerRelic1ID,PlayerRelic2ID," +
                                                        "PlayerWon,PlayerRoleID,PlayerPickOrder FROM playerstat WHERE GodPlayedID = ?id", conn);
                    cmd.Parameters.AddWithValue("id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PlayerStatDTO playerStat = new PlayerStatDTO
                            {
                                PlayerStatID = reader[0] as int? ?? default,
                                PlayerID = reader[1] as int? ?? default,
                                MatchID = reader[2] as int? ?? default,
                                TeamID = reader[3] as int? ?? default,
                                GodPlayedID = reader[4] as int? ?? default,
                                PlayerLevel = reader[5] as int? ?? default,
                                PlayerKills = reader[6] as int? ?? default,
                                PlayerDeaths = reader[7] as int? ?? default,
                                PlayerAssists = reader[8] as int? ?? default,
                                PlayerDamage = reader[9] as int? ?? default,
                                PlayerDamageTaken = reader[10] as int? ?? default,
                                PlayerDamageMitigated = reader[11] as int? ?? default,
                                PlayerHealing = reader[12] as int? ?? default,
                                PlayerGoldEarned = reader[13] as int? ?? default,
                                PlayerGoldPerMinute = reader[14] as int? ?? default,
                                PlayerItem1ID = reader[15] as int? ?? default,
                                PlayerItem2ID = reader[16] as int? ?? default,
                                PlayerItem3ID = reader[17] as int? ?? default,
                                PlayerItem4ID = reader[18] as int? ?? default,
                                PlayerItem5ID = reader[19] as int? ?? default,
                                PlayerItem6ID = reader[20] as int? ?? default,
                                PlayerRelic1ID = reader[21] as int? ?? default,
                                PlayerRelic2ID = reader[22] as int? ?? default,
                                PlayerWon = reader[23] as bool? ?? default,
                                PlayerRoleID = reader[24] as int? ?? default,
                                PlayerPickOrder = reader[25] as int? ?? default,
                            };
                            playerStatsList.Add(playerStat);
                        }
                    }
                }
                return playerStatsList;
            }
            catch (Exception ex)
            {
                throw new ContextErrorException(ex);
            }
        }
        public IEnumerable<PlayerStatDTO> GetByTeamID(int? id)
        {
            try
            {
                List<PlayerStatDTO> playerStatsList = new List<PlayerStatDTO>();

                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT PlayerStatID,PlayerID,MatchID,TeamID,GodPlayedID,PlayerLevel,PlayerKills," +
                                                        "PlayerDeaths,PlayerAssists,PlayerDamage,PlayerDamageTaken,PlayerDamageMitigated," +
                                                        "PlayerHealing,PlayerGoldEarned,PlayerGoldPerMinute,PlayerItem1ID,PlayerItem2ID," +
                                                        "PlayerItem3ID,PlayerItem4ID,PlayerItem5ID,PlayerItem6ID,PlayerRelic1ID,PlayerRelic2ID," +
                                                        "PlayerWon,PlayerRoleID,PlayerPickOrder FROM playerstat WHERE TeamID = ?id", conn);
                    cmd.Parameters.AddWithValue("id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PlayerStatDTO playerStat = new PlayerStatDTO
                            {
                                PlayerStatID = reader[0] as int? ?? default,
                                PlayerID = reader[1] as int? ?? default,
                                MatchID = reader[2] as int? ?? default,
                                TeamID = reader[3] as int? ?? default,
                                GodPlayedID = reader[4] as int? ?? default,
                                PlayerLevel = reader[5] as int? ?? default,
                                PlayerKills = reader[6] as int? ?? default,
                                PlayerDeaths = reader[7] as int? ?? default,
                                PlayerAssists = reader[8] as int? ?? default,
                                PlayerDamage = reader[9] as int? ?? default,
                                PlayerDamageTaken = reader[10] as int? ?? default,
                                PlayerDamageMitigated = reader[11] as int? ?? default,
                                PlayerHealing = reader[12] as int? ?? default,
                                PlayerGoldEarned = reader[13] as int? ?? default,
                                PlayerGoldPerMinute = reader[14] as int? ?? default,
                                PlayerItem1ID = reader[15] as int? ?? default,
                                PlayerItem2ID = reader[16] as int? ?? default,
                                PlayerItem3ID = reader[17] as int? ?? default,
                                PlayerItem4ID = reader[18] as int? ?? default,
                                PlayerItem5ID = reader[19] as int? ?? default,
                                PlayerItem6ID = reader[20] as int? ?? default,
                                PlayerRelic1ID = reader[21] as int? ?? default,
                                PlayerRelic2ID = reader[22] as int? ?? default,
                                PlayerWon = reader[23] as bool? ?? default,
                                PlayerRoleID = reader[24] as int? ?? default,
                                PlayerPickOrder = reader[25] as int? ?? default,
                            };
                            playerStatsList.Add(playerStat);
                        }
                    }
                }
                return playerStatsList;
            }
            catch (Exception ex)
            {
                throw new ContextErrorException(ex);
            }
        }
        public IEnumerable<PlayerStatDTO> GetByMatchID(int? id)
        {
            try
            {
                List<PlayerStatDTO> playerStatsList = new List<PlayerStatDTO>();

                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT PlayerStatID,PlayerID,MatchID,TeamID,GodPlayedID,PlayerLevel,PlayerKills," +
                                                        "PlayerDeaths,PlayerAssists,PlayerDamage,PlayerDamageTaken,PlayerDamageMitigated," +
                                                        "PlayerHealing,PlayerGoldEarned,PlayerGoldPerMinute,PlayerItem1ID,PlayerItem2ID," +
                                                        "PlayerItem3ID,PlayerItem4ID,PlayerItem5ID,PlayerItem6ID,PlayerRelic1ID,PlayerRelic2ID," +
                                                        "PlayerWon,PlayerRoleID,PlayerPickOrder FROM playerstat WHERE MatchID = ?id", conn);
                    cmd.Parameters.AddWithValue("id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PlayerStatDTO playerStat = new PlayerStatDTO
                            {
                                PlayerStatID = reader[0] as int? ?? default,
                                PlayerID = reader[1] as int? ?? default,
                                MatchID = reader[2] as int? ?? default,
                                TeamID = reader[3] as int? ?? default,
                                GodPlayedID = reader[4] as int? ?? default,
                                PlayerLevel = reader[5] as int? ?? default,
                                PlayerKills = reader[6] as int? ?? default,
                                PlayerDeaths = reader[7] as int? ?? default,
                                PlayerAssists = reader[8] as int? ?? default,
                                PlayerDamage = reader[9] as int? ?? default,
                                PlayerDamageTaken = reader[10] as int? ?? default,
                                PlayerDamageMitigated = reader[11] as int? ?? default,
                                PlayerHealing = reader[12] as int? ?? default,
                                PlayerGoldEarned = reader[13] as int? ?? default,
                                PlayerGoldPerMinute = reader[14] as int? ?? default,
                                PlayerItem1ID = reader[15] as int? ?? default,
                                PlayerItem2ID = reader[16] as int? ?? default,
                                PlayerItem3ID = reader[17] as int? ?? default,
                                PlayerItem4ID = reader[18] as int? ?? default,
                                PlayerItem5ID = reader[19] as int? ?? default,
                                PlayerItem6ID = reader[20] as int? ?? default,
                                PlayerRelic1ID = reader[21] as int? ?? default,
                                PlayerRelic2ID = reader[22] as int? ?? default,
                                PlayerWon = reader[23] as bool? ?? default,
                                PlayerRoleID = reader[24] as int? ?? default,
                                PlayerPickOrder = reader[25] as int? ?? default,
                            };
                            playerStatsList.Add(playerStat);
                        }
                    }
                }
                return playerStatsList;
            }
            catch (Exception ex)
            {
                throw new ContextErrorException(ex);
            }
        }
        //Update
        public void Update(PlayerStatDTO entity)
        {
            try
            {
                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE playerstat SET(PlayerID = ?PlayerID,MatchID = ?MatchID," +
                                                        "TeamID = ?TeamID, GodPlayedID = ?GodPlayedID,PlayerLevel = ?PlayerLevel,PlayerKills = ?PlayerKills," +
                                                        "PlayerDeaths = ?PlayerDeaths,PlayerAssists = ?PlayerAssists,PlayerDamage = ?PlayerDamage,PlayerDamageTaken = ?PlayerDamageTaken," +
                                                        "PlayerDamageMitigated = ?PlayerDamageMitigated,PlayerHealing = ?PlayerHealing,PlayerGoldEarned = ?PlayerGoldEarned," +
                                                        "PlayerGoldPerMinute = ?PlayerGoldPerMinute,PlayerItem1ID = ?PlayerItem1ID,PlayerItem2ID = ?PlayerItem2ID," +
                                                        "PlayerItem3ID = ?PlayerItem3ID,PlayerItem4ID = ?PlayerItem4ID,PlayerItem5ID = ?PlayerItem5ID,PlayerItem6ID = ?PlayerItem6ID," +
                                                        "PlayerRelic1ID = ?PlayerRelic1ID,PlayerRelic2ID = ?PlayerRelic2ID,PlayerWon = ?PlayerWon, PlayerRoleID = ?PlayerRoleID, PlayerPickOrder = ?PlayerPickOrder) WHERE PlayerStatID = ?id", conn);
                    //where id is
                    cmd.Parameters.AddWithValue("id", entity.PlayerStatID);
                    //values
                    cmd.Parameters.AddWithValue("PlayerID", entity.PlayerID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("MatchID", entity.MatchID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("TeamID", entity.TeamID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("GodPlayedID", entity.GodPlayedID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("PlayerLevel", entity.PlayerLevel);
                    cmd.Parameters.AddWithValue("PlayerKills", entity.PlayerKills);
                    cmd.Parameters.AddWithValue("PlayerDeaths", entity.PlayerDeaths);
                    cmd.Parameters.AddWithValue("PlayerAssists", entity.PlayerAssists);
                    cmd.Parameters.AddWithValue("PlayerDamage", entity.PlayerDamage);
                    cmd.Parameters.AddWithValue("PlayerDamageTaken", entity.PlayerDamageTaken);
                    cmd.Parameters.AddWithValue("PlayerDamageMitigated", entity.PlayerDamageMitigated);
                    cmd.Parameters.AddWithValue("PlayerHealing", entity.PlayerHealing);
                    cmd.Parameters.AddWithValue("PlayerGoldEarned", entity.PlayerGoldEarned);
                    cmd.Parameters.AddWithValue("PlayerGoldPerMinute", entity.PlayerGoldPerMinute);
                    cmd.Parameters.AddWithValue("PlayerItem1ID", entity.PlayerItem1ID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("PlayerItem2ID", entity.PlayerItem2ID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("PlayerItem3ID", entity.PlayerItem3ID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("PlayerItem4ID", entity.PlayerItem4ID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("PlayerItem5ID", entity.PlayerItem5ID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("PlayerItem6ID", entity.PlayerItem6ID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("PlayerRelic1ID", entity.PlayerRelic1ID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("PlayerRelic2ID", entity.PlayerRelic2ID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("PlayerWon", entity.PlayerWon);
                    cmd.Parameters.AddWithValue("PlayerRoleID", entity.PlayerRoleID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("PlayerPickOrder", entity.PlayerPickOrder);


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
        public void Remove(PlayerStatDTO entity)
        {
            try
            {
                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM playerstat WHERE PlayerStatID = ?id", conn);
                    //where id =
                    cmd.Parameters.AddWithValue("id", entity.PlayerStatID);
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
