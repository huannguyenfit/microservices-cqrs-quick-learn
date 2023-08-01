using MediatR;
using Microsoft.EntityFrameworkCore;
using Test.Core.Data;
using Test.Core.Entities;
using Test.Core.Models;

namespace OrderAPI.Application.Handler.Commands
{
    public class PlaceOrderCommand: IRequest<bool>
    {
        public OrderViewModel ViewModel { get; }
        public PlaceOrderCommand(OrderViewModel viewModel)
        {
            ViewModel = viewModel;
        }
    }

    public class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand, bool>
    {
        IRepository<Product> _productRepository;
        IRepository<Order> _repository;
        public PlaceOrderCommandHandler(IRepository<Order> repository, IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
            _repository = repository;
        }

        public async Task<bool> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
        {
            var viewModel = request.ViewModel;

            //Validation
            var product = await _productRepository.Table.FirstOrDefaultAsync(o => o.Id == viewModel.ProductId);

            //Map
            var entity = new Order()
            {
                CustomerId = viewModel.CustomerId,
                OrderTotal = viewModel.OrderPaid,
                StoreId = product?.StoreId ?? 0
            };

            entity.OrderItems.Add(new OrderItem()
            {
                ProductId = viewModel.ProductId,
                Quantity = 1,
                Price = viewModel.OrderPaid
            });

            await _repository.AddAsync(entity);
            return true;
        }
    }
}
