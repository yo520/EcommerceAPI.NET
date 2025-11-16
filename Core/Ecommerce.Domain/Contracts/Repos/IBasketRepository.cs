using Ecommerce.Domain.Models.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Contracts.Repos
{
    public interface IBasketRepository
    {
        Task<CustomarBasket?> GetBasketAsync (string Key);
        Task<CustomarBasket?> CreateUpdateBasketAsync (CustomarBasket basket,TimeSpan? TimeToLive=null);
        Task<bool> DeleteBasketAsync (string Key);
    }
}
