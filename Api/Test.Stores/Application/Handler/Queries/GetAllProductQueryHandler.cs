using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Test.Core.Data;
using Test.Core.Domain.Models;
using Test.Core.Entities;
using Test.Core.Models;
using Test.Data;

namespace StoreAPI.Application.Handler.Queries
{
    public class GetAllProductQuery : IRequest<PagingData<ProductDto>>
    {
        public string Keyword { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 100;
        public GetAllProductQuery(string keyword, decimal? minPrice, decimal? maxPrice, int skip = 0, int take = 100)
        {
            Keyword = keyword;
            MinPrice = minPrice;
            MaxPrice = maxPrice;
            Skip = skip;
            Take = take;
        }
    }

    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, PagingData<ProductDto>>
        {
        private readonly IMapper _mapper;
        IRepository<Product> _repository;
        public GetAllProductQueryHandler(IRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PagingData<ProductDto>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Keyword))
            {
                request.Keyword = "";
            }

            var query = _repository.Table.AsNoTracking();

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.ApplyLike(request.Keyword, x => x.Name);
            }

            if (request.MinPrice > 0 || request.MaxPrice > 0)
            {

                query = query.Where(product =>
               (request.MinPrice > 0 && request.MaxPrice > 0 && request.MinPrice <= product.Price && product.Price <= request.MaxPrice) ||

               (request.MinPrice > 0 && (request.MaxPrice == null) && request.MinPrice <= product.Price) ||

               (request.MinPrice == null && (request.MaxPrice > 0) && request.MaxPrice >= product.Price));
            }

            var queryTask = await query.OrderByDescending(o => o.Price).Skip(request.Skip)
                           .Take(request.Take).Select(o => new ProductDto
                           {
                               Id = o.Id,
                               Name = o.Name,
                               Price = o.Price,
                               StoreName = o.Store != null ? o.Store.Name : "",
                               Quantity = o.Quantity
                           }).ToListAsync();


            return new PagingData<ProductDto>() { Data = queryTask, Total = 0 };
        }

    }
}
