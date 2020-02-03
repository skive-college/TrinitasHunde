﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TrinitasHunde.Pages.Admin
{
    public class MapModel : PageModel
    {
        public List<object> Objects { get; set; }
        public List<Pin> Pins { get; set; }

        public MapModel()
        {
        }

        public void OnGet()
        {
            TrinitasDataAccess.Database database = new TrinitasDataAccess.Database(
               @"Data Source=planner.aspitweb.dk;Initial Catalog=trinitashunde;user id = aspitlab; password = aspitlab; Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            Objects = database.getAllObjects();
            Pins = new List<Pin>();

            //Converts each object in a List of anonymous objects to Pin type object
            foreach (var item in Objects)
            {
                Type type = item.GetType();
                Pin pin = new Pin()
                {
                    //System reflection to get property values from anonymous object type
                    Name = (string)type.GetProperty("Name").GetValue(item),
                    PinType = (string)type.GetProperty("PinType").GetValue(item),
                    LocationType = (string)type.GetProperty("Type").GetValue(item),
                    GPSLatitude = (double)type.GetProperty("GPSLatitude").GetValue(item),
                    GPSLongitude = (double)type.GetProperty("GPSLongitude").GetValue(item)
                };
                //Maps marker layouts > https://sites.google.com/site/gmapsdevelopment/
                if (pin.PinType == "Godkendt")
                {
                    pin.PinColor = "http://maps.google.com/mapfiles/ms/icons/green-dot.png";
                }
                else if (pin.PinType == "Afventer")
                {
                    pin.PinColor = "http://maps.google.com/mapfiles/ms/icons/blue-dot.png";
                }
                else if (pin.PinType == "Afvist")
                {
                    pin.PinColor = "http://maps.google.com/mapfiles/ms/icons/red-dot.png";
                }
                Pins.Add(pin);
            }
        }

        public IActionResult OnPost()
        {
            TrinitasDataAccess.Database DB = new TrinitasDataAccess.Database("Data Source = planner.aspitweb.dk; Initial Catalog = trinitashunde;user id = aspitlab; password = aspitlab; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
            var txtName = Request.Form["Name"];
            double txtGPSLat = double.Parse(Request.Form["GPSLat"], CultureInfo.InvariantCulture);
            double txtGPSLon = double.Parse(Request.Form["GPSLon"], CultureInfo.InvariantCulture);
            int txtLocation = int.Parse(Request.Form["Location"]);
            int txtPinType = int.Parse(Request.Form["PinType"]);
            DB.addLocation(txtName, txtGPSLat, txtGPSLon, txtLocation, txtPinType);

            // Redirect to Get for PRG
            return RedirectToPage();
        }
    }
}