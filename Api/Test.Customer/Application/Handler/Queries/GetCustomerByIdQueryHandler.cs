using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Test.Core.Entities;
using Test.Core.Models;
using Test.Data;
using Test.Core.Data;
namespace CustomerAPI.Application.Handler.Queries
{
    public class GetCustomerByIdQuery : IRequest<CustomerDto>
    {
        public long CustomerId { get; }

        public GetCustomerByIdQuery(long id)
        {
            CustomerId = id;
        }
    }

    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
    {
        private readonly IMapper _mapper;
        IRepository<Customer> _repository;
        public GetCustomerByIdQueryHandler(IRepository<Customer> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CustomerDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetByIdAsync(request.CustomerId);
            return _mapper.Map<CustomerDto>(entities);
        }
    }
}
