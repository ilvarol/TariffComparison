using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TariffProvider.Domain.Models;
using TariffProvider.Persistence.Context;

namespace TariffProvider.Persistence.EntityConfigurations;

public class ProductEntityConfiguration : BaseEntityConfiguration<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.IncludedKwh)
               .IsRequired(false);

        builder.Property(p => p.AdditionalKwhCost)
               .HasColumnType("money");

        builder.Property(p => p.BaseCost)
               .HasColumnType("money");

        builder.ToTable("product", TariffProviderContext.DEFAULT_SCHEMA);
    }
}
