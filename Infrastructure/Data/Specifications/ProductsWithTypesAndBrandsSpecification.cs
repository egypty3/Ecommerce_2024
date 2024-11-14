using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecificatio<Product>
    {
        public ProductsWithTypesAndBrandsSpecification()
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
            ApplyCriteria(p => p.Price > 42000);
        }


        public ProductsWithTypesAndBrandsSpecification(int id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }
    }
}
