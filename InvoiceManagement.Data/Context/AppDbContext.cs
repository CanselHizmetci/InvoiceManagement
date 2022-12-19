using InvoiceManagement.Data.Configurations;
using InvoiceManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InvoiceManagement.Data.Context
{
    public class AppDbContext: IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<InvoiceType> InvoiceTypes { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Debt> Debts { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Block> Blocks { get; set; }
        public DbSet<ApartmentType> ApartmentTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var IdentitySchema = "Identity";
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "User",schema:IdentitySchema);
            });

            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role", schema: IdentitySchema);
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles", schema: IdentitySchema);
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims", schema: IdentitySchema);
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins", schema: IdentitySchema);
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims", schema: IdentitySchema);

            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens", schema: IdentitySchema);
            });
            builder.ApplyConfiguration(new ApartmentConfiguration());
            builder.ApplyConfiguration(new ApartmentTypeConfiguration());
            builder.ApplyConfiguration(new BlockConfiguration());
            builder.ApplyConfiguration(new DebtConfiguration());
            builder.ApplyConfiguration(new InvoiceConfiguration());
            builder.ApplyConfiguration(new InvoiceTypeConfiguration());
            builder.ApplyConfiguration(new MessageConfiguration());
            builder.ApplyConfiguration(new PaymentConfiguration());
        }
    }
}
