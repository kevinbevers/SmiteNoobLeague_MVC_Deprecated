using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SNL_PersistenceLayer.Entities;

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
        public List<Team> GetAllTeams()
        {
            List<Team> list = new List<Team>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from teams", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Team team = new Team
                        {
                            TeamName = reader["TeamName"].ToString()
                        };

                        list.Add(team);
                    }
                }
            }
            return list;
        }
        public Team GetTeam(int id)
        {
            Team team = new Team();

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
        //end of class
    }
}
