using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Products;
using Shared;

namespace Services.Specifications
{
    public class ProductCountSpesification : BaseSpecifications<Product, int>
    {
      
        public ProductCountSpesification(ProductQueryParams productQuery)
            :base (p => (!productQuery.BrandId.HasValue || p.BrandId == productQuery.BrandId)
                     && (!productQuery.TypeId.HasValue || p.TypeId == productQuery.TypeId)
                     && (string.IsNullOrEmpty(productQuery.SearchValue) || p.Name.ToLower().Contains(productQuery.SearchValue.ToLower())))
        {
            
        }

        //{
        //    AddInclude(p => p.Brand);
        //    AddInclude(p => p.Type);

        //    switch
        //        (productQuery.SortingOptions)
        //    {
        //        case ProductSortingOptions.NameAsc:
        //            AddOrderBy(p => p.Name);
        //            break;
        //        case ProductSortingOptions.NamdDesc:
        //            AddOrderByDesc(p => p.Name);
        //            break;

        //        case ProductSortingOptions.PriceAsc:
        //            AddOrderBy(p => p.Price);
        //            break;
        //        case ProductSortingOptions.PeiceDesc:
        //            AddOrderByDesc(p => p.Price);
        //            break;

        //        default:
        //            break;
        //    }

        //    ApllyPagination(productQuery.PageSize, productQuery.PageIndex);
        // }
    }
}
