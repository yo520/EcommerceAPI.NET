using Ecommerce.Domain.Contracts.Repos;
using Ecommerce.Domain.Models.Baskets;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecommerce.Persistance.Repos
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task<CustomarBasket?> CreateUpdateBasketAsync(CustomarBasket basket, TimeSpan? TimeToLive = null)
        {
            var jesonBasket = JsonSerializer.Serialize(basket);
            var IsCreated = await _database.StringSetAsync(basket.Id, jesonBasket, TimeToLive?? TimeSpan.FromHours(5));
            if (IsCreated)
                return await GetBasketAsync(basket.Id);
            else
                return null;
        }

        public async Task<bool> DeleteBasketAsync(string Key)
        {
            return await _database.KeyDeleteAsync(Key);
        }

        public async Task<CustomarBasket?> GetBasketAsync(string Key)
        {
            var basket =await _database.StringGetAsync(Key);
            if (basket.IsNullOrEmpty)
                return null;
            return JsonSerializer.Deserialize<CustomarBasket>(basket);

        }
    }
}
