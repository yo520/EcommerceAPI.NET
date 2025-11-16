using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.Dtos
{
    public class BasketDto
    {
        public string Id { get; set; } = null!;
        public ICollection<BasketitemDto> Items { get; set; } 
    }
}
