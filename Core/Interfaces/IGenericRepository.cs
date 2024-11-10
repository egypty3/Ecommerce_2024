using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {

        Task<T> GetProductByIdAsync(int id,
            params Expression<Func<T,object>>[] includes);
        Task<IList<T>> GetProductsAsync(
            params Expression<Func<T, object>>[] includes);
        Task<IList<T>> GetProductBrandsAsync();
        Task<IList<T>> GetProductTypesAsync();
    }
}
