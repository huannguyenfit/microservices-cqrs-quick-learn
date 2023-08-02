using Microsoft.AspNetCore.Mvc;
using System.Net;
using MediatR;
using Test.Core.Models;
using OrderAPI.Application.Handler.Commands;
using Grpc.Net.Client;
using Test.NotificationGrpcService;
namespace OrderAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly GrpcChannel _channel;
        private readonly NotificationGrpcService.NotificationGrpcServiceClient _gRpcCall;

        public OrderController(IMediator mediator, IConfiguration _configuration)
        {
            _mediator = mediator;
           _channel =  GrpcChannel.ForAddress(_configuration.GetValue<string>("GrpcSettings:NotificationServiceUrl"));
            _gRpcCall = new NotificationGrpcService.NotificationGrpcServiceClient(_channel);
        }


        [HttpPost("PlaceOrder")]
        [ProducesResponseType(typeof(IEnumerable<OrderViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderViewModel viewModel)
        {
            // var response = await _mediator.Send(new PlaceOrderCommand(viewModel));

           var response = await _gRpcCall.SendNotifyCustomerAsync(new OrderCreatedRequest()
            {
                Email = "test@gmail.com",
                ProductName = "Product Test"
            });
            return Ok(true);
        }
    }
}

