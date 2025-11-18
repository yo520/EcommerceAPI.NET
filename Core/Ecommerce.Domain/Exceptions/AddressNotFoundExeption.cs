using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Exceptions
{
    public class AddressNotFoundExeption(string DesplayName):NotFoundException($"user with name {DesplayName} has no address")
    {
    }
}
