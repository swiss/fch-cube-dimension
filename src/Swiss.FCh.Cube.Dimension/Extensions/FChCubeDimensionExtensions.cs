// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

using Swiss.FCh.Cube.Dimension.Services;
using Swiss.FCh.Cube.Dimension.Contract;
using Microsoft.Extensions.DependencyInjection;

namespace Swiss.FCh.Cube.Dimension.Extensions
{

    /// <summary>
    /// This class holds extension methods that are used to register all the dependencies of this library in the DI container.
    /// </summary>
    public static class FChCubeDimensionExtensions
    {
        /// <summary>
        /// Adds the required dependencies of this library to the DI container.
        /// </summary>
        /// <param name="services">The <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection"/> where the services will be added</param>
        /// <returns></returns>
        public static IServiceCollection AddDimensionService(this IServiceCollection services)
        {
            services.AddScoped<IDimensionService, DimensionService>();

            return services;
        }
    }
}
