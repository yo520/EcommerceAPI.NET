using Ecommerce.Domain.Contracts.seed;
using Ecommerce.Domain.Models.Products;
using Ecommerce.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecommerce.Persistance.Seed
{
    public class DataSeeding : IDataSeeding
    {
        private readonly StoreDbContext Context;

        public DataSeeding(StoreDbContext context)
        {
            this.Context = context;
        }
        public void SeedData()
        {
            if(Context.Database.GetPendingMigrations().Any())
            {
                Context.Database.Migrate();
            }

            if(!Context.ProductBrands.Any())
            {
                var productBrandsData=File.ReadAllText(@"..\Infrastructure\Ecommerce.Persistance\Data\brands.json");
                var productBrands = JsonSerializer.Deserialize<List<productBrand>>(productBrandsData);
                if(productBrands!=null && productBrands.Any())
                {
                  Context.ProductBrands.AddRange(productBrands);
                }
            }

            if (!Context.ProductTypes.Any())
            { 
                var productTypesData=File.ReadAllText(@"..\Infrastructure\Ecommerce.Persistance\Data\types.json");
                var productTypes = JsonSerializer.Deserialize<List<productType>>(productTypesData);
                if (productTypes != null && productTypes.Any())
                {
                    Context.ProductTypes.AddRange(productTypes);
                }

            }
            if (!Context.Products.Any())
            {
                var productsData = File.ReadAllText(@"..\Infrastructure\Ecommerce.Persistance\Data\products.json");
                var products = JsonSerializer.Deserialize<List<product>>(productsData);
                if (products != null && products.Any())
                {
                    Context.Products.AddRange(products);
                }
            }
            Context.SaveChanges();
        }
    }
}
