using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using Test.Core.Data;
using Test.Core.Entities;
using Test.Core.Models;
using Test.Data;

namespace StoreAPI.Application.Handler.Queries
{
    public class AddStoreCommand : IRequest<bool>
    {
        public StoreViewModel ViewModel { get; }
        public AddStoreCommand(StoreViewModel viewModel)
        {
            ViewModel = viewModel;
        }
    }

    public class AddStoreCommandHandler : IRequestHandler<AddStoreCommand, bool>
    {
        IRepository<Store> _repository;
        public AddStoreCommandHandler(IRepository<Store> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(AddStoreCommand request, CancellationToken cancellationToken)
        {
            var viewModel = request.ViewModel;

            //Validation


            //Map
            var entity = new Store()
            {
                Name = viewModel.Name,
                Location = viewModel.Location,
            };

             await _repository.AddAsync(entity);

            return true;
        }
    }
}
