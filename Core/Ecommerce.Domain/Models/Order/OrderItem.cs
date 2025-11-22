using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Models.Order
{
    public class OrderItem:BaseEntity<int>
    {
        public ProductItemOrdered ItemOrdered { get; set; }=null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
