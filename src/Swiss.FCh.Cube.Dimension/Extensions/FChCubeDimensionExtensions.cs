// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

using Swiss.FCh.Cube.Dimension.Services;
using Swiss.FCh.Cube.Dimension.Contract;
using Microsoft.Extensions.DependencyInjection;

namespace Swiss.FCh.Cube.Dimension.Extensions
{

    public static class FChCubeDimensionExtensions
    {

        public static IServiceCollection AddDimesionService(this IServiceCollection services)
        {
            services.AddScoped<IDimensionService, DimensionService>();

            return services;
        }
    }
}
