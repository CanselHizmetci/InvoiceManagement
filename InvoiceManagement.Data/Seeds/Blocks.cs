using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceManagement.Data.Context;
using InvoiceManagement.Domain.Entities;

namespace InvoiceManagement.Data.Seeds
{
    public class Blocks
    {
        public static async Task SeedBlocks(AppDbContext _context)
        {
            var block = new Block
            {
                IsDeleted = false,
                CreateDateTime = DateTime.Now,
                Title = "A-1",
                UpdateDateTime = DateTime.Now
            };
            var block2 = new Block
            {
                IsDeleted = false,
                CreateDateTime = DateTime.Now,
                Title = "A-2",
                UpdateDateTime = DateTime.Now
            };
            if (!_context.Blocks.Any(c =>
                    c.Title == block.Title))
                await _context.Blocks.AddAsync(block);
            if (!_context.Blocks.Any(c =>
                    c.Title == block2.Title))
                await _context.Blocks.AddAsync(block2);
            await _context.SaveChangesAsync();
        }
    }
}
