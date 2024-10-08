using Microsoft.Extensions.Hosting;

namespace TariffComparison.WebApi.Infrastructure.Models.Product;

public class BasicElectricityTariffProduct : BaseProduct
{
    private readonly int months = 12;  

    public BasicElectricityTariffProduct()
    {
    }

    public override decimal CalculateConsumption(double consumption)
    {
        var consumptionCosts = (decimal)consumption * AdditionalKwhCost;
        var baseCosts = BaseCost * months;

        var annualCosts = consumptionCosts + baseCosts;

        return annualCosts;
    }
}
