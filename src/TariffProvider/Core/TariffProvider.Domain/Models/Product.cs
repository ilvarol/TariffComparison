using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TariffProvider.Domain.Models;

public class Product : BaseEntity
{
    public required string Name { get; set; }
    public ProductType Type { get; set; }
    public double? IncludedKwh { get; set; }
    public decimal BaseCost { get; set; }
    public decimal AdditionalKwhCost { get; set; }
}

public enum ProductType
{
    BasicElectricityTariff = 1,
    PackagedTariff = 2
}