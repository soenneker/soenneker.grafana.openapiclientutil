[![](https://img.shields.io/nuget/v/soenneker.grafana.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.grafana.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.grafana.openapiclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.grafana.openapiclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.grafana.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.grafana.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.grafana.openapiclientutil/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.grafana.openapiclientutil/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Grafana.OpenApiClientUtil

Reuse a configured `GrafanaOpenApiClient` throughout an application without rebuilding the Kiota adapter for each request.

## Installation

```bash
dotnet add package Soenneker.Grafana.OpenApiClientUtil
```

## Configuration

```json
{
  "Grafana": {
    "ApiKey": "grafana-service-account-token",
    "ClientBaseUrl": "https://grafana.example.com/api"
  }
}
```

`ClientBaseUrl` is required and must include Grafana's `/api` root. Custom authentication header settings are inherited from `Soenneker.Grafana.HttpClients`.

## Registration

```csharp
services.AddGrafanaOpenApiClientUtilAsSingleton();
```

Use `AddGrafanaOpenApiClientUtilAsScoped()` when the consumer should be scoped. The scoped utility still uses the singleton HTTP provider; disposing a scope releases its cached OpenAPI wrapper without removing the shared authenticated transport.

## Usage

```csharp
public sealed class GrafanaService
{
    private readonly IGrafanaOpenApiClientUtil _clients;

    public GrafanaService(IGrafanaOpenApiClientUtil clients)
    {
        _clients = clients;
    }

    public ValueTask<GrafanaOpenApiClient> GetClient(
        CancellationToken cancellationToken = default)
    {
        return _clients.Get(cancellationToken);
    }
}
```

`Get` returns the same generated client for the lifetime of the utility. Authentication is supplied by the underlying HTTP provider, so the Kiota adapter does not add a second authorization header.
