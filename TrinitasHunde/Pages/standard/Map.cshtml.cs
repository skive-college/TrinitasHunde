using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TrinitasHunde.Pages.Admin
{
    public class MapModel : PageModel
    {
        public void OnGet()
        {

        }
        [HttpPost]
        public void OnPostIndex()
        {

        }

        public void OnPost()
        {
            TrinitasDataAccess.Database DB = new TrinitasDataAccess.Database("Data Source = planner.aspitweb.dk; Initial Catalog = trinitashunde;user id = aspitlab; password = aspitlab; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
            var txtName = Request.Form["Name"];
            var txtGPSLat = Request.Form["GPSLat"];
            var txtGPSLon = Request.Form["GPSLon"];
            var txtLocation = Request.Form["Location"];
            var txtPinType = Request.Form["PinType"];

            DB.addLocation(txtName, double.Parse(txtGPSLat), double.Parse(txtGPSLon), int.Parse(txtLocation), int.Parse(txtPinType));



        }
    }
}