using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrinitasHunde
{
    public static class DataSeeder
    {
        public static void Seed(RoleManager<IdentityRole> roleManager)
        {
            SeedAsync(roleManager).Wait();
        }

        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            await SeedRoles(roleManager);
        }

        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = new string[]{ "Standard", "Admin" };
            foreach (string roleName in roles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    IdentityRole role = new IdentityRole();
                    role.Name = roleName;
                    IdentityResult roleResult = await roleManager.CreateAsync(role);
                }
            }
        }
    }
}
