using Core.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(decimal price)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
            SetCriteria(p => p.Price > price);
        }

        public ProductsWithTypesAndBrandsSpecification()
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
           
        }

        public ProductsWithTypesAndBrandsSpecification(int id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
            SetCriteria(p => p.Id == id);
        }
    }
}
