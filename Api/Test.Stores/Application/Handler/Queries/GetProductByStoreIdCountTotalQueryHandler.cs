using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Test.Core.Entities;
using Test.Core.Models;
using Test.Data;
using Test.Core.Data;
using Test.Core.Domain.Models;

namespace StoreAPI.Application.Handler.Queries
{
    public class GetProductByStoreIdCountTotalQuery : IRequest<int>
    {
        public long StoreId { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }

        public GetProductByStoreIdCountTotalQuery(long id, int skip, int take)
        {
            StoreId = id;
            Skip = skip;
            Take = take;
        }
    }

    public class GetProductByStoreIdCountTotalQueryHandler : IRequestHandler<GetProductByStoreIdCountTotalQuery, int>
    {
        IServiceProvider _serviceProvider;
        private readonly IMapper _mapper;
        IRepository<Product> _repository;
        public GetProductByStoreIdCountTotalQueryHandler(IRepository<Product> repository, IMapper mapper, IServiceProvider
            serviceProvider)
        {
            _repository = repository;
            _mapper = mapper;
            _serviceProvider = serviceProvider;

        }

        public async Task<int> Handle(GetProductByStoreIdCountTotalQuery request, CancellationToken cancellationToken)
        {
            var query = _repository.Table.AsNoTracking().Where(o => o.StoreId == request.StoreId);
           
            return await query.CountAsync();
        }
    }
}
