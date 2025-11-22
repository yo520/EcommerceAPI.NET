using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.Dtos
{
    public class OrderToReturnDto
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; } = null!;
        public DateTimeOffset OrderDate { get; set; }
        public AddressDto ShipToAddress { get; set; } = null!;
        public string DeliveryMethod { get; set; } = null!;
        public string Status { get; set; } = null!;
        public ICollection<OrderItemDto> OrderItems { get; set; } = [];
        public decimal Subtotal { get; set; }   
        public decimal Total { get; set; }
    }
}

