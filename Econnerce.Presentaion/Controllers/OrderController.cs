using Ecommerce.Abstruction.IServices;
using Ecommerce.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Econnerce.Presentaion.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class OrderControllerO(IServiceManger serviceManger):ControllerBase
    {
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto Order)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var order= await serviceManger.OrderService.CreateOrderAsync(Order,email);
            return Ok(order);
        }
        [HttpGet("AllOrders")]
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetALlOrdersForUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var orders = await serviceManger.OrderService.GetAllOrdersAsync(email);
            return Ok(orders);
        }
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodDto>>> GetDeliveryMethods()
        {
            var deliveryMethods = await serviceManger.OrderService.GetDeliveryMethodsAsync();
            return Ok(deliveryMethods);
        }
        [HttpGet]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(Guid Orderid)
        {
            var order = await serviceManger.OrderService.GetOrderByIdAsync(Orderid);
            return Ok(order);
        }

    }
}
