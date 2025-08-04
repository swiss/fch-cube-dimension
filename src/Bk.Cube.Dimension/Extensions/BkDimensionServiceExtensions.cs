using Bk.Cube.Dimension.Services;
using Bk.Dimension.Contract;
using Microsoft.Extensions.DependencyInjection;

namespace Bk.Dimension.Extensions;

// ReSharper disable once UnusedType.Global : Used by the library's consumers
public static class BkDimensionServiceExtensions
{
    // ReSharper disable once UnusedMember.Global : Used by the library's consumers
    public static IServiceCollection AddDimesionService(this IServiceCollection services)
    {
        services.AddScoped<IDimensionService, DimensionService>();

        return services;
    }
}
