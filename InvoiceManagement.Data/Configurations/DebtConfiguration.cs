using InvoiceManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceManagement.Data.Configurations
{
    public class DebtConfiguration:IEntityTypeConfiguration<Debt>
    {
        public void Configure(EntityTypeBuilder<Debt> builder)
        {
            builder.Property(c => c.Title).IsRequired();
            builder.Property(c => c.Amount).IsRequired().HasPrecision(18,2);
            builder.Property(c => c.IsPaid).IsRequired();
            builder.Property(c => c.DueTime).IsRequired();
        }
    }
}
