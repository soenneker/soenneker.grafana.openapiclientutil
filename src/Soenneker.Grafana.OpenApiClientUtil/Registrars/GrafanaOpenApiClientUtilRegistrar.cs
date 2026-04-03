using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Grafana.HttpClients.Registrars;
using Soenneker.Grafana.OpenApiClientUtil.Abstract;

namespace Soenneker.Grafana.OpenApiClientUtil.Registrars;

/// <summary>
/// Registers the OpenAPI client utility for dependency injection.
/// </summary>
public static class GrafanaOpenApiClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="GrafanaOpenApiClientUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddGrafanaOpenApiClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddGrafanaOpenApiHttpClientAsSingleton()
                .TryAddSingleton<IGrafanaOpenApiClientUtil, GrafanaOpenApiClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="GrafanaOpenApiClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddGrafanaOpenApiClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddGrafanaOpenApiHttpClientAsSingleton()
                .TryAddScoped<IGrafanaOpenApiClientUtil, GrafanaOpenApiClientUtil>();

        return services;
    }
}
