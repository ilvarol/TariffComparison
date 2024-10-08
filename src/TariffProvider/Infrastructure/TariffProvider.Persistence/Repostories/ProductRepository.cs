using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TariffProvider.Application.Interfaces.Repositories;
using TariffProvider.Domain.Models;
using TariffProvider.Persistence.Context;

namespace TariffProvider.Persistence.Repostories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(TariffProviderContext dbContext) : base(dbContext)
    {
    }
}