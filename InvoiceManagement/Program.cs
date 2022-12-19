using InvoiceManagement.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using InvoiceManagement.Data.Context;
using InvoiceManagement.Service.Abstracts;
using Microsoft.Extensions.Configuration;

namespace InvoiceManagement
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host=CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                var _configuration = services.GetRequiredService<IConfiguration>();
                try
                {
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    var context = services.GetRequiredService<AppDbContext>();
                    await Data.Seeds.Roles.SeedRolesAsync(userManager, roleManager);
                    await Data.Seeds.Users.SeedUserAsync(userManager, roleManager, _configuration);
                    await Data.Seeds.CreditCards.SeedCreditCard();
                    await Data.Seeds.Blocks.SeedBlocks(context);
                    await Data.Seeds.ApartmentTypes.SeedApartmentTypes(context);
                    await Data.Seeds.Apartments.SeedApartments(context);
                    await Data.Seeds.Debts.SeedDebts(context);
                    await Data.Seeds.InvoiceTypes.SeedInvoiceTypes(context);
                    await Data.Seeds.Invoices.SeedInvoices(context);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
