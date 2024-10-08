using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TariffProvider.Application.Interfaces.Repositories;
using TariffProvider.Persistence.Context;
using TariffProvider.Persistence.Repostories;

namespace TariffProvider.Persistence.Extensions;

public static class Registiration
{
    public static async Task<IServiceCollection> AddInfrastructureRegistrationAsync(this IServiceCollection services, IConfiguration configuration)
    {
        var connStr = configuration.GetConnectionString("TariffProviderConnStr");
        services.AddDbContext<TariffProviderContext>(conf =>
        {
            conf.UseSqlServer(connStr, opt =>
            {
                opt.EnableRetryOnFailure();
            });
        });

        var context = services.BuildServiceProvider().GetRequiredService<TariffProviderContext>();
        
        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();

            var seed = !await context.Products.AnyAsync();

            if (seed)
            {
                var seedData = new SeedData();
                await seedData.SeedAsync(context);
            }
        }

        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}