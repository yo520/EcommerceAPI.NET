using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Exceptions
{
    public class DeliveryMethodNotFoundException(int id):NotFoundException($"Delivery Method Wiht id {id} not Found")
    {
    }
}
