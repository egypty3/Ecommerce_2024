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
        public ProductsWithTypesAndBrandsSpecification(string sort, int skip, int take,
            int? productTypeId, int? productBrandId, string search, decimal? price)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);

            if (productTypeId.HasValue)
            {
                AddCriteria(p => p.ProductTypeId == productTypeId.Value);
            }

            if (productBrandId.HasValue)
            {
                AddCriteria(p => p.ProductBrandId == productBrandId.Value);
            }

            if (!string.IsNullOrEmpty(search))
            {
                AddCriteria(p =>
                    p.Name.ToLower().Contains(
                        search.ToLower())
                );
            }

            if (price.HasValue)
            {
                AddCriteria(p => p.Price > price.Value);
            }


            if (skip >= 0 && take > 0)
            {
                ApplyPaging(skip, take);
            }

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
            AddCriteria(p => p.Price > price);
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
            AddCriteria(p => p.Id == id);
        }
    }
}
