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
    public class AddProductCommand : IRequest<bool>
    {
        public ProductViewModel ViewModel { get; }
        public AddProductCommand(ProductViewModel viewModel)
        {
            ViewModel = viewModel;
        }
    }

    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, bool>
    {
        IRepository<Product> _repository;
        public AddProductCommandHandler(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var viewModel = request.ViewModel;

            //Validation


            //Map
            var entity = new Product()
            {
                Name = viewModel.Name,
                DisplayName = viewModel.Name,
                Price = viewModel.Price,
                StoreId = viewModel.StoreId,
                Quantity = viewModel.Quantity
            };

             await _repository.AddAsync(entity);

            return true;
        }
    }
}
