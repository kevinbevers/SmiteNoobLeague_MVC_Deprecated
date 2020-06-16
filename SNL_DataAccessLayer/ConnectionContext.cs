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
        private string ConnectionString { get;  set; }

        public ConnectionContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        internal MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}
