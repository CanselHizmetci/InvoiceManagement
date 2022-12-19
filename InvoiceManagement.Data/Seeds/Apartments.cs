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
    public class Apartments
    {
        public static async Task SeedApartments(AppDbContext context)
        {
            var apartment = new Apartment
            {
                IsDeleted = false,
                ApartmentNumber = 4,
                BlockId = context.Blocks.FirstOrDefault().Id,
                UserId = context.Users.FirstOrDefault().Id,
                CreateDateTime = DateTime.Now,
                Floor = 1,
                Status = true,
                TypeId = context.ApartmentTypes.FirstOrDefault().Id,
                UpdateDateTime = DateTime.Now
            };
            var apartment2 = new Apartment
            {
                IsDeleted = false,
                ApartmentNumber = 2,
                BlockId = context.Blocks.FirstOrDefault().Id,
                UserId = context.Users.FirstOrDefault().Id,
                CreateDateTime = DateTime.Now,
                Floor = 3,
                Status = true,
                TypeId = context.ApartmentTypes.FirstOrDefault().Id,
                UpdateDateTime = DateTime.Now
            };
            if (!context.Apartments.Any(c =>
                    c.ApartmentNumber == apartment.ApartmentNumber && c.BlockId == apartment.BlockId))
                await context.Apartments.AddAsync(apartment);
            if (!context.Apartments.Any(c =>
                    c.ApartmentNumber == apartment2.ApartmentNumber && c.BlockId == apartment2.BlockId))
                await context.Apartments.AddAsync(apartment2);
            await context.SaveChangesAsync();
        }
    }
}
