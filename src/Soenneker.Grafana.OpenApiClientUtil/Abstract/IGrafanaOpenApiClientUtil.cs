using Soenneker.Grafana.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Grafana.OpenApiClientUtil.Abstract;

/// <summary>
/// Exposes a cached OpenAPI client instance.
/// </summary>
public interface IGrafanaOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    ValueTask<GrafanaOpenApiClient> Get(CancellationToken cancellationToken = default);
}
