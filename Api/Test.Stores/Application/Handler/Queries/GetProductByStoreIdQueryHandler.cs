using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Test.Core.Entities;
using Test.Core.Models;
using Test.Data;
using Test.Core.Data;
namespace StoreAPI.Application.Handler.Queries
{
    public class GetProductByStoreIdQuery : IRequest<IEnumerable<ProductDto>>
    {
        public long StoreId { get; }

        public GetProductByStoreIdQuery(long id)
        {
            StoreId = id;
        }
    }

    public class GetProductByStoreIdQueryHandler : IRequestHandler<GetProductByStoreIdQuery, IEnumerable<ProductDto>>
    {
        private readonly IMapper _mapper;
        IRepository<Product> _repository;
        public GetProductByStoreIdQueryHandler(IRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> Handle(GetProductByStoreIdQuery request, CancellationToken cancellationToken)
        {
            var entities = _repository.Table.AsNoTracking().Where(o => o.StoreId == request.StoreId);
            return await Task.FromResult(_mapper.Map<IEnumerable<ProductDto>>(entities));
        }
    }
}
