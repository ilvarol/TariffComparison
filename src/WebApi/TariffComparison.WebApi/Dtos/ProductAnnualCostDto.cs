namespace TariffComparison.WebApi.Dtos;

public class ProductAnnualCostDto
{
    public required string TariffName { get; set; }

    public decimal AnnualCosts { get; set; }
}
