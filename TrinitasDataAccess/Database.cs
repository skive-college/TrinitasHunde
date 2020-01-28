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

        public int addLocation(string Name, double GPSLatitude, double GPSLongitude, int Location, int PinType)
        {
            int retur = -1;
            using (connection = new SqlConnection(connectionString))
            {
                string sql = "insert into Locations values(@Name, @GPSLatitude, @GPSLongitude, @Location, @PinType)";
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("Name", Name);
                cmd.Parameters.AddWithValue("GPSLatitude", GPSLatitude);
                cmd.Parameters.AddWithValue("GPSLongitude", GPSLongitude);
                cmd.Parameters.AddWithValue("Location", Location);
                cmd.Parameters.AddWithValue("PinType", PinType);
                retur = cmd.ExecuteNonQuery();
            }
            return retur;
        }
    }
}
