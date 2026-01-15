using Microsoft.Extensions.DependencyInjection;
using PruebaHAPSA.Application.Interfaces;
using PruebaHAPSA.Application.Services;

namespace PruebaHAPSA.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IReservaService, ReservaService>();

        return services;
    }
}