using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Test.Core.Data;
using Test.Core.Entities;
using Test.Core.Models;
using Test.Data;

namespace CustomerAPI.Application.Handler.Queries
{
    public class GetOrderByCustomerIdQuery : IRequest<IEnumerable<OrderDto>>
    {
        public long CustomerId { get; }

        public GetOrderByCustomerIdQuery(long id)
        {
            CustomerId = id;
        }
    }

    public class GetOrderByCustomerIdQueryHandler : IRequestHandler<GetOrderByCustomerIdQuery, IEnumerable<OrderDto>>
    {
        private readonly IMapper _mapper;
        IRepository<Order> _repository;
        public GetOrderByCustomerIdQueryHandler(IRepository<Order> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDto>> Handle(GetOrderByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var entities = await Task.FromResult(_repository.Table.Where(o=> o.CustomerId == request.CustomerId).Include(o => o.Customer).Include(o => o.Store).Include(o=> o.OrderItems).ThenInclude(o=> o.Product));
            return _mapper.Map<IEnumerable<OrderDto>>(entities);
        }
    }
}
