using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TrinitasDataAccess;
using TrinitasHunde.Data;
using TrinitasHunde.Models;

namespace TrinitasHunde.Pages.Admin
{
    public class UsersModel : PageModel
    {
        private readonly ApplicationDbContext dbcontext;

        public List<ApplicationUser> PendingUsers { get; set; }

        public UsersModel(ApplicationDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public void OnGet()
        {
            // Get users with roles
            IQueryable<string> usersWithRoles = dbcontext.UserRoles.Select(userRole => userRole.UserId).Distinct();
            // Get users without any role
            PendingUsers = dbcontext.Users.Where(user =>
                !usersWithRoles.Contains(user.Id)
            ).ToList();
        }

        public void OnPost()
        {
            ;
        }
    }
}