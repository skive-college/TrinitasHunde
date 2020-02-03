using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrinitasDataAccess;

namespace TrinitasHunde.Pages.Admin
{
    public class MapModel : PageModel
    {

        public List<Pin> Pins { get; set; }
        public List<PinType> PinTypes { get; set; }
        public List<LocationType> LocationTypes { get; set; }

        public MapModel()
        {
            TrinitasDataAccess.Database database = new TrinitasDataAccess.Database(
               @"Data Source=planner.aspitweb.dk;Initial Catalog=trinitashunde;user id = aspitlab; password = aspitlab; Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            // Gets all Locations
            getLocations(database);

            // Gets all LocationTypes
            getLocationTypes(database);

            // Gets all PinTypes
            getPinTypes(database);
        }

        private void getPinTypes(Database _database)
        {
            List<object> objects = _database.getAllPinTypes();
            PinTypes = new List<PinType>();

            // Converts each object in a List of anonymous objects to Pin type object
            foreach (var item in objects)
            {
                Type type = item.GetType();
                PinType pinType = new PinType()
                {
                    // System reflection to get property values from anonymous object type
                    ID = (int)type.GetProperty("ID").GetValue(item),
                    Name = (string)type.GetProperty("Name").GetValue(item)
                };
                PinTypes.Add(pinType);
            }
        } // End getPinTypes(Database)

        public void getLocationTypes(Database _database)
        {
            List<object> objects = _database.getAllLocationTypes();
            LocationTypes = new List<LocationType>();

            // Converts each object in a List of anonymous objects to Pin type object
            foreach (var item in objects)
            {
                Type type = item.GetType();
                LocationType locationType = new LocationType()
                {
                    // System reflection to get property values from anonymous object type
                    ID = (int)type.GetProperty("ID").GetValue(item),
                    Name = (string)type.GetProperty("Name").GetValue(item)
                };
                LocationTypes.Add(locationType);
            }
        } // End getLocationTypes(Database)

        public void getLocations(Database _database)
        {
            List<object> objects = _database.getAllLocations();
            Pins = new List<Pin>();

            // Converts each object in a List of anonymous objects to Pin type object
            foreach (var item in objects)
            {
                Type type = item.GetType();
                Pin pin = new Pin()
                {
                    // System reflection to get property values from anonymous object type
                    ID = (int)type.GetProperty("ID").GetValue(item),
                    Name = (string)type.GetProperty("Name").GetValue(item),
                    TypePin = new PinType()
                    {
                        ID = (int)type.GetProperty("PinTypeID").GetValue(item),
                        Name = (string)type.GetProperty("PinType").GetValue(item)
                    },
                    TypeLocation = new LocationType()
                    {
                        ID = (int)type.GetProperty("LocationTypeID").GetValue(item),
                        Name = (string)type.GetProperty("LocationType").GetValue(item)
                    },
                    GPSLatitude = (double)type.GetProperty("GPSLatitude").GetValue(item),
                    GPSLongitude = (double)type.GetProperty("GPSLongitude").GetValue(item)
                };
                // Maps marker layouts > https://sites.google.com/site/gmapsdevelopment/
                // if (Pin type = Approved) > pinColor = green
                if (pin.TypePin.ID == 1)
                {
                    pin.PinColor = "http://maps.google.com/mapfiles/ms/icons/green-dot.png";
                }
                // if (Pin type = Awaiting) > pinColor = blue
                else if (pin.TypePin.ID == 2)
                {
                    pin.PinColor = "http://maps.google.com/mapfiles/ms/icons/blue-dot.png";
                }
                // if (Pin type == Denied) > pinColor = red
                else if (pin.TypePin.ID == 3)
                {
                    pin.PinColor = "http://maps.google.com/mapfiles/ms/icons/red-dot.png";
                }
                Pins.Add(pin);
            }
        } // End getLocations(Database)
        public void OnGet()
        {

        }

        [BindProperty]
        public int Number { get; set; }

        public List<SelectListItem> Options { get; set; }
        public void OnPost()
        {
            Options = new List<SelectListItem>();

            TrinitasDataAccess.Database DB = new TrinitasDataAccess.Database("Data Source = planner.aspitweb.dk; Initial Catalog = trinitashunde;user id = aspitlab; password = aspitlab; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
            var txtName = Request.Form["Name"];
            double txtGPSLat = double.Parse( Request.Form["GPSLat"],CultureInfo.InvariantCulture);
            double txtGPSLon = double.Parse( Request.Form["GPSLon"],CultureInfo.InvariantCulture);
            int txtLocation = int.Parse(Request.Form["Location"]);
            int txtPinType = int.Parse(Request.Form["PinType"]);

            DB.addLocation(txtName, txtGPSLat, txtGPSLon, txtLocation, txtPinType);



        }
    }
}