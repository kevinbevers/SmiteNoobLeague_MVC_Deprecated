using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmiteNoobLeague.Models
{
    public class SmiteNoobLeagueContext
    {
        public string ConnectionString { get; set; }

        public SmiteNoobLeagueContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public List<string> GetAllTeams()
        {
            List<string> list = new List<string>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from teams", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(reader["TeamName"].ToString());
                    }
                }
            }

            return list;
        }
        //end of class
    }
}
