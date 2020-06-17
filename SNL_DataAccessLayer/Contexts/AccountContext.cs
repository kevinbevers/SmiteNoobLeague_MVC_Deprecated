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
    public class AccountContext : IAccountContext
    {
        private readonly ConnectionContext _con;
        public AccountContext(ConnectionContext con)
        {
            _con = con;
        }

        //Create
        public void Add(AccountDTO entity)
        {
            try
            {
                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO player (AccountName, AccountEmail,AccountPassword,PlayerID) " +
                        "                               VALUES(?AccountName,?AccountEmail,?AccountPassword,?PlayerID)", conn);
                    //values
                    cmd.Parameters.AddWithValue("AccountName", entity.AccountName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("AccountEmail", entity.AccountEmail ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("AccountPassword", entity.AccountPassword ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("PlayerID", entity.PlayerID ?? (object)DBNull.Value);
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
        public IEnumerable<AccountDTO> GetAll()
        {
            try
            {
                List<AccountDTO> accountList = new List<AccountDTO>();

                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT AccountID,AccountName,AccountEmail,AccountPassword,PlayerID FROM account", conn);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AccountDTO account = new AccountDTO
                            {
                                AccountID = reader[0] as int? ?? default,
                                AccountName = reader[1] as string ?? default,
                                AccountEmail = reader[2] as string ?? default,
                                AccountPassword = reader[3] as string ?? default,
                                PlayerID = reader[4] as int? ?? default,
                            };
                            accountList.Add(account);
                        }
                    }
                }
                return accountList;
            }
            catch (Exception ex)
            {
                throw new ContextErrorException(ex);
            }
        }
        public AccountDTO GetByID(int? id)
        {
            try
            {
                AccountDTO account = new AccountDTO();

                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT AccountID,AccountName,AccountEmail,AccountPassword,PlayerID FROM account WHERE AccountID = ?id", conn);
                    cmd.Parameters.AddWithValue("id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            account.AccountID = reader[0] as int? ?? default;
                            account.AccountName = reader[1] as string ?? default;
                            account.AccountEmail = reader[2] as string ?? default;
                            account.AccountPassword = reader[3] as string ?? default;
                            account.PlayerID = reader[4] as int? ?? default;
                        }
                    }
                }
                return account;
            }
            catch (Exception ex)
            {
                throw new ContextErrorException(ex);
            }
        }
        //Update
        public void Update(AccountDTO entity)
        {
            try
            {
                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE account SET(AccountName = ?AccountName," +
                                                        "AccountEmail = ?AccountEmail, AccountPassword = ?AccountPassword, PlayerID = ?PlayerID) WHERE AccountID = ?id", conn);
                    //where id is
                    cmd.Parameters.AddWithValue("id", entity.AccountID);
                    //values
                    cmd.Parameters.AddWithValue("AccountName", entity.AccountName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("AccountEmail", entity.AccountEmail ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("AccountPassword", entity.AccountPassword ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("PlayerID", entity.PlayerID ?? (object)DBNull.Value);
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
        public void Remove(AccountDTO entity)
        {
            try
            {
                using (MySqlConnection conn = _con.GetConnection())
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM account WHERE AccountID = ?id", conn);
                    //where id =
                    cmd.Parameters.AddWithValue("id", entity.AccountID);
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
