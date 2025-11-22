using Ecommerce.Domain.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Specifications
{
    public class OrderSpecifications:BaseSpecifications<Order,Guid>
    {
        public OrderSpecifications(string email):base(o=>o.UserEmail==email)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.Address);
            AddOrderByDescending(o => o.OrderDate);
        }

        public OrderSpecifications(Guid id) : base(o => o.Id == id)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
        }


    }
}
