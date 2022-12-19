using InvoiceManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceManagement.Data.Configurations
{
    public class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
    {
        public void Configure(EntityTypeBuilder<Apartment> builder)
        {
            builder.Property(c => c.Status).IsRequired();
            builder.Property(c => c.Floor).IsRequired();
            builder.Property(c => c.ApartmentNumber).IsRequired();

        }
    }
}
