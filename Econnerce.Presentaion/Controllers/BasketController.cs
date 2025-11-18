using Ecommerce.Abstruction.IServices;
using Ecommerce.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Econnerce.Presentaion.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class BasketController(IServiceManger serviceManger) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetBasket(string Key )
        {
            var basket = await serviceManger.BasketService.GetBasketAsync(Key);
            return Ok(basket);

        }
        [HttpPost]
        public async Task<ActionResult> CreateOrUpdateBasket([FromBody] BasketDto basket)
        {
            var updatedBasket = await serviceManger.BasketService.CreateOrUpdateBasketAsync(basket);
            return Ok(updatedBasket);
        }
        [HttpDelete("{Key}")]
        public async Task<ActionResult<bool>> DeleteBasket(string Key)
        {
            var result= await serviceManger.BasketService.DeleteBasketAsync(Key);
            return Ok(result);
        }

    }
}
