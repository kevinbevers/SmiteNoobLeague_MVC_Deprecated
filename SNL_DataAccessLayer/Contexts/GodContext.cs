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
    public class GodContext : IContext<GodDTO>
    {
        private readonly ConnectionContext _con;
        public GodContext(ConnectionContext con)
        {
            _con = con;
        }

        //Create
        public void Add(GodDTO entity)
        {
            try
            {
                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO god VALUES(GodID = ?GodID, GodName = ?GodName," +
                                                        "GodTitle = ?GodTitle, GodLore = ?GodLore, GodClass = ?GodClass, GodIcon = ?GodIcon, GodCardArt = ?GodCardArt)", conn);
                    //values
                    cmd.Parameters.AddWithValue("GodID", entity.GodID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("GodName", entity.GodName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("GodTitle", entity.GodTitle ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("GodLore", entity.GodLore ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("GodClass", entity.GodClass ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("GodIcon", entity.GodIcon ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("GodCardArt", entity.GodCardArt ?? (object)DBNull.Value);
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
        public void AddMultiple(IEnumerable<GodDTO> entityList)
        {
            //build a string with all the values in it. no mysql.escapestring needed because integers cannot be used for SQL injection
            StringBuilder sCommand = new StringBuilder("INSERT INTO god (GodID,GodName,GodTitle,GodLore,GodClass,GodIcon,GodCardArt) VALUES ");

            List<string> Rows = new List<string>();
            foreach (var entity in entityList)
            {
                Rows.Add(string.Format("('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", 
                    entity.GodID,
                    entity.GodName ?? (object)DBNull.Value,
                    entity.GodTitle ?? (object)DBNull.Value,
                    entity.GodLore ?? (object)DBNull.Value,
                    entity.GodClass ?? (object)DBNull.Value,
                    entity.GodIcon ?? (object)DBNull.Value,
                    entity.GodCardArt ?? (object)DBNull.Value));
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
        public IEnumerable<GodDTO> GetAll()
        {
            try
            {
                List<GodDTO> godList = new List<GodDTO>();

                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT GodID,GodName,GodTitle,GodLore,GodClass,GodIcon,GodCardArt FROM god", conn);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            GodDTO god = new GodDTO
                            {
                                GodID = reader[0] as int? ?? default,
                                GodName = reader[1] as string ?? default,
                                GodTitle = reader[2] as string ?? default,
                                GodLore = reader[3] as string ?? default,
                                GodClass = reader[4] as string ?? default,
                                GodIcon = reader[5] as byte[] ?? default,
                                GodCardArt = reader[6] as byte[] ?? default,
                            };
                            godList.Add(god);
                        }
                    }
                }
                return godList;
            }
            catch (Exception ex)
            {
                throw new ContextErrorException(ex);
            }
        }
        public GodDTO GetByID(int? id)
        {
            try
            {
                GodDTO god = new GodDTO();

                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT GodID,GodName,GodTitle,GodLore,GodClass,GodIcon,GodCardArt FROM god WHERE GodID = ?id", conn);
                    cmd.Parameters.AddWithValue("id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            god.GodID = reader[0] as int? ?? default;
                            god.GodName = reader[1] as string ?? default;
                            god.GodTitle = reader[2] as string ?? default;
                            god.GodLore = reader[3] as string ?? default;
                            god.GodClass = reader[4] as string ?? default;
                            god.GodIcon = reader[5] as byte[] ?? default;
                            god.GodCardArt = reader[6] as byte[] ?? default;
                        }
                    }
                }
                return god;
            }
            catch (Exception ex)
            {
                throw new ContextErrorException(ex);
            }
        }
        //Update
        public void Update(GodDTO entity)
        {
            try
            {
                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE god SET(GodName = ?GodName,GodTitle = ?GodTitle, GodLore = ?GodLore, " +
                                                        "GodClass = ?GodClass, GodIcon = ?GodIcon, GodCardArt = ?GodCardArt) WHERE GodID = ?id", conn);
                    //where id is
                    cmd.Parameters.AddWithValue("id", entity.GodID);
                    //values
                    cmd.Parameters.AddWithValue("GodName", entity.GodName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("GodTitle", entity.GodTitle ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("GodLore", entity.GodLore ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("GodClass", entity.GodClass ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("GodIcon", entity.GodIcon ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("GodCardArt", entity.GodCardArt ?? (object)DBNull.Value);
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
        public void Remove(GodDTO entity)
        {
            try
            {
                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM god WHERE GodID = ?id", conn);
                    //where id =
                    cmd.Parameters.AddWithValue("id", entity.GodID);
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
