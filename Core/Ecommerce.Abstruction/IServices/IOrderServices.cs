using Ecommerce.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Abstruction.IServices
{
    public interface IOrderServices
    {
        public Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto,string Email);
        public Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync();
        public Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string Email);
        public Task<OrderToReturnDto> GetOrderByIdAsync(Guid id);
    }
}
