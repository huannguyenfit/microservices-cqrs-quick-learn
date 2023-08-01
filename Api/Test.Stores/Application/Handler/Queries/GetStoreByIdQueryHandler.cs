using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Test.Core.Entities;
using Test.Core.Models;
using Test.Data;
using Test.Core.Data;

namespace StoreAPI.Application.Handler.Queries
{
    public class GetStoreByIdQuery : IRequest<StoreDto>
    {
        public long storeId { get; }

        public GetStoreByIdQuery(long id)
        {
            storeId = id;
        }
    }

    public class GetStoreByIdQueryHandler : IRequestHandler<GetStoreByIdQuery, StoreDto>
    {
        private readonly IMapper _mapper;
        IRepository<Store> _repository;
        public GetStoreByIdQueryHandler(IRepository<Store> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<StoreDto> Handle(GetStoreByIdQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.Table.Where(o => o.Id == request.storeId).Include(p=> p.Products).FirstOrDefaultAsync();
            return _mapper.Map<StoreDto>(entities);
        }
    }
}
