using Soenneker.Grafana.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Grafana.OpenApiClientUtil.Abstract;

/// <summary>
/// Provides a cached Grafana OpenAPI client backed by the shared authenticated HTTP provider.
/// </summary>
public interface IGrafanaOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets the value.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    ValueTask<GrafanaOpenApiClient> Get(CancellationToken cancellationToken = default);
}
