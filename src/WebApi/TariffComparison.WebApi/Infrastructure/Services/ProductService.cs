using Microsoft.AspNetCore.Http.Json;
using System.Text.Json;
using TariffComparison.WebApi.Dtos;
using TariffComparison.WebApi.Infrastructure.Interfaces;
using TariffComparison.WebApi.Infrastructure.Models.JsonConverters;
using TariffComparison.WebApi.Infrastructure.Models.Product;

namespace TariffComparison.WebApi.Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly HttpClient client;
    private readonly JsonSerializerOptions jsonOptions = new() { Converters = { new BaseProductJsonConverter() }, 
                                                                 PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public ProductService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<List<BaseProduct>> GetProducts()
    {
        var response = await client.GetFromJsonAsync<List<BaseProduct>>($"/api/Product/Products", jsonOptions);

        return response!;
    }

    public async Task<List<ProductAnnualCostDto>> GetAnnualCosts(double consumption)
    {
        var products = await GetProducts();

        if (products is null || products.Count == 0)
            return new List<ProductAnnualCostDto>();

        var productAnnualCostDto = products
            .Select(product => new ProductAnnualCostDto
            {
                TariffName = product.Name,
                AnnualCosts = product.CalculateConsumption(consumption)
            })
            .OrderBy(o => o.AnnualCosts)
            .ToList();

        return productAnnualCostDto!;
    }
}
