using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TariffProvider.Application.Extensions;

public static class Registiration
{
    public static IServiceCollection AddApplicationRegistration(this IServiceCollection services)
    {
        var assm = Assembly.GetExecutingAssembly();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(MediatRHandler).GetTypeInfo().Assembly));

        services.AddAutoMapper(assm);

        return services;
    }
}

public class MediatRHandler() { }
