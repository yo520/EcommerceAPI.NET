using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.ErrorModels
{
    public class ValidationError
    {
        public string fild { get; set; }
        public IEnumerable<string> errors { get; set; }
    }
}
