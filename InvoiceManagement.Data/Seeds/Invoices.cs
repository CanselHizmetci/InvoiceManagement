using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceManagement.Data.Context;
using InvoiceManagement.Domain.Entities;

namespace InvoiceManagement.Data.Seeds
{
    public class Invoices
    {
        public static async Task SeedInvoices(AppDbContext context)
        {
            var invoice = new Invoice
            {
                IsDeleted = false,
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now,
                InvoiceTypeId = context.InvoiceTypes.FirstOrDefault().Id,
                Amount = 200,
                DueTime = DateTime.Now.AddMonths(1),
                InvoiceNumber = "1",
                InvoiceReadDate = DateTime.Now
            };
            var invoice2 = new Invoice
            {
                IsDeleted = false,
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now,
                InvoiceTypeId = context.InvoiceTypes.FirstOrDefault().Id,
                Amount = 124,
                DueTime = DateTime.Now.AddMonths(1),
                InvoiceNumber = "2",
                InvoiceReadDate = DateTime.Now
            };
            if (!context.Invoices.Any(c =>
                    c.InvoiceNumber == invoice.InvoiceNumber && c.InvoiceTypeId == invoice.InvoiceTypeId))
                await context.Invoices.AddAsync(invoice);
            if (!context.Invoices.Any(c =>
                    c.InvoiceNumber == invoice2.InvoiceNumber && c.InvoiceTypeId == invoice2.InvoiceTypeId))
                await context.Invoices.AddAsync(invoice2);
            await context.SaveChangesAsync();
        }
    }
}
