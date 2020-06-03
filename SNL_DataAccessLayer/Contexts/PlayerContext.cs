using System;
using System.Collections.Generic;
using System.Text;
using SNL_PersistenceLayer.Interfaces;
using SNL_InterfaceLayer.CustomExceptions;
using SNL_InterfaceLayer.DateTransferObjects;
using MySql.Data.MySqlClient;

namespace SNL_PersistenceLayer.Contexts
{
    public class PlayerContext : IContext<PlayerDTO>
    {
        private readonly ConnectionContext _con;
        public PlayerContext(ConnectionContext con)
        {
            _con = con;
        }

        //Create
        public void Add(PlayerDTO entity)
        {
            try
            {
                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO player VALUES(PlayerID = ?PlayerID, PlayerName = ?PlayerName, PlayerPlatformID = ?PlayerPlatformID," +
                                                        "PlayerRoleID = ?PlayerRoleID, PlayerTeamID = ?PlayerTeamID)", conn);
                    //values
                    cmd.Parameters.AddWithValue("PlayerID", entity.PlayerID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("PlayerName", entity.PlayerName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("PlayerPlatformID", entity.PlayerPlatformID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("PlayerRoleID", entity.PlayerRoleID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("PlayerTeamID", entity.PlayerTeamID ?? (object)DBNull.Value);
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
        public PlayerDTO GetByID(int? id)
        {
            try
            {
                PlayerDTO player = new PlayerDTO();

                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT PlayerID,PlayerName,PlayerTeamID,PlayerRoleID,PlayerPlatformID FROM player WHERE PlayerID = ?id", conn);
                    cmd.Parameters.AddWithValue("id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            player.PlayerID = reader[0] as int? ?? default;
                            player.PlayerName = reader[1] as string ?? default;
                            player.PlayerTeamID = reader[2] as int? ?? default;
                            player.PlayerRoleID = reader[3] as int? ?? default;
                            player.PlayerPlatformID = reader[4] as int? ?? default;
                        }
                    }
                }
                return player;
            }
            catch (Exception ex)
            {
                throw new ContextErrorException(ex);
            }
        }
        public IEnumerable<PlayerDTO> GetAll()
        {
            try
            {
                List<PlayerDTO> playerList = new List<PlayerDTO>();

                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT PlayerID,PlayerName,PlayerTeamID,PlayerRoleID,PlayerPlatformID FROM player", conn);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PlayerDTO player = new PlayerDTO
                            {
                                PlayerID = reader[0] as int? ?? default,
                                PlayerName = reader[1] as string ?? default,
                                PlayerTeamID = reader[2] as int? ?? default,
                                PlayerRoleID = reader[3] as int? ?? default,
                                PlayerPlatformID = reader[4] as int? ?? default,
                            };
                            playerList.Add(player);
                        }
                    }
                }
                return playerList;
            }
            catch (Exception ex)
            {
                throw new ContextErrorException(ex);
            }
        }
        //Update
        public void Update(PlayerDTO entity)
        {
            try
            {
                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE player SET(PlayerName = ?PlayerName, PlayerPlatformID = ?PlayerPlatformID," +
                                                        "PlayerRoleID = ?PlayerRoleID, PlayerTeamID = ?PlayerTeamID) WHERE PlayerID = ?id", conn);
                    //where id is
                    cmd.Parameters.AddWithValue("id", entity.PlayerID);
                    //values
                    cmd.Parameters.AddWithValue("PlayerName", entity.PlayerName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("PlayerPlatformID", entity.PlayerPlatformID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("PlayerRoleID", entity.PlayerRoleID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("PlayerTeamID", entity.PlayerTeamID ?? (object)DBNull.Value);
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
        public void Remove(PlayerDTO entity)
        {
            try
            {
                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM player WHERE PlayerID = ?id", conn);
                    //where id =
                    cmd.Parameters.AddWithValue("id", entity.PlayerID);
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
