using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Exceptions
{
    public class BadRequesException(List<string> errors): Exception(" validation errors occurred.")
    {
        public List<string> Errors  = errors;
    }
}
