using Microsoft.EntityFrameworkCore;
using OMS.Core.Entities;
using OMS.Core.Repositries.Interfaces;
using OMS.Core.Specification;
using OMS.Repositry.Data;
using OMS.Repositry.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Repositry.Repostries
{
    public class GenericRepositry<T> : IGenericRepositry<T> where T : BaseEntity
    {
        private readonly OMSDbContext _context;

        public GenericRepositry(OMSDbContext context) 
        {
            _context = context;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Product))
            {
                return (IEnumerable<T>)await _context.Products.ToListAsync();
            }
            return await _context.Set<T>().ToListAsync();
        }

       
        public async Task<T?> GetAsync(int id)
        {
            if (typeof(T) == typeof(Product))
            {
                return await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync() as T;
            }
            return await _context.Set<T>().FindAsync(id);
        }

        


        public  Task<T?> GetWithSpecAsync(ISpecification<T> spec)
        {
            return  SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), spec).FirstOrDefaultAsync();

        }

        public async Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
            return await SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), spec).ToListAsync();

        }
        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

    }
}
