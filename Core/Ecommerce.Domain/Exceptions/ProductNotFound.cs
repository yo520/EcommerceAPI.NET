using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Exceptions
{
    public class ProductNotFound(int Id): NotFoundException($"Product with id {Id} Not Found")
    {
    }
}
