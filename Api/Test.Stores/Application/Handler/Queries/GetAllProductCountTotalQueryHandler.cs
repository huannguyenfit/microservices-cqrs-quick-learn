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
    public class GetAllProductCountTotalQuery : IRequest<int>
    {
        public string Keyword { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 100;
        public GetAllProductCountTotalQuery(string keyword, decimal? minPrice, decimal? maxPrice, int skip = 0, int take = 100)
        {
            Keyword = keyword;
            MinPrice = minPrice;
            MaxPrice = maxPrice;
            Skip = skip;
            Take = take;
        }
    }

    public class GetAllProductCountTotalQueryHandler : IRequestHandler<GetAllProductCountTotalQuery, int>
        {
        private readonly IMapper _mapper;
        IRepository<Product> _repository;
        public GetAllProductCountTotalQueryHandler(IRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(GetAllProductCountTotalQuery request, CancellationToken cancellationToken)
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

            var count = await query.CountAsync();

            return count;
        }

    }
}
