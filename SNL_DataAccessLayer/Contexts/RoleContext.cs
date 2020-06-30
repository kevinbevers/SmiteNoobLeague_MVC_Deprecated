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
    public class Rolecontext : IRoleContext
    {
        private readonly ConnectionContext _con;
        public Rolecontext(ConnectionContext con)
        {
            _con = con;
        }

        //Create
        public int? Add(RoleDTO entity)
        {
            try
            {
                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO role (RoleName,RoleDescription) VALUES(?RoleName,?RoleDescription)", conn);
                    //values
                    cmd.Parameters.AddWithValue("RoleName", entity.RoleName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("RoleDescription", entity.RoleDescription ?? (object)DBNull.Value);
                    //execute command
                    int rowsAffected = cmd.ExecuteNonQuery();
                    //should return if a row is affected or not
                    return rowsAffected;
                }
            }
            catch (Exception ex)
            {
                throw new ContextErrorException(ex);
            }
        }
        //Read
        public IEnumerable<RoleDTO> GetAll()
        {
            try
            {
                List<RoleDTO> roleList = new List<RoleDTO>();

                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT RoleID,RoleName,RoleDescription FROM role", conn);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            RoleDTO role = new RoleDTO
                            {
                                RoleID = reader[0] as int? ?? default,
                                RoleName = reader[1] as string ?? default,
                                RoleDescription = reader[2] as string ?? default,
                            };
                            roleList.Add(role);
                        }
                    }
                }
                return roleList;
            }
            catch (Exception ex)
            {
                throw new ContextErrorException(ex);
            }
        }
        public RoleDTO GetByID(int? id)
        {
            try
            {
                RoleDTO role = new RoleDTO();

                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT RoleID,RoleName,RoleDescription FROM role WHERE RoleID = ?id", conn);
                    cmd.Parameters.AddWithValue("id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            role.RoleID = reader[0] as int? ?? default;
                            role.RoleName = reader[1] as string ?? default;
                            role.RoleDescription = reader[2] as string ?? default;
                        }
                    }
                }
                return role;
            }
            catch (Exception ex)
            {
                throw new ContextErrorException(ex);
            }
        }
        //Update
        public int? Update(RoleDTO entity)
        {
            try
            {
                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE role SET RoleName = ?RoleName, RoleDescription = ?RoleDescription WHERE RoleID = ?id", conn);
                    //where id is
                    cmd.Parameters.AddWithValue("id", entity.RoleID);
                    //values
                    cmd.Parameters.AddWithValue("RoleName", entity.RoleName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("RoleDescription", entity.RoleDescription ?? (object)DBNull.Value);
                    //execute command
                    int rowsAffected = cmd.ExecuteNonQuery();
                    //should return if a row is affected or not
                    return rowsAffected;
                }
            }
            catch (Exception ex)
            {
                throw new ContextErrorException(ex);
            }
        }
        //Delete
        public int? Remove(RoleDTO entity)
        {
            try
            {
                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM role WHERE RoleID = ?id", conn);
                    //where id =
                    cmd.Parameters.AddWithValue("id", entity.RoleID);
                    //execute command
                    int rowsAffected = cmd.ExecuteNonQuery();
                    //should return if a row is affected or not
                    return rowsAffected;
                }
            }
            catch (Exception ex)
            {
                throw new ContextErrorException(ex);
            }
        }
    }
}
