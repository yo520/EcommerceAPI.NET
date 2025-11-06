using Ecommerce.Abstruction.IServices;
using Ecommerce.Shared.Dtos;
using Ecommerce.Domain.Contracts.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Domain.Models.Products;
using AutoMapper;

namespace Ecommerce.Domain.UOf
{
    public class ProductService (IUnitOfWork unitOfWork,IMapper mapper) : IProductService
    {
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var brandRepo = unitOfWork.GetRepository<productBrand, int>();
            var brands = await brandRepo.GetAllAsync();
           var BrandDto=mapper.Map<IEnumerable<productBrand>,IEnumerable<BrandDto>>(brands);
            return BrandDto;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductAsync()
        {
           var productRepo=unitOfWork.GetRepository<product, int>();
            var products=await  productRepo.GetAllAsync();
            var productDto=mapper.Map<IEnumerable<product>, IEnumerable<ProductDto>>(products);
            return productDto;
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var typeRepo = unitOfWork.GetRepository<productType, int>();
            var types = await typeRepo.GetAllAsync();
            var typeDto = mapper.Map<IEnumerable<productType>, IEnumerable<TypeDto>>(types);
            return typeDto;
        }

        public async Task<IEnumerable<ProductDto>> GetProductByIdAsync(int id)
        {
            var productRepo = unitOfWork.GetRepository<product, int>();
            var Product = await productRepo.GetByIdAsync(id);
            var productDto = mapper.Map<product, ProductDto>(Product);
            return (IEnumerable<ProductDto>)productDto;

        }

    }
}
