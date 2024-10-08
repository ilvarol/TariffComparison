namespace TariffComparison.WebApi.Infrastructure.Models.Product;

public abstract class BaseProduct
{
    public required string Name { get; set; }
    public ProductType Type { get; set; }
    public decimal BaseCost { get; set; }
    public decimal AdditionalKwhCost { get; set; }

    public abstract decimal CalculateConsumption(double consumption);
}

public enum ProductType
{
    BasicElectricityTariff = 1,
    PackagedTariff = 2
}
