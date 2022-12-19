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
    public class ApartmentTypes
    {
        public static async Task SeedApartmentTypes(AppDbContext _context)
        {
            var apartmentType = new ApartmentType
            {
                IsDeleted = false,
                CreateDateTime = DateTime.Now,
                Title = "2+1",
                UpdateDateTime = DateTime.Now
            };
            var apartmentType2 = new ApartmentType
            {
                IsDeleted = false,
                CreateDateTime = DateTime.Now,
                Title = "1+1",
                UpdateDateTime = DateTime.Now
            };
            if (!_context.ApartmentTypes.Any(c =>
                    c.Title == apartmentType.Title))
                await _context.ApartmentTypes.AddAsync(apartmentType);
            if (!_context.ApartmentTypes.Any(c =>
                    c.Title == apartmentType2.Title))
                await _context.ApartmentTypes.AddAsync(apartmentType2);
            await _context.SaveChangesAsync();
        }
    }
}
