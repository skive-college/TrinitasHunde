using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace TrinitasHunde
{
    // Ordered by importance for default comparison
    public enum Role
    {
        Pending,
        Standard,
        Admin,
        SuperUser
    }

    public static class Utilities
    {
        // Returns the highest role of the specified user
        public static Role getRole(ClaimsPrincipal user)
        {
            if (user.IsInRole("SuperUser"))
            {
                return Role.SuperUser;
            }
            if (user.IsInRole("Admin"))
            {
                return Role.Admin;
            }
            if (user.IsInRole("Standard"))
            {
                return Role.Standard;
            }
            return Role.Pending;
        }

        // Create a lookup for each users highest role (can have multiple)
        public static ILookup<Role, ApplicationUser> getRoleUserLookup(UserManager<ApplicationUser> userManager)
        {
            IList<ApplicationUser> superUsers = userManager.GetUsersInRoleAsync("SuperUser").Result;
            IList<ApplicationUser> adminUsers = userManager.GetUsersInRoleAsync("Admin").Result;
            IList<ApplicationUser> standardUsers = userManager.GetUsersInRoleAsync("Standard").Result;

            return userManager.Users.ToLookup(user =>
            {
                if (superUsers.Contains(user))
                {
                    return Role.SuperUser;
                }
                if (adminUsers.Contains(user))
                {
                    return Role.Admin;
                }
                if (standardUsers.Contains(user))
                {
                    return Role.Standard;
                }
                return Role.Pending;
            });
        }
    }
}