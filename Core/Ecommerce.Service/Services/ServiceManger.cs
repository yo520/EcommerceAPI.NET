using AutoMapper;
using Ecommerce.Abstruction.IServices;
using Ecommerce.Domain.Contracts.UOW;
using Ecommerce.Domain.UOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Services
{
    public class ServiceManger(IMapper mapper,IUnitOfWork unitOfWork) : IServiceManger
    {
        private readonly Lazy<IProductService> LazyproductService =new Lazy<IProductService>(()=>new ProductService(unitOfWork,mapper));
        public IProductService ProductService => LazyproductService.Value;
    }
}
