using InvoiceManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceManagement.Data.Configurations
{
    public class PaymentConfiguration:IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(c => c.CreditCardNo).IsRequired();
            builder.Property(c => c.PaymentDate).IsRequired();
            builder.Property(c => c.PaymentStatus).IsRequired();

        }
    }
}
