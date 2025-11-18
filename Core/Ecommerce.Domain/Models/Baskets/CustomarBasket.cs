using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Models.Baskets
{
    public class CustomarBasket
    {
        public string Id { get; set; }
        public ICollection<BasketItem> Items { get; set; } 
    }
}
