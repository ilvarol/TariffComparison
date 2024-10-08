using TariffComparison.WebApi.Dtos;
using TariffComparison.WebApi.Infrastructure.Models.Product;

namespace TariffComparison.WebApi.Infrastructure.Interfaces;

public interface IProductService
{
    Task<List<BaseProduct>> GetProducts();

    Task<List<ProductAnnualCostDto>> GetAnnualCosts(double consumption);
}
