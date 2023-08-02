using Grpc.Core;
using Test.NotificationGrpcService;

namespace Test.NotificationGrpcService.Services
{
    public class NotificationService : NotificationGrpcService.NotificationGrpcServiceBase
    {
        private readonly ILogger<NotificationService> _logger;
        public NotificationService(ILogger<NotificationService> logger)
        {
            _logger = logger;
        }

        public async override Task<OrderCreatedResponse> SendNotifyCustomer(OrderCreatedRequest request, ServerCallContext context)
        {
            //add to queue or send email directly
            //do something.
            _logger.LogInformation("Received");
            //return
            return await Task.FromResult(new OrderCreatedResponse()
            {
                Success = true
            });
        }

    }
}
