using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly EcommerceContext _context;
        public Repository(EcommerceContext context )
        {
            _context = context;
        }
        public async Task<T> GetByIdAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            var query = _context.Set<T>().AsQueryable();

            if (spec.Criteria != null && spec.Criteria.Any())
            {
                foreach (var criteria in spec.Criteria)
                {
                    query = query.Where(criteria);
                }
            }

            foreach (var include in spec.Includes)
            {
                query = query.Include(include);
            }

            if (spec.OrderBy != null)
            {
                if (spec.OrderByDirection == Core.Enums.OrderBy.Ascending)
                {
                    query = query.OrderBy(spec.OrderBy);
                }
                else if (spec.OrderByDirection == Core.Enums.OrderBy.Descending)
                {
                    query = query.OrderByDescending(spec.OrderBy);
                }
            }
            if (spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            return query;
        }
    }
}
