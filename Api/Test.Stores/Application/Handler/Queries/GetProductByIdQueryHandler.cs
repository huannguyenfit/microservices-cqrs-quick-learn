using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Test.Core.Entities;
using Test.Core.Models;
using Test.Data;
using Test.Core.Data;
namespace StoreAPI.Application.Handler.Queries
{
    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        public long productId { get; }

        public GetProductByIdQuery(long id)
        {
            productId = id;
        }
    }

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IMapper _mapper;
        IRepository<Product> _repository;
        public GetProductByIdQueryHandler(IRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetByIdAsync(request.productId);
            return _mapper.Map<ProductDto>(entities);
        }
    }
}
