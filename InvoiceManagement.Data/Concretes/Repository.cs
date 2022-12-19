
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvoiceManagement.Data.Abstract;
using InvoiceManagement.Data.Context;
using InvoiceManagement.Domain;
using InvoiceManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvoiceManagement.Data.Concretes
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<T>> Get()
        {
            var datas = _context.Set<T>().Where(c => !c.IsDeleted);
            //foreach (var propertyInfo in typeof(T).GetProperties().Where(c => c.PropertyType.BaseType == typeof(BaseEntity) || c.PropertyType == typeof(ApplicationUser)))
            //{
            //    datas = datas.Include(propertyInfo.Name);
            //}

            return datas;
        }

        public async Task<T> GetById(int id)
        {
            return await (await Get()).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task Add(T entity)
        {
            entity.CreateDateTime = DateTime.Now;
            entity.UpdateDateTime = DateTime.Now;
            // entitynin propertylerini getir
            foreach (var property in entity.GetType().GetProperties()
                         .Where(c =>
                             // ilgili Property ApplicationUser ise
                             c.PropertyType == typeof(ApplicationUser) ||
                             // ilgili Property Entity ise
                             c.PropertyType.BaseType == typeof(BaseEntity) ||
                             // ilgili Property Entity Collection ise
                             c.PropertyType == typeof(ICollection<BaseEntity>)))
            {
                // dbdeki entitynin ilgili property değerinin formdan gelen değerle vdeğiştir
                property?.SetValue(entity, null);
            }
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(int id)
        {
            var item = await _context.Set<T>().FirstOrDefaultAsync(c => c.Id == id);
            item.IsDeleted = true;
            item.UpdateDateTime = DateTime.Now;
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, T entity)
        {
            var e = await _context.Set<T>().FindAsync(id);
            if (e == null) return;
            // dbden gelen entitynin propertylerini getir
            foreach (var property in e.GetType().GetProperties()
                         .Where(c =>
                         // ilgili Property db propertysi olmalı
                         c.PropertyType == typeof(int) ||
                         c.PropertyType== typeof(string) ||
                         c.PropertyType== typeof(decimal) ||
                         c.PropertyType== typeof(DateTime) ||
                         c.PropertyType== typeof(bool)))
            {
                // dbdeki entitynin ilgili property değerinin formdan gelen değerle değiştir
                property?.SetValue(e, entity.GetType().GetProperty(property.Name)?.GetValue(entity, null));
            }
            entity.UpdateDateTime = DateTime.Now;

            await _context.SaveChangesAsync();
        }
    }
}
