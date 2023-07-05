using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Test.Core.Data;
using Test.Core.Entities;

namespace StoreAPI.Application.Handler.Queries
{
    public class CheckMasterDataEnoughQuery : IRequest<bool>
    {

    }

    public class CheckMasterDataEnoughQueryHandler : IRequestHandler<CheckMasterDataEnoughQuery, bool>
    {

        IServiceProvider _serviceProvider;
        public CheckMasterDataEnoughQueryHandler(IServiceProvider
            serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<bool> Handle(CheckMasterDataEnoughQuery request, CancellationToken cancellationToken)
        {
            var transientContext = _serviceProvider.GetRequiredService<ShopDbContext>();
            var productCountTask = transientContext.Set<Product>().CountAsync();

            var transientContext2 = _serviceProvider.GetRequiredService<ShopDbContext>();
            var storeCountTask = transientContext2.Set<Store>().CountAsync();

            var transientContext3 = _serviceProvider.GetRequiredService<ShopDbContext>();
            var customerCountTask = transientContext3.Set<Customer>().CountAsync();

            await Task.WhenAll(storeCountTask, productCountTask, customerCountTask);

            return storeCountTask.Result >= 3 && customerCountTask.Result >= 30 && productCountTask.Result >= 3000;


        }

    }
}
