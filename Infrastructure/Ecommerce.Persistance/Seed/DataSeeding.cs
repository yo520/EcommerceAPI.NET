using Ecommerce.Domain.Contracts.seed;
using Ecommerce.Domain.Models.Products;
using Ecommerce.Persistance.Contexts;
using Ecommerce.Persistance.Identity.Models;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly StoreIdentityDbContext identityDbContext;

        public DataSeeding(StoreDbContext context,UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager,StoreIdentityDbContext identityDbContext)
        {
            this.Context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.identityDbContext = identityDbContext;
        }

        public async Task IdentitySeedData()
        {
           
          
                if (!roleManager.Roles.Any())
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                    await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }
                if (!userManager.Users.Any())
                {
                    var user1 = new ApplicationUser
                    {
                        Email = "youssif@gmail.com",
                        PhoneNumber = "1234567890",
                        UserName = "YoussifAhmed",
                        DesplayNamr = "youssif"
                    };
                    var user2 = new ApplicationUser
                    {
                        Email = "ahmed@gmail.com",
                        PhoneNumber = "1234567890",
                        UserName = "ahmedhassen",
                        DesplayNamr = "Ahmed"
                    };
                    await userManager.CreateAsync(user1, "P@ssw0rd");
                    await userManager.CreateAsync(user2, "P@ssw0rd");

                    await userManager.AddToRoleAsync(user1, "Admin");
                    await userManager.AddToRoleAsync(user2, "SuperAdmin");


                }
                await identityDbContext.SaveChangesAsync();
            
         

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
