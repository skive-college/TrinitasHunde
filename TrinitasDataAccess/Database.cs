﻿using System;
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

        public List<object> getAllObjects()
        {
            List<object> objects = new List<object>();
            using (connection = new SqlConnection(connectionString))
            {
                string sqlString = "SELECT Locations.[Name], LocationTypes.[Type], PinTypes.[Type] AS PinType, Locations.GPSLatitude, Locations.GPSLongitude " +
                                   "FROM Locations, LocationTypes, PinTypes " +
                                   "WHERE Locations.[Type] = LocationTypes.[ID] AND Locations.PinType = PinTypes.[ID];";
                connection.Open();
                command = new SqlCommand(sqlString, connection);
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    objects.Add(new
                    {
                        Name = dataReader["Name"].ToString(),
                        Type = dataReader["Type"].ToString(),
                        PinType = dataReader["PinType"].ToString(),
                        GPSLatitude = double.Parse(dataReader["GPSLatitude"].ToString()),
                        GPSLongitude = double.Parse(dataReader["GPSLongitude"].ToString())
                    });
                }
                return objects;
            }
        } // getAllObjects() end
    }
}
