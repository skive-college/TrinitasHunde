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

        public List<object> getAllLocations()
        {
            List<object> objects = new List<object>();

            using (connection = new SqlConnection(connectionString))
            {
                string sqlString = "SELECT Locations.[ID], Locations.[Name], LocationTypes.[ID] AS TypeID, LocationTypes.[Type], PinTypes.[ID] AS PinTypeID, PinTypes.[Type] AS PinType, Locations.GPSLatitude, Locations.GPSLongitude " +
                                   "FROM Locations, LocationTypes, PinTypes " +
                                   "WHERE Locations.[Type] = LocationTypes.[ID] AND Locations.PinType = PinTypes.[ID];";
                connection.Open();
                command = new SqlCommand(sqlString, connection);
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    objects.Add(new
                    {
                        ID = int.Parse(dataReader["ID"].ToString()),
                        Name = dataReader["Name"].ToString(),
                        LocationTypeID = int.Parse(dataReader["TypeID"].ToString()),
                        LocationType = dataReader["Type"].ToString(),
                        PinTypeID = int.Parse(dataReader["PinTypeID"].ToString()),
                        PinType = dataReader["PinType"].ToString(),
                        GPSLatitude = double.Parse(dataReader["GPSLatitude"].ToString()),
                        GPSLongitude = double.Parse(dataReader["GPSLongitude"].ToString())
                    });
                }
            }
            return objects;
        } // End getAllLocations()

        public List<object> getAllPinTypes()
        {
            List<object> objects = new List<object>();
            using (connection = new SqlConnection(connectionString))
            {
                string sqlString = "SELECT * " +
                                   "FROM PinTypes";
                connection.Open();
                command = new SqlCommand(sqlString, connection);
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    objects.Add(new
                    {
                        ID = int.Parse(dataReader["ID"].ToString()),
                        Name = dataReader["Type"].ToString()
                    });
                }
            }
            return objects;
        } // End getAllPinTypes()

        public List<object> getAllLocationTypes()
        {
            List<object> objects = new List<object>();
            using (connection = new SqlConnection(connectionString))
            {
                string sqlString = "SELECT * " +
                                   "FROM LocationTypes";
                connection.Open();
                command = new SqlCommand(sqlString, connection);
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    objects.Add(new
                    {
                        ID = int.Parse(dataReader["ID"].ToString()),
                        Name = dataReader["Type"].ToString()
                    });
                }
            }
            return objects;
        } // End getAllLocationTypes()

        public int addLocation(string _name, double _gpsLatitude, double _gpsLongitude, int _location, int _pinType)
        {
            int retur = -1;
            using (connection = new SqlConnection(connectionString))
            {
                string sql = "insert into Locations values(@Name, @GPSLatitude, @GPSLongitude, @Location, @PinType)";
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("Name", _name);
                cmd.Parameters.AddWithValue("GPSLatitude", _gpsLatitude);
                cmd.Parameters.AddWithValue("GPSLongitude", _gpsLongitude);
                cmd.Parameters.AddWithValue("Location", _location);
                cmd.Parameters.AddWithValue("PinType", _pinType);
                retur = cmd.ExecuteNonQuery();
            }
            return retur;
        }
    }
}
