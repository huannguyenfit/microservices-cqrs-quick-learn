using Microsoft.AspNetCore.Mvc;
using System.Net;
using MediatR;
using Test.Core.Models;
using OrderAPI.Application.Handler.Commands;

namespace OrderAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {

        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }
   
   
        [HttpPost("PlaceOrder")]
        [ProducesResponseType(typeof(IEnumerable<OrderViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderViewModel viewModel)
        {
            var response = await _mediator.Send(new PlaceOrderCommand(viewModel));
            return Ok(response);
        }
    }
}
