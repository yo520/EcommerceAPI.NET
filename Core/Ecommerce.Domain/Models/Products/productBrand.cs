using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Models.Products
{
    public class productBrand:BaseEntity<int>
    {
       public string Name { get; set; } = null!;
    }
}
