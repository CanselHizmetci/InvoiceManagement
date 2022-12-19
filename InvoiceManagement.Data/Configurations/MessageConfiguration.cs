using InvoiceManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceManagement.Data.Configurations
{
    public class MessageConfiguration:IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.Property(c => c.Content).IsRequired();
            builder.Property(c => c.Title).IsRequired();
            builder.Property(c => c.SendDate).IsRequired();
            builder.Property(c => c.IsReaded).IsRequired();

        }
    }
}
