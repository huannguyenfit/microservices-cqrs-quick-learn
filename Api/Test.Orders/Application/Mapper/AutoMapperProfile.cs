using AutoMapper;
using System.Net;
using Test.Core.Entities;
using Test.Core.Models;
namespace OrderAPI.Core.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Order, OrderDto>();
        }
    }
}
