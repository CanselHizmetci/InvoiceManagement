using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceManagement.Data.Context;
using InvoiceManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvoiceManagement.Data.Seeds
{
    public class InvoiceTypes
    {
        public static async Task SeedInvoiceTypes(AppDbContext context)
        {
            var invoiceType = new InvoiceType
            {
                IsDeleted = false,
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now,
                Title = "Water"
            };
            var invoiceType2 = new InvoiceType
            {
                IsDeleted = false,
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now,
                Title = "electric"
            };
            if (!context.InvoiceTypes.Any(c =>
                    c.Title == invoiceType.Title))
                await context.InvoiceTypes.AddAsync(invoiceType);
            if (!context.InvoiceTypes.Any(c =>
                    c.Title == invoiceType2.Title))
                await context.InvoiceTypes.AddAsync(invoiceType2);
            await context.SaveChangesAsync();
        }
    }
}
