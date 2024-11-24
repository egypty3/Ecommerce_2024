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
        public ProductsWithTypesAndBrandsSpecification(string sort)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
            ApplyOrderBy(p => p.Name, Core.Enums.OrderBy.Ascending);

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "priceasc":
                        ApplyOrderBy(p => p.Price, Core.Enums.OrderBy.Ascending);
                        break;
                    case "pricedesc":
                        ApplyOrderBy(p => p.Price, Core.Enums.OrderBy.Descending);
                        break;
                    case "namedesc":
                        ApplyOrderBy(p => p.Name, Core.Enums.OrderBy.Descending);
                        break;
                    default:
                        ApplyOrderBy(p => p.Name, Core.Enums.OrderBy.Ascending);
                        break;
                }
            }
        }
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
