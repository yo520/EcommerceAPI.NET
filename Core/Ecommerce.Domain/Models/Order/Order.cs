using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Models.Order
{
    public class Order:BaseEntity<Guid>
    {
        public Order()
        {
            
        }
        public Order(string userEmail, OrderAddress address, DeliveryMethod deliveryMethod, ICollection<OrderItem> orderItems, decimal subtotal)
        {
            UserEmail = userEmail;
            Address = address;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            Subtotal = subtotal;
        }

        public string UserEmail { get; set; }=null!;
        public OrderAddress Address { get; set; }=null!;
        [ForeignKey("DeliveryMethod")]
        public int DeliveryMethodId { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }=null!;
        public ICollection<OrderItem> OrderItems { get; set; }=[];
        public decimal Subtotal { get; set; }
        public DateTimeOffset OrderDate { get; set; }=DateTimeOffset.Now;
        public OrderStatus Status { get; set; }
        public decimal GetTotal()
        {
            return Subtotal + DeliveryMethod.Price;
        }
    }
}
