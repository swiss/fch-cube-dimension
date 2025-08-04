using FCh.Cube.Dimension.Services;
using FCh.Cube.Dimension.Contract;
using Microsoft.Extensions.DependencyInjection;

namespace FCh.Cube.Dimension.Extensions;

// ReSharper disable once UnusedType.Global : Used by the library's consumers
public static class FChCubeDimensionExtensions
{
    // ReSharper disable once UnusedMember.Global : Used by the library's consumers
    public static IServiceCollection AddDimesionService(this IServiceCollection services)
    {
        services.AddScoped<IDimensionService, DimensionService>();

        return services;
    }
}
