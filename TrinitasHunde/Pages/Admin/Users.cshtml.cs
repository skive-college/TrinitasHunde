using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Threading.Tasks;

namespace TrinitasHunde.Pages.Admin
{
    public class UsersModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;

        public ILookup<Role, ApplicationUser> UserLookup { get; set; }

        public UsersModel(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public void OnGet()
        {
            UserLookup = Utilities.getRoleUserLookup(userManager);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ApplicationUser user = await userManager.FindByEmailAsync(Request.Form["email"]);
            if (user != null)
            {
                switch (Request.Form["submit"].ToString().ToLowerInvariant())
                {
                    case "accept":
                        await userManager.AddToRoleAsync(user, "Standard");
                        break;

                    case "decline":
                    case "delete":
                        await userManager.DeleteAsync(user);
                        break;

                    case "promote":
                        if (Utilities.getRole(User) >= Role.SuperUser)
                        {
                            await userManager.AddToRoleAsync(user, "Admin");
                        }
                        break;

                    case "demote":
                        if (Utilities.getRole(User) >= Role.SuperUser)
                        {
                            await userManager.RemoveFromRoleAsync(user, "Admin");
                        }
                        break;

                    default:
                        // Invalid post, do nothing
                        break;
                }
            }

            // Redirect to Get for PRG
            return RedirectToPage();
        }
    }
}