using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(InvoiceManagement.Areas.Identity.IdentityHostingStartup))]
namespace InvoiceManagement.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}