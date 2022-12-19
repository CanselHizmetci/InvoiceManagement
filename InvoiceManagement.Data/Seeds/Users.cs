using InvoiceManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace InvoiceManagement.Data.Seeds
{
    public class Users
    {
        public static async Task SeedUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            //Seed Admin
            var admin = new ApplicationUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                Name = "Yönetici Adı",
                Surname = "Yönetici Soyadı",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                PhoneNumber = "05001112233",
                LicensePlate = "",
                HaveAVehicle = false,
                LockoutEnabled = false
            };

            var user = new ApplicationUser
            {
                UserName = "user@gmail.com",
                Email = "user@gmail.com",
                Name = "Kullanıcı Adı",
                Surname = "Kullanıcı Soyadı",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                PhoneNumber = "05990001122",
                LicensePlate = "34ABC1234",
                HaveAVehicle = true,
                LockoutEnabled = false
            };

            if (!userManager.Users.Any(x => x.Id == user.Id))
            {
                var item = await userManager.FindByEmailAsync(user.Email);
                if (item == null)
                {
                    await userManager.CreateAsync(user, configuration["IdentityServerPasswords:SeedUserPassword"]);
                    await userManager.AddToRoleAsync(user, Enums.Roles.User.ToString());
                }
            }

            if (userManager.Users.All(u => u.Id != admin.Id))
            {
                var item = await userManager.FindByEmailAsync(admin.Email);
                if (item == null)
                {
                    await userManager.CreateAsync(admin, configuration["IdentityServerPasswords:SeedAdminPassword"]);
                    await userManager.AddToRoleAsync(admin, Enums.Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(admin, Enums.Roles.User.ToString());
                }

            }
        }
    }
}
