using Ecommerce.Abstruction.IServices;
using Ecommerce.Domain.Contracts.Repos;
using Ecommerce.Domain.Contracts.seed;
using Ecommerce.Domain.Contracts.UOW;
using Ecommerce.Persistance.Contexts;
using Ecommerce.Persistance.Repos;
using Ecommerce.Persistance.Seed;
using Ecommerce.Persistance.UOW;
using Ecommerce.Service.MappingProfile;
using Ecommerce.Service.Services;
using Ecommerce.Shared.ErrorModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Ecommerce.Persistance.Identity.Models;

namespace Ecommerce.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            #region DbContext
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });
            builder.Services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection"));
            });
            #endregion

            // Register UnitOfWork so ServiceManger can be constructed
            #region services
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IDataSeeding, DataSeeding>();
            builder.Services.AddScoped<IServiceManger, ServiceManger>();
            builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            #endregion


            builder.Services.AddAutoMapper(m => m.AddProfile(new ProjectProfile(builder.Configuration)));
            builder.Services.Configure<ApiBehaviorOptions>((options) =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var validationErrors = context.ModelState.Where(m => m.Value.Errors.Any())
                        .Select(m => new ValidationError()
                        {
                            fild = m.Key,
                            errors = m.Value.Errors.Select(e => e.ErrorMessage)
                        });
                    var responses = new ValidationErrorToReturn();

                    return new BadRequestObjectResult(responses);
                };
            });
          
            // Use ApplicationUser here so DI can resolve UserManager<ApplicationUser>
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                            .AddEntityFrameworkStores<StoreIdentityDbContext>();

            var app = builder.Build();

            var scope = app.Services.CreateScope();
            var Objectseeding= scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            Objectseeding.SeedData();
            // Ensure identity seeding completes before continuing
            Objectseeding.IdentitySeedData().GetAwaiter().GetResult();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }
            app.UseMiddleware<CustomMiddleWares.CustomExeptionMiddleWare>();
            app.UseHttpsRedirection();


            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}
