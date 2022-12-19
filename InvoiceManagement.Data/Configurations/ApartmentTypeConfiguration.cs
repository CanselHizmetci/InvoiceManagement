using InvoiceManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceManagement.Data.Configurations
{
    public class ApartmentTypeConfiguration:IEntityTypeConfiguration<ApartmentType>
    {
        public void Configure(EntityTypeBuilder<ApartmentType> builder)
        {
            builder.Property(c => c.Title).IsRequired();
        }
    }
}
