using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SNL_PersistenceLayer.Entities;
using System.Data;

namespace SNL_PersistenceLayer
{
    public class SmiteNoobLeagueContext
    {
        public string ConnectionString { get; set; }

        public SmiteNoobLeagueContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        #region team
        public List<TeamEntity> GetAllTeams()
        {
            List<TeamEntity> list = new List<TeamEntity>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from teams", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TeamEntity team = new TeamEntity
                        {
                            TeamName = reader["TeamName"].ToString()
                        };

                        list.Add(team);
                    }
                }
            }
            return list;
        }
        public TeamEntity GetTeam(int id)
        {
            TeamEntity team = new TeamEntity();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from team where TeamID = {id}", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        team.TeamID = reader["TeamID"] as int? ?? default;
                        team.TeamName = reader["TeamName"] as string ?? default;
                        team.TeamLogo = reader["TeamLogo"] as byte[] ?? default;
                        team.TeamDivisionID = reader["TeamDivisionID"] as int? ?? default;
                    }
                }
            }
            return team;
        }
        #endregion
        #region generic
        public DataSet GetByID(int id, string table)
        {
            DataSet ds = new DataSet();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = new string($"select * from {table} where {table}ID = {id}");

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                da.Fill(ds, "result");

                return ds;
            }
        }
        public DataSet GetAll(string table)
        {
            DataSet ds = new DataSet();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = new string($"select * from {table}");

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                da.Fill(ds, "result");

                return ds;
            }
        }
        #endregion
        //end of class
    }
}
