using Ecommerce.Abstruction.IServices;
using Ecommerce.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Econnerce.Presentaion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IServiceManger serviceManger):ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProduct()
        { 
            var products = await serviceManger.ProductService.GetAllProductAsync();
            return Ok(products);

        }

        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrands()
        {
            var brands=await serviceManger.ProductService.GetAllBrandsAsync();
            return Ok(brands);
        }
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetAllTypes()
        {
            var types = await serviceManger.ProductService.GetAllTypesAsync();
            return Ok(types);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetAllProductById(int id)
        {
            var types = await serviceManger.ProductService.GetProductByIdAsync(id);
            return Ok(types);
        }
    }
}
