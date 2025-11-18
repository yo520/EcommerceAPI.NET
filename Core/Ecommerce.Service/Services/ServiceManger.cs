using AutoMapper;
using Ecommerce.Abstruction.IServices;
using Ecommerce.Domain.Contracts.Repos;
using Ecommerce.Domain.Contracts.UOW;
using Ecommerce.Domain.UOf;
using Ecommerce.Persistance.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Services
{
    public class ServiceManger(IMapper mapper,IUnitOfWork unitOfWork,IBasketRepository basketRepository,UserManager<ApplicationUser> userManager,IConfiguration configuration) : IServiceManger
    {
        private readonly Lazy<IProductService> LazyproductService =new Lazy<IProductService>(()=>new ProductService(unitOfWork,mapper));
        public IProductService ProductService => LazyproductService.Value;

        private readonly Lazy<IBasketService> LazybasketService = new Lazy<IBasketService>(() => new BasketService(basketRepository, mapper));
        public IBasketService BasketService => LazybasketService.Value;

        private readonly Lazy<IAuthenticationService> LazyAuthenticationService = new Lazy<IAuthenticationService>(()=>new AuthenticationService(userManager,configuration,mapper));
        public IAuthenticationService AuthenticationService => LazyAuthenticationService.Value;
    }
}
