using Core.Entities;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(ISpecification<T> spec);
        Task<IList<T>> ListAsync(ISpecification<T> spec);
    }
}
