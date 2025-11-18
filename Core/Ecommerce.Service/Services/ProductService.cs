using AutoMapper;
using Ecommerce.Abstruction.IServices;
using Ecommerce.Domain.Contracts.UOW;
using Ecommerce.Domain.Exceptions;
using Ecommerce.Domain.Models.Products;
using Ecommerce.Service.Specifications;
using Ecommerce.Shared.Common;
using Ecommerce.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<PaginationResult<ProductDto>> GetAllProductAsync(ProductQueryParams queryParams)
        {
            var specs = new ProductSpeceifications(queryParams);
            var productRepo=unitOfWork.GetRepository<product, int>();
            var products=await  productRepo.GetAllWithSpecificationsAsync(specs);
            var Data=mapper.Map<IEnumerable<product>, IEnumerable<ProductDto>>(products);
            var PageSize=Data.Count();
            var countSpecs = new CountProductSpecification(queryParams);
            var totalCount = await productRepo.GetCountWithSpecificationsAsync(countSpecs);
            return new PaginationResult<ProductDto>(queryParams.PageIndex,PageSize,totalCount,Data);
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
            var specs = new ProductSpeceifications(id);
            var productRepo = unitOfWork.GetRepository<product, int>();
            var Product = await productRepo.GetByIdWithSpecificationsAsync(specs);
            var productDto = mapper.Map<product, ProductDto>(Product);
            if(Product==null)
            {
                throw new ProductNotFound(id);
            }
            return (IEnumerable<ProductDto>)productDto;

        }

    }
}
