using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public  interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<IList<Product>> GetProductsAsync();
        Task<IList<ProductBrand>> GetProductBrandsAsync();
        Task<IList<ProductType>> GetProductTypesAsync();
    }
}
