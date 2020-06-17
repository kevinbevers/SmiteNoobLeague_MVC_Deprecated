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
    public class ItemContext : IContext<ItemDTO>
    {
        private readonly ConnectionContext _con;
        public ItemContext(ConnectionContext con)
        {
            _con = con;
        }

        //Create
        public void Add(ItemDTO entity)
        {
            try
            {
                List<string> ItemStats = entity.ItemStats.ToList();

                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO item (ItemID,ItemName,ItemIcon,ItemDescription,ItemShortDescription,ItemPrice,ItemStat1,ItemStat2,ItemStat3,ItemStat4) " +
                                                            "VALUES(?ItemID,?ItemName,?ItemIcon,?ItemDescription,?ItemShortDescription,?ItemPrice,?ItemStat1,?ItemStat2,?ItemStat3,?ItemStat4)", conn);

                    //values
                    cmd.Parameters.AddWithValue("ItemID", entity.ItemID);
                    cmd.Parameters.AddWithValue("ItemName", entity.ItemName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("ItemIcon", entity.ItemIcon ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("ItemDescription", entity.ItemDescription ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("ItemShortDescription", entity.ItemShortDescription ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("ItemPrice", entity.ItemPrice);
                    cmd.Parameters.AddWithValue("ItemStat1", ItemStats[0] ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("ItemStat2", ItemStats[1] ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("ItemStat3", ItemStats[2] ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("ItemStat4", ItemStats[3] ?? (object)DBNull.Value);
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
        public void AddMultiple(List<ItemDTO> entityList)
        {
            try
            {
                //build a string with all the values in it.
                StringBuilder sCommand = new StringBuilder("INSERT INTO item (ItemID,ItemName,ItemIcon,ItemDescription," +
                                                            "ItemShortDescription,ItemPrice,ItemStat1,ItemStat2,ItemStat3,ItemStat4) VALUES ");

                List<string> Rows = new List<string>();
                foreach (var entity in entityList)
                {
                    List<string> ItemStats = entity.ItemStats.ToList();

                    Rows.Add(string.Format("('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')",
                        entity.ItemID,
                        entity.ItemName ?? (object)DBNull.Value,
                        entity.ItemIcon ?? (object)DBNull.Value,
                        entity.ItemDescription ?? (object)DBNull.Value,
                        entity.ItemShortDescription ?? (object)DBNull.Value,
                        entity.ItemPrice ?? (object)DBNull.Value,
                        ItemStats[0] ?? (object)DBNull.Value,
                        ItemStats[1] ?? (object)DBNull.Value,
                        ItemStats[2] ?? (object)DBNull.Value,
                        ItemStats[3] ?? (object)DBNull.Value));
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
            catch(Exception ex)
            {
                throw new ContextErrorException(ex);
            }
        }
        //Read
        public IEnumerable<ItemDTO> GetAll()
        {
            try
            {
                List<ItemDTO> itemList = new List<ItemDTO>();

                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT ItemID,ItemName,ItemIcon,ItemDescription,ItemShortDescription,ItemPrice,ItemStat1,ItemStat2,ItemStat3,ItemStat4 FROM item", conn);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ItemDTO item = new ItemDTO
                            {
                                ItemID = reader[0] as int? ?? default,
                                ItemName = reader[1] as string ?? default,
                                ItemIcon = reader[2] as byte[] ?? default,
                                ItemDescription = reader[3] as string ?? default,
                                ItemShortDescription = reader[4] as string ?? default,
                                ItemPrice = reader[5] as int? ?? default,
                                ItemStats = new List<string>
                                {
                                    reader[6] as string ?? default,
                                    reader[7] as string ?? default,
                                    reader[8] as string ?? default,
                                    reader[9] as string ?? default,
                                },
                        };
                            itemList.Add(item);
                        }
                    }
                }
                return itemList;
            }
            catch (Exception ex)
            {
                throw new ContextErrorException(ex);
            }
        }
        public ItemDTO GetByID(int? id)
        {
            try
            {
                ItemDTO item = new ItemDTO();

                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT ItemID,ItemName,ItemIcon,ItemDescription,ItemShortDescription,ItemPrice,ItemStat1,ItemStat2,ItemStat3,ItemStat4 FROM item WHERE ItemID = ?id", conn);
                    cmd.Parameters.AddWithValue("id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            item.ItemID = reader[0] as int? ?? default;
                            item.ItemName = reader[1] as string ?? default;
                            item.ItemIcon = reader[2] as byte[] ?? default;
                            item.ItemDescription = reader[3] as string ?? default;
                            item.ItemShortDescription = reader[4] as string ?? default;
                            item.ItemPrice = reader[5] as int? ?? default;
                            item.ItemStats = new List<string> 
                                {
                                    reader[6] as string ?? default, 
                                    reader[7] as string ?? default, 
                                    reader[8] as string ?? default,
                                    reader[9] as string ?? default,
                                };
                        }
                    }
                }
                return item;
            }
            catch (Exception ex)
            {
                throw new ContextErrorException(ex);
            }
        }
        //Update
        public void Update(ItemDTO entity)
        {
            List<string> ItemStats = entity.ItemStats.ToList();

            try
            {
                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE account SET(ItemName = ?ItemName,ItemIcon = ?ItemIcon, ItemDescription = ?ItemDescription," +
                                                        "ItemShortDescription = ?ItemShortDescription, ItemPrice = ?ItemPrice, ItemStat1 = ?ItemStat1, ItemStat2 = ?ItemStat2, ItemStat3 = ?ItemStat3, ItemStat4 = ?ItemStat4) WHERE ItemID = ?id", conn);
                    //where id is
                    cmd.Parameters.AddWithValue("id", entity.ItemID);
                    //values
                    cmd.Parameters.AddWithValue("ItemName", entity.ItemName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("ItemIcon", entity.ItemIcon ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("ItemDescription", entity.ItemDescription ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("ItemShortDescription", entity.ItemShortDescription ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("ItemPrice", entity.ItemPrice);
                    cmd.Parameters.AddWithValue("ItemStat1", ItemStats[0] ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("ItemStat2", ItemStats[1] ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("ItemStat3", ItemStats[2] ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("ItemStat4", ItemStats[3] ?? (object)DBNull.Value);

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
        public void Remove(ItemDTO entity)
        {
            try
            {
                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM item WHERE ItemID = ?id", conn);
                    //where id =
                    cmd.Parameters.AddWithValue("id", entity.ItemID);
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
