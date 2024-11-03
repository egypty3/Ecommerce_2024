using Core.Entities;
using Core.Interfaces;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly EcommerceContext _context;

        public ProductRepository(EcommerceContext context)
        {
            _context = context;
        }


        public async Task<Product> GetProductByIdAsync(int id)
        {
            return _context
                .Products
                .Include(p => p.ProductBrand)
                .Include(p => p.ProductType)
                .FirstOrDefault(p => p.Id == id) 
                ?? throw new NotFoundException("Product not found");

        }

        public async Task<IList<Product>> GetProductsAsync()
        {
            return 
                await _context
                .Products
                .Include(p => p.ProductBrand)
                .Include(p => p.ProductType)
                .ToListAsync();
        }
        public Task<IList<ProductBrand>> GetProductBrandsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IList<ProductType>> GetProductTypesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
