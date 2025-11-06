using AutoMapper;
using Ecommerce.Domain.Models.Products;
using Ecommerce.Shared.Dtos;
using Microsoft.Extensions.Configuration;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.MappingProfile
{
    public class PicureUrlResolve(IConfiguration configuration) : IValueResolver<product, ProductDto, string>
    {
        public string Resolve(product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            string url = string.Empty;
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                 url= $"{configuration.GetSection("urls")["BaseUrl"]}{source.PictureUrl}";
            }
            return url;
        }
    }
}
