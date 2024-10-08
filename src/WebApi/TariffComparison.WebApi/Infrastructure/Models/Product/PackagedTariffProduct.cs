namespace TariffComparison.WebApi.Infrastructure.Models.Product;

public class PackagedTariffProduct : BaseProduct
{
    private readonly int baseCost = 800;
    private readonly int consumptionThreshold = 4000;

    public PackagedTariffProduct()
    {
    }

    public double IncludedKwh { get; set; }

    public override decimal CalculateConsumption(double consumption)
    {
        if (consumption <= consumptionThreshold)
            return baseCost;

        var consumptionCosts = (decimal)(consumption - consumptionThreshold) * AdditionalKwhCost;
        var annualCosts = baseCost + consumptionCosts;

        return annualCosts;
    }
}
