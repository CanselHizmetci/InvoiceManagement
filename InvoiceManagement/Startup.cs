using System;
using Hangfire;
using Hangfire.SqlServer;
using InvoiceManagement.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using InvoiceManagement.Data.Abstract;
using InvoiceManagement.Data.Concretes;
using InvoiceManagement.Domain.Entities;
using InvoiceManagement.Service.Abstracts;
using InvoiceManagement.Service.Concretes;
using InvoiceManagement.Service.Mapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using InvoiceManagement.Service.Services;

namespace InvoiceManagement
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("AppDbContextConStr")).UseLazyLoadingProxies());
            services.AddIdentity<ApplicationUser, IdentityRole>(
                //    c =>
                //{
                //    c.Password.RequireDigit = false;
                //    c.Password.RequiredLength = 4;
                //    c.Password.RequireNonAlphanumeric = false;
                //    c.Password.RequireUppercase = false;
                //}
                    )
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IApartmentService, ApartmentService>();
            services.AddScoped<IApartmentTypeService, ApartmentTypeService>();
            services.AddScoped<IBlockService, BlockService>();
            services.AddScoped<IDebtService, DebtService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IInvoiceTypeService, InvoiceTypeService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<DebtReminderService>();
            services.AddScoped<IHangfireService, HangfireService>();
            services.AddSingleton<ILoggerService, ConsoleLogger>();
            services.AddHangfire(c =>
                c.UseSqlServerStorage(Configuration.GetConnectionString("AppDbContextConStr")));
            
            services.AddHangfireServer();
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHangfireService hangfireService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHangfireDashboard("/jobs");

            
                hangfireService.Run();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
