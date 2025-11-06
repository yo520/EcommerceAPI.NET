using Ecommerce.Abstruction.IServices;
using Ecommerce.Domain.Contracts.seed;
using Ecommerce.Domain.Contracts.UOW;
using Ecommerce.Persistance.Contexts;
using Ecommerce.Persistance.Seed;
using Ecommerce.Persistance.UOW;
using Ecommerce.Service.MappingProfile;
using Ecommerce.Service.Services;
using Microsoft.EntityFrameworkCore;

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

            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Register UnitOfWork so ServiceManger can be constructed
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IDataSeeding, DataSeeding>();
            builder.Services.AddAutoMapper(m => m.AddProfile(new ProjectProfile(builder.Configuration)));
            builder.Services.AddScoped<IServiceManger, ServiceManger>();

            var app = builder.Build();

            var scope = app.Services.CreateScope();
            var Objectseeding= scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            Objectseeding.SeedData();
                

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();


            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}
