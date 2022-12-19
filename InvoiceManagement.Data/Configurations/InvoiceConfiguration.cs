using InvoiceManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceManagement.Data.Configurations
{
    public class InvoiceConfiguration:IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.Property(c => c.InvoiceNumber).IsRequired();
            builder.Property(c => c.Amount).IsRequired().HasPrecision(18,2);
            builder.Property(c => c.InvoiceReadDate).IsRequired();
            builder.Property(c => c.DueTime).IsRequired();
        }
    }
}
