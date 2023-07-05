using Microsoft.AspNetCore.Mvc;
using System.Net;
using MediatR;
using Test.Core.Models;
using StoreAPI.Application.Handler.Queries;
using Test.Core.Domain.Models;

namespace StoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
   
        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(string? keyword, decimal? minPrice, decimal? maxPrice, int skip, int take)
        {
            var response = await _mediator.Send(new GetAllProductQuery(keyword, minPrice, maxPrice, skip, take));
            return Ok(response);
        }

        [HttpGet("GetAllCountTotal")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllCountTotal(string? keyword, decimal? minPrice, decimal? maxPrice, int skip, int take)
        {
            var response = await _mediator.Send(new GetAllProductCountTotalQuery(keyword, minPrice, maxPrice, skip, take));
            return Ok(response);
        }

        [HttpGet("GetById")]
        [ProducesResponseType(typeof(ProductDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _mediator.Send(new GetProductByIdQuery(id));
            return Ok(response);
        }


        [HttpGet("search")]
        [ProducesResponseType(typeof(PagingData<ProductDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> search(string? keyword, decimal? minPrice, decimal? maxPrice, int skip, int take)
        {
            var response = await _mediator.Send(new SearchProductQuery(keyword, minPrice, maxPrice,  skip ,take));
            return Ok(response);
        }

        [HttpGet("SearchCountTotal")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> SearchCountTotal(string? keyword, decimal? minPrice, decimal? maxPrice, int skip, int take)
        {
            var response = await _mediator.Send(new SearchProductCountQuery(keyword, minPrice, maxPrice, skip, take));
            return Ok(response);
        }

        [HttpPost("Add")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Add([FromBody] ProductViewModel viewModel)
        {
            var response = await _mediator.Send(new AddProductCommand(viewModel));
            return Ok(response);
        }


        [HttpGet("CheckMasterDataEnough")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CheckMasterDataEnough()
        {
            var response = await _mediator.Send(new CheckMasterDataEnoughQuery());
            return Ok(response);
        }
    }
}
