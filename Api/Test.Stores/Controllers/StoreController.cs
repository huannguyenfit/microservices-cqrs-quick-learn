using Microsoft.AspNetCore.Mvc;
using System.Net;
using MediatR;
using StoreAPI.Application.Handler.Queries;
using Test.Core.Models;

namespace StoreAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class StoreController : ControllerBase
    {

        private readonly IMediator _mediator;

        public StoreController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(IEnumerable<StoreDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetAllStoreQuery());
            return Ok(response);
        }

        [HttpGet("GetById")]
        [ProducesResponseType(typeof(StoreDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _mediator.Send(new GetStoreByIdQuery(id));
            return Ok(response);
        }

        [HttpPost("Add")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Add([FromBody] StoreViewModel viewModel)
        {
            var response = await _mediator.Send(new AddStoreCommand(viewModel));
            return Ok(response);
        }


        [HttpGet("GetProductByStoreId")]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProductByStoreId(int id)
        {
            var response = await _mediator.Send(new GetProductByStoreIdQuery(id));
            return Ok(response);
        }
        [HttpGet("GetProductByStoreIdCountTotal")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProductByStoreIdCountTotal(int id, int skip, int take)
        {
            var response = await _mediator.Send(new GetProductByStoreIdCountTotalQuery(id, skip, take));
            return Ok(response);
        }
    }
}
