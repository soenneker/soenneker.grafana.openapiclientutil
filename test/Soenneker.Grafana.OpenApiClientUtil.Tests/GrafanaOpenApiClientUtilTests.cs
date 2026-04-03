using Soenneker.Grafana.OpenApiClientUtil.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Grafana.OpenApiClientUtil.Tests;

[Collection("Collection")]
public sealed class GrafanaOpenApiClientUtilTests : FixturedUnitTest
{
    private readonly IGrafanaOpenApiClientUtil _openapiclientutil;

    public GrafanaOpenApiClientUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _openapiclientutil = Resolve<IGrafanaOpenApiClientUtil>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
