using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TariffProvider.Domain.Models;

namespace TariffProvider.Persistence.Context;

public class TariffProviderContext : DbContext
{
    public const string DEFAULT_SCHEMA = "dbo";

    public TariffProviderContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
