using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrinitasHunde
{
    public static class DataSeeder
    {
        public static void Seed(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config)
        {
            SeedAsync(userManager, roleManager, config).Wait();
        }

        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config)
        {
            await SeedRoles(roleManager);
            await SeedSuperUser(userManager, config);
        }

        private static async Task SeedSuperUser(UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            // Prepare SuperUser
            ApplicationUser user = new ApplicationUser {
                UserName = config["SuperUser:Email"],
                Email = config["SuperUser:Email"]
            };

            // If SuperUser doesn't exist
            if (await userManager.FindByNameAsync(user.UserName) == null)
            {
                // Create SuperUser
                IdentityResult result = await userManager.CreateAsync(user, config["SuperUser:Password"]);
                // Add SuperUser to Admin role
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }

        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            // Specify roles to seed
            string[] roles = new string[]{ "Standard", "Admin" };
            // Iterate over each role
            foreach (string roleName in roles)
            {
                // If role doesn't exist
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    // Create role
                    IdentityRole role = new IdentityRole() { Name = roleName };
                    IdentityResult roleResult = await roleManager.CreateAsync(role);
                }
            }
        }
    }
}
