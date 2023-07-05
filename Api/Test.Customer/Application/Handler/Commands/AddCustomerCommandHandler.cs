using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using Test.Core.Data;
using Test.Core.Entities;
using Test.Core.Models;
using Test.Data;

namespace CustomerAPI.Application.Handler.Queries
{
    public class AddCustomerCommand : IRequest<bool>
    {
        public CustomerViewModel ViewModel { get; }
        public AddCustomerCommand(CustomerViewModel viewModel)
        {
            ViewModel = viewModel;
        }
    }

    public class AddCustomerCommandHandler : IRequestHandler<AddCustomerCommand, bool>
    {
        readonly IRepository<Customer> _repository;
        readonly IMapper _mapper;
        public AddCustomerCommandHandler(IRepository<Customer> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
        {
            var viewModel = request.ViewModel;

            //Validation


            //Map
            var entity = _mapper.Map<Customer>(viewModel);
            await _repository.AddAsync(entity);

            return true;
        }
    }
}
