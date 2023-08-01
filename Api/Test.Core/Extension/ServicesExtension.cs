
using Microsoft.Extensions.DependencyInjection;
using Test.Core.Entities;
using Test.Data;
using Microsoft.EntityFrameworkCore;
using Test.Core.Data;

namespace Test.Core.Extension
{
    public static class ServiceExtensions 
    {
        public static void AddCoreExtension(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ShopDbContext>(x => x.UseSqlServer(connectionString), ServiceLifetime.Transient, ServiceLifetime.Transient);


            services.AddScoped<IRepository<Product>, EfRepository<Product>>();
            services.AddScoped<IRepository<Store>, EfRepository<Store>>();
            services.AddScoped<IRepository<Customer>, EfRepository<Customer>>();
            services.AddScoped<IRepository<OrderItem>, EfRepository<OrderItem>>();
            services.AddScoped<IRepository<Order>, EfRepository<Order>>();
        }
    }
}
