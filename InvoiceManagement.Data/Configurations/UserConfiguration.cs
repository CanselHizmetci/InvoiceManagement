using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceManagement.Data.Configurations
{
    internal class UserConfiguration:IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.Surname).IsRequired();
            builder.Property(c => c.Email).IsRequired();
            builder.Property(c => c.PhoneNumber).IsRequired();
            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.HaveAVehicle).IsRequired();
            builder.Property(c => c.IsAdmin).IsRequired();
        }
    }
}
