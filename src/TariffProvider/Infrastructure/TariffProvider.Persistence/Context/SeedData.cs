using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TariffProvider.Domain.Models;

namespace TariffProvider.Persistence.Context;

public class SeedData
{
    private static List<Product> GetProducts()
    {
        var result = new List<Product>()
        {
            new Product()
            {
                Name = "Product A",
                Type = ProductType.BasicElectricityTariff,
                BaseCost = 5,
                AdditionalKwhCost = 0.22m
            },
            new Product()
            {
                Name = "Product B",
                Type = ProductType.PackagedTariff,
                IncludedKwh = 4000,
                BaseCost = 800,
                AdditionalKwhCost = 0.30m
            }
        };

        return result;
    }

    public async Task SeedAsync(TariffProviderContext context)
    {
        var products = GetProducts();

        await context.Products.AddRangeAsync(products);
        await context.SaveChangesAsync();
    }
}
