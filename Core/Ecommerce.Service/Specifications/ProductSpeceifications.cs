using Ecommerce.Domain.Models.Products;
using Ecommerce.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Specifications
{
    public class ProductSpeceifications: BaseSpecifications<product, int>
    {
        public ProductSpeceifications(ProductQueryParams queryParams)
            :base(p=>(!queryParams.BrandId.HasValue||p.BrandId==queryParams.BrandId)&&(!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId)
            &&(string.IsNullOrEmpty(queryParams.SearchValu)||p.Name.ToLower().Contains(queryParams.SearchValu.ToLower())))
        {
            AddInclude(p => p.Brand);
            AddInclude(p => p.Type);

            switch(queryParams.SortingOption)
            {
                case ProductSortingOptions.name_asc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.name_desc:
                    AddOrderByDescending(p => p.Name);
                    break;
                case ProductSortingOptions.price_asc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.price_desc:
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    AddOrderBy(p => p.Name);
                    break;
            }
            ApplyPaging(queryParams.PageIndex, queryParams.PageSize);

        }
        public ProductSpeceifications(int id) : base(P=>P.Id==id)
        {
            AddInclude(p => p.Brand);
            AddInclude(p => p.Type);

        }
    }
}
