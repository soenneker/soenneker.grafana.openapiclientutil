using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.ValueTask;
using Soenneker.Grafana.HttpClients.Abstract;
using Soenneker.Grafana.OpenApiClientUtil.Abstract;
using Soenneker.Grafana.OpenApiClient;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Grafana.OpenApiClientUtil;

/// <inheritdoc cref="IGrafanaOpenApiClientUtil" />
public sealed class GrafanaOpenApiClientUtil : IGrafanaOpenApiClientUtil
{
    private readonly AsyncSingleton<GrafanaOpenApiClient> _client;

    public GrafanaOpenApiClientUtil(IGrafanaOpenApiHttpClient httpClientUtil)
    {
        _client = new AsyncSingleton<GrafanaOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var requestAdapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider(), httpClient: httpClient);

            return new GrafanaOpenApiClient(requestAdapter);
        });
    }

    public ValueTask<GrafanaOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    /// <summary>
    /// Releases resources used by the current instance.
    /// </summary>
    public void Dispose()
    {
        _client.Dispose();
    }

    /// <summary>
    /// Asynchronously releases resources used by the current instance.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}
