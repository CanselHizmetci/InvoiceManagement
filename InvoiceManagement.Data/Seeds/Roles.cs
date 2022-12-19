using System.Linq;
using InvoiceManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace InvoiceManagement.Data.Seeds
{
    public class Roles
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            if (!roleManager.Roles.Any(c => c.Name == Enums.Roles.Admin.ToString()))
                await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Admin.ToString()));
            if (!roleManager.Roles.Any(c => c.Name == Enums.Roles.User.ToString()))
                await roleManager.CreateAsync(new IdentityRole(Enums.Roles.User.ToString()));
        }
    }
}
