using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.Grafana.HttpClients.Abstract;
using Soenneker.Grafana.OpenApiClientUtil.Abstract;
using Soenneker.Grafana.OpenApiClient;
using Soenneker.Kiota.GenericAuthenticationProvider;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Grafana.OpenApiClientUtil;

///<inheritdoc cref="IGrafanaOpenApiClientUtil"/>
public sealed class GrafanaOpenApiClientUtil : IGrafanaOpenApiClientUtil
{
    private readonly AsyncSingleton<GrafanaOpenApiClient> _client;

    public GrafanaOpenApiClientUtil(IGrafanaOpenApiHttpClient httpClientUtil, IConfiguration configuration)
    {
        _client = new AsyncSingleton<GrafanaOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var apiKey = configuration.GetValueStrict<string>("Grafana:ApiKey");
            string authHeaderValueTemplate = configuration["Grafana:AuthHeaderValueTemplate"] ?? "{token}";
            string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

            var requestAdapter = new HttpClientRequestAdapter(new GenericAuthenticationProvider(headerValue: authHeaderValue), httpClient: httpClient);

            return new GrafanaOpenApiClient(requestAdapter);
        });
    }

    public ValueTask<GrafanaOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}
