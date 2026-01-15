using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PruebaHAPSA.Domain.Interfaces;
using PruebaHAPSA.Infrastructure.Persistence;
using PruebaHAPSA.Infrastructure.Persistence.Repositories;

namespace PruebaHAPSA.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<PruebaHapsaDbContext>(options =>
            options.UseSqlite(connectionString));

        services.AddScoped<IReservaRepository, ReservaRepository>();

        return services;
    }
}