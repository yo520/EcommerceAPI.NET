using AutoMapper;
using Ecommerce.Abstruction.IServices;
using Ecommerce.Domain.Contracts.Repos;
using Ecommerce.Domain.Contracts.UOW;
using Ecommerce.Domain.Exceptions;
using Ecommerce.Domain.Models.Order;
using Ecommerce.Domain.Models.Products;
using Ecommerce.Persistance.Identity.Models;
using Ecommerce.Service.Specifications;
using Ecommerce.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Services
{
    public class OrderService(IMapper mapper,IBasketRepository basketRepository,IUnitOfWork unitOfWork) : IOrderServices
    {
        public async Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto, string Email)
        {
            var OrderAddress = mapper.Map<AddressDto, OrderAddress>(orderDto.Address);
            var Basket=await basketRepository.GetBasketAsync(orderDto.BasketId)?? throw new BasketNotFoundException(orderDto.BasketId);
            List<OrderItem> orderItems = [];
            var ProductRepository=unitOfWork.GetRepository<product,int>();
            foreach (var item in Basket.Items)
            {
                var product=await ProductRepository.GetByIdAsync(item.Id) ?? throw new ProductNotFound(item.Id);
                var OrderItem=new OrderItem()
                {
                  ItemOrdered=new ProductItemOrdered()
                  {
                    ProductId=product.Id,
                    ProductName=product.Name,
                    PictureUrl=product.PictureUrl
                  },
                  Quantity=item.Quantity,
                    Price=product.Price
                };
              
                orderItems.Add(OrderItem);
            }   
            var DeliveryMethod=await unitOfWork.GetRepository<DeliveryMethod,int>().GetByIdAsync(orderDto.DeliveryMethodId) 
                ?? throw new DeliveryMethodNotFoundException(orderDto.DeliveryMethodId);
            var subtotal=orderItems.Sum(item=>item.Price*item.Quantity);
            var order=new Order()
            {
              UserEmail=Email,
              OrderItems=orderItems,
              Address=OrderAddress,
              DeliveryMethod=DeliveryMethod,
              Subtotal=subtotal
            };
            unitOfWork.GetRepository<Order,Guid>().Add(order);
            await unitOfWork.SaveChaingesAsync();
            return mapper.Map<Order,OrderToReturnDto>(order);
        }

        public async Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string Email)
        {
            var specs = new OrderSpecifications(Email);
            var order=await unitOfWork.GetRepository<Order,Guid>().GetAllWithSpecificationsAsync(specs);
            return mapper.Map<Task<IEnumerable<Order>>,Task<IEnumerable<OrderToReturnDto>>>(order);

        }

        public async Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync()
        {
            var deliveryMethods=await unitOfWork.GetRepository<DeliveryMethod,int>().GetAllAsync();
            return mapper.Map<IEnumerable<DeliveryMethod>,IEnumerable<DeliveryMethodDto>>(deliveryMethods);
        }

        public Task<OrderToReturnDto> GetOrderByIdAsync(Guid id)
        {
            var specs = new OrderSpecifications(id);
            var order=unitOfWork.GetRepository<Order,Guid>().GetByIdWithSpecificationsAsync(specs);
            return mapper.Map<Task<Order>,Task<OrderToReturnDto>>(order);
        }
    }
}
