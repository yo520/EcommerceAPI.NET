using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.Dtos
{
    public class BasketitemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string pictureUrl { get; set; } = null!;
        [Range(1,double.MaxValue)]
        public decimal Price { get; set; }
        [Range(1, 200)]
        public int Quantity { get; set; }
    }
}
