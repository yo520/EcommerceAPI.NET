using Ecommerce.Domain.Models.Products;
using Ecommerce.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Specifications
{
    public class CountProductSpecification:BaseSpecifications<product,int>
    {
        public CountProductSpecification(ProductQueryParams queryParams)
            : base(p => (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId) && (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId)
            && (string.IsNullOrEmpty(queryParams.SearchValu) || p.Name.ToLower().Contains(queryParams.SearchValu.ToLower())))
        {
            
        }
    }
}
