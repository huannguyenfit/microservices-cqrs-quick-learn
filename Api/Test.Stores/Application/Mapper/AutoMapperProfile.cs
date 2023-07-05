using AutoMapper;
using Test.Core.Entities;
using Test.Core.Models;

namespace StoreAPI.Application.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductDto>().AfterMap((product, s) =>
            {
                s.StoreName = product?.Store?.Name ?? "";
            });

            CreateMap<Store, StoreDto>();

        }
    }
}
