using Soenneker.Grafana.OpenApiClientUtil.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Grafana.OpenApiClientUtil.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class GrafanaOpenApiClientUtilTests : HostedUnitTest
{
    private readonly IGrafanaOpenApiClientUtil _openapiclientutil;

    public GrafanaOpenApiClientUtilTests(Host host) : base(host)
    {
        _openapiclientutil = Resolve<IGrafanaOpenApiClientUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
