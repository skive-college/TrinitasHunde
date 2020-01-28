using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace TrinitasDataAccess
{
    public class Database
    {
        private readonly string connectionString;
        private SqlConnection connection;
        private SqlCommand command;

        public Database(string _connectionString)
        {
            connectionString = _connectionString;
        }
    }
}
