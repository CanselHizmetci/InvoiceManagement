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
    public class Debts
    {
        public static async Task SeedDebts(AppDbContext context)
        {
            var debt = new Debt
            {
                IsDeleted = false,
                CreateDateTime = DateTime.Now,
                IsPaid = false,
                Amount = 40,
                ApartmentId = context.Apartments.FirstOrDefault().Id,
                DueTime = DateTime.Now,
                Title = "TestDebt",
                UpdateDateTime = DateTime.Now
            };
            var debt2 = new Debt
            {
                IsDeleted = false,
                CreateDateTime = DateTime.Now,
                IsPaid = false,
                Amount = 80,
                ApartmentId = context.Apartments.FirstOrDefault().Id,
                DueTime = DateTime.Now,
                Title = "TestDebtTitle",
                UpdateDateTime = DateTime.Now
            };

            if (!context.Debts.Any(c =>
                    c.Title == debt.Title&&c.ApartmentId==debt.ApartmentId))
                await context.Debts.AddAsync(debt);
            if (!context.Debts.Any(c =>
                    c.Title == debt2.Title && c.ApartmentId == debt2.ApartmentId))
                await context.Debts.AddAsync(debt2);
            await context.SaveChangesAsync();
        }
    }
}
