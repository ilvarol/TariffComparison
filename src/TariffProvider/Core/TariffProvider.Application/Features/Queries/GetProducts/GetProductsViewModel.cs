using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TariffProvider.Domain.Models;

namespace TariffProvider.Application.Features.Queries.GetProducts;

public class GetProductsViewModel
{
    public required string Name { get; set; }
    public ProductType Type { get; set; }
    public double? IncludedKwh { get; set; }
    public decimal BaseCost { get; set; }
    public decimal AdditionalKwhCost { get; set; }
}
