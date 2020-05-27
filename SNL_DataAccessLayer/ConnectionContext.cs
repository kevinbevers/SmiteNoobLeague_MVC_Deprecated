using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace SNL_PersistenceLayer
{
    public class ConnectionContext
    {
        public string ConnectionString { get; set; }

        public ConnectionContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}
