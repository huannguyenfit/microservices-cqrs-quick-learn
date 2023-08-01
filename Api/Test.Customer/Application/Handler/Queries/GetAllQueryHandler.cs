using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Test.Core.Data;
using Test.Core.Entities;
using Test.Core.Models;
using Test.Data;

namespace CustomerAPI.Application.Handler.Queries
{
    public class GetAllQuery : IRequest<IEnumerable<CustomerDto>>
    {
    }

    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, IEnumerable<CustomerDto>>
        {
        private readonly IMapper _mapper;
        IRepository<Customer> _repository;
        public GetAllQueryHandler(IRepository<Customer> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerDto>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var entities = await Task.FromResult(_repository.Table.AsNoTracking().OrderBy(customer => customer.Email));
            return _mapper.Map<IEnumerable<CustomerDto>>(entities);
        }
    }
}
