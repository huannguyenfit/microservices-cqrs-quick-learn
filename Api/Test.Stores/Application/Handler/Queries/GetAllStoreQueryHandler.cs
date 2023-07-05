using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Test.Core.Data;
using Test.Core.Entities;
using Test.Core.Models;
using Test.Data;


namespace StoreAPI.Application.Handler.Queries
{
    public class GetAllStoreQuery : IRequest<IEnumerable<StoreDto>>
    {
    }

    public class GetAllStoreQueryHandler : IRequestHandler<GetAllStoreQuery, IEnumerable<StoreDto>>
        {
        private readonly IMapper _mapper;
        IRepository<Store> _repository;
        public GetAllStoreQueryHandler(IRepository<Store> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StoreDto>> Handle(GetAllStoreQuery request, CancellationToken cancellationToken)
        {
            var entities = await Task.FromResult(_repository.Table.AsNoTracking().OrderByDescending(o=> o.Location));
            return _mapper.Map<IEnumerable<StoreDto>>(entities);
        }
    }
}
