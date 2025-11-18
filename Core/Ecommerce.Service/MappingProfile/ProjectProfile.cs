using AutoMapper;
using Ecommerce.Domain.Models.Baskets;
using Ecommerce.Domain.Models.Products;
using Ecommerce.Persistance.Identity.Models;
using Ecommerce.Shared.Dtos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.MappingProfile
{
    public class ProjectProfile:Profile
    {
        public ProjectProfile(IConfiguration configuration)
        {
            CreateMap<product,ProductDto>()
                .ForMember(dest=>dest.brandname,options=>options.MapFrom(src=>src.Brand.Name))
                .ForMember(dest=>dest.typeName,options=>options.MapFrom(src=>src.Type.Name))
                .ForMember(dest=>dest.pictureUrl,options=>options.MapFrom(new PicureUrlResolve(configuration)));

            CreateMap<productBrand, BrandDto>();
            CreateMap<productType, TypeDto>();
            CreateMap<CustomarBasket,BasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketitemDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();


        }
    }
}
