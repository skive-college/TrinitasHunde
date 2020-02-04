using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TrinitasDataAccess;
using TrinitasHunde.Data;
using TrinitasHunde.Models;

namespace TrinitasHunde.Pages.Admin
{
    public class UsersModel : PageModel
    {
        private readonly ApplicationDbContext dbcontext;
        private readonly UserManager<ApplicationUser> userManager;

        public IList<ApplicationUser> PendingUsers { get; set; }
        public IList<ApplicationUser> AdminUsers { get; set; }
        public IList<ApplicationUser> StandardUsers { get; set; }

        public UsersModel(ApplicationDbContext dbcontext, UserManager<ApplicationUser> userManager)
        {
            this.dbcontext = dbcontext;
            this.userManager = userManager;
        }

        public void OnGet()
        {
            // Get users with roles
            IQueryable<string> usersWithRoles = dbcontext.UserRoles.Select(userRole => userRole.UserId).Distinct();
            // Get users without any role
            PendingUsers = dbcontext.Users.Where(user =>
                !usersWithRoles.Contains(user.Id)
            ).ToList();

            AdminUsers = userManager.GetUsersInRoleAsync("Admin").Result;
            StandardUsers = userManager.GetUsersInRoleAsync("Standard").Result;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            switch (Request.Form["submit"].ToString().ToLowerInvariant())
            {
                case "accept":
                    await acceptUser(Request.Form["email"]);
                    break;
                case "decline":
                    await declineUser(Request.Form["email"]);
                    break;
                default:
                    // Invalid post, do nothing
                    break;
            }
            // Redirect to Get for PRG
            return RedirectToPage();
        }

        private async Task acceptUser(string email)
        {
            // Add user to Standard role
            ApplicationUser user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                await userManager.AddToRoleAsync(user, "Standard");
            }
        }

        private async Task declineUser(string email)
        {
            // Delete user
            ApplicationUser user = await userManager.FindByEmailAsync(email);
            if(user != null)
            {
                await userManager.DeleteAsync(user);
            }
        }
    }
}