using AutoMapper;
using Ecommerce.Domain.Models.Order;
using Ecommerce.Shared.Dtos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.MappingProfile
{
    public class OrderPictureUrlResolver (IConfiguration configuration) : IValueResolver<OrderItem, OrderItemDto, string>
    {
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            string url = string.Empty;
            if (!string.IsNullOrEmpty(source.ItemOrdered.PictureUrl))
            {
                url = $"{configuration.GetSection("urls")["BaseUrl"]}{source.ItemOrdered.PictureUrl}";
            }
            return url;
        }
    }
}
