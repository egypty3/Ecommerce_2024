using Core.Entities;
using Core.Interfaces;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly EcommerceContext _context;
        public GenericRepository(EcommerceContext context)
        {
            _context = context;
        }
        public async Task<IList<T>> GetProductBrandsAsync()
        {
           return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetProductByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            var entity = await query.FirstOrDefaultAsync(e => e.Id == id);

            if (entity == null)
            {
                throw new NotFoundException($"{ typeof(T).Name } not found");
            }
            return entity;
        }

        public async Task<IList<T>> GetProductsAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.ToListAsync();
        }

        public async Task<IList<T>> GetProductTypesAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
    }
}
