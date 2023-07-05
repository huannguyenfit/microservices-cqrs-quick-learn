using AutoMapper;
using System.Net;
using Test.Core.Entities;
using Test.Core.Models;
namespace CustomerAPI.Core.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerViewModel, Customer>();


            CreateMap<Order, OrderDto>().AfterMap((order, s) =>
            {
                s.CustomerName = order.Customer?.FullName ?? "";
                s.Email = order.Customer?.Email ?? "";
                s.StoreName = order.Store?.Name ?? "";
                s.StoreLocation = order.Store?.Location ?? "";
                //always: OrderItem.Length = 1;
                s.ProductName = string.Join(",", order.OrderItems.Select(x => x.Product?.Name ?? "").ToArray());
                s.Price = order.OrderTotal;
            });

        }
    }
}
