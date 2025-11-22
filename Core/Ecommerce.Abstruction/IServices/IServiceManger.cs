using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Abstruction.IServices
{
    public interface IServiceManger
    {
        public IProductService ProductService { get; }
        public IBasketService BasketService { get; }
        public IAuthenticationService AuthenticationService { get; }
        public IOrderServices OrderService { get; }
    }
}
