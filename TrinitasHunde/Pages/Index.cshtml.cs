using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TrinitasHunde.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {

        }

        public void OnPost()
        {
            TrinitasDataAccess.Database DB = new TrinitasDataAccess.Database("Server=planner.aspitweb.dk;Database=TrinitasHunde;user id = aspitlab;password = aspitlab;MultipleActiveResultSets=true");
            //DB.addLocation("test", 54.5, 9.7, 1, 2);
        }
    }
}
