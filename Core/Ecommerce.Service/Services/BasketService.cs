using AutoMapper;
using Ecommerce.Abstruction.IServices;
using Ecommerce.Domain.Contracts.Repos;
using Ecommerce.Domain.Exceptions;
using Ecommerce.Domain.Models.Baskets;
using Ecommerce.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Services
{
    public class BasketService(IBasketRepository BasketRepository,IMapper mapper) : IBasketService
    {
        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket)
        {
            var basketEntity = mapper.Map<BasketDto, CustomarBasket>(basket);
            var savedBasket = BasketRepository.CreateUpdateBasketAsync(basketEntity);
            if(savedBasket is not null)
            {
                return await GetBasketAsync(basket.Id);
            }
            else
            {
                throw new Exception("Failed to create or update basket.");
            }


        }

        public async Task<bool> DeleteBasketAsync(string Key)
        {
            return await BasketRepository.DeleteBasketAsync(Key);

        }

        public async Task<BasketDto> GetBasketAsync(string Key)
        {
            var basket = await BasketRepository.GetBasketAsync(Key);
            if(basket is not null)
            { return mapper.Map<CustomarBasket,BasketDto>(basket); }
            else
            {
                throw new BasketNotFoundException(Key);
            }

        }
    }
}
