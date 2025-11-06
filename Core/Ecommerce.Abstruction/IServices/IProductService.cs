using Ecommerce.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Abstruction.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductAsync();
        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
        Task<IEnumerable<TypeDto>> GetAllTypesAsync();
        Task<IEnumerable<ProductDto>> GetProductByIdAsync(int id);
    }
}
