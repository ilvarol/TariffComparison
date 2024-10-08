using Microsoft.AspNetCore.Mvc;
using TariffComparison.WebApi.Dtos;
using TariffComparison.WebApi.Infrastructure.Interfaces;

namespace TariffComparison.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TariffComparisonController : ControllerBase
    {
        IProductService productService;

        public TariffComparisonController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        [Route("AnnualCosts")]
        public async Task<IActionResult> GetAnnualCosts(double consumption)
        {
            var products = await productService.GetAnnualCosts(consumption);

            return Ok(products);
        }
    }
}
