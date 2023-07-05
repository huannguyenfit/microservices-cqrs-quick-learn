using Microsoft.AspNetCore.Mvc;
using System.Net;
using MediatR;
using Test.Core.Models;
using CustomerAPI.Application.Handler.Queries;

namespace CustomerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {

        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }
   
        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(IEnumerable<OrderDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(int skip, int take)
        {
            var response = await _mediator.Send(new GetAllQuery());
            return Ok(response);
        }

        [HttpGet("GetOrderByCustomerId")]
        [ProducesResponseType(typeof(IEnumerable<OrderDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrderByCustomerId(int id)
        {
            var response = await _mediator.Send(new GetOrderByCustomerIdQuery(id));
            return Ok(response);
        }
        [HttpPost("Add")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Add([FromBody] CustomerViewModel viewModel)
        {
            var response = await _mediator.Send(new AddCustomerCommand(viewModel));
            return Ok(response);
        }

        [HttpGet("GetById")]
        [ProducesResponseType(typeof(ProductDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _mediator.Send(new GetCustomerByIdQuery(id));
            return Ok(response);
        }



    }
}
