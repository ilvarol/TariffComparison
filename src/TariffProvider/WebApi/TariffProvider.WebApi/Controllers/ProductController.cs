using MediatR;
using Microsoft.AspNetCore.Mvc;
using TariffProvider.Application.Features.Queries.GetProducts;

namespace TariffProvider.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        IMediator mediator;

        public ProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("Products")]
        public async Task<IActionResult> GetProducts()
        {
            var request = new GetProductsQuery();
            var products = await mediator.Send(request);

            return Ok(products);
        }
    }
}
