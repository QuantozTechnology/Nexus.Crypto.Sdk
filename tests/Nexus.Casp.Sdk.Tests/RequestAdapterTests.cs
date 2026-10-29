using Microsoft.Extensions.DependencyInjection;
using Microsoft.Kiota.Abstractions;
using Nexus.Casp.Client;
using Xunit;

namespace Nexus.Casp.Sdk.Tests;

public class RequestAdapterTests
{
    private sealed class RecordingHandler : HttpMessageHandler
    {
        public HttpRequestMessage? Request { get; private set; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Request = request;
            return Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.NoContent));
        }
    }

    private sealed class RequestBuilder(IRequestAdapter adapter)
        : BaseRequestBuilder(adapter, "{+baseurl}/hidden", new Dictionary<string, object> { ["baseurl"] = adapter.BaseUrl! })
    {
        public Task SendAsync() =>
            RequestAdapter.SendNoContentAsync(new RequestInformation(Method.GET, UrlTemplate, PathParameters));
    }

    private static ServiceProvider CreateProvider(RecordingHandler handler, Uri? baseAddress, string? defaultBase)
    {
        var services = new ServiceCollection();
        services.AddNexusCaspSdk(o =>
        {
            o.AuthenticationType = NexusApiAuthenticationType.None;
            o.DefaultBaseAddress = defaultBase!;
        });
        services.AddHttpClient(NexusCaspClientFactory.HttpClientName, c => c.BaseAddress = baseAddress)
            .ConfigurePrimaryHttpMessageHandler(() => handler);
        return services.BuildServiceProvider();
    }

    [Fact]
    public void GetRequestAdapter_ReturnsAdapterUsedByRequestBuilders()
    {
        using var provider = CreateProvider(new RecordingHandler(), new Uri("https://example.test/"), null);
        var client = provider.GetRequiredService<NexusCaspClientFactory>().GetClient();

        var adapter = client.GetRequestAdapter();

        Assert.Same(adapter, typeof(BaseRequestBuilder)
            .GetProperty("RequestAdapter", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!
            .GetValue(client.Accounts));
    }

    [Fact]
    public void CreateRequestAdapter_UsesHttpClientBaseAddress()
    {
        using var provider = CreateProvider(new RecordingHandler(), new Uri("https://example.test/api/"), "https://fallback.test");

        var adapter = provider.GetRequiredService<NexusCaspClientFactory>().CreateRequestAdapter();

        Assert.Equal("https://example.test/api", adapter.BaseUrl);
    }

    [Fact]
    public void CreateRequestAdapter_FallsBackToDefaultBaseAddress()
    {
        using var provider = CreateProvider(new RecordingHandler(), null, "https://fallback.test/");

        var adapter = provider.GetRequiredService<NexusCaspClientFactory>().CreateRequestAdapter();

        Assert.Equal("https://fallback.test", adapter.BaseUrl);
    }

    [Fact]
    public void CreateRequestAdapter_ThrowsWithoutBaseAddress()
    {
        using var provider = CreateProvider(new RecordingHandler(), null, null);

        Assert.Throws<InvalidOperationException>(() =>
            provider.GetRequiredService<NexusCaspClientFactory>().CreateRequestAdapter());
    }

    [Fact]
    public async Task AddHeader_IsSentByRequestsThroughGetRequestAdapter()
    {
        var handler = new RecordingHandler();
        using var provider = CreateProvider(handler, new Uri("https://example.test/"), null);
        var client = provider.GetRequiredService<NexusCaspClientFactory>().GetClient();
        client.AddHeader("x-Correlation-Id", "abc");

        await new RequestBuilder(client.GetRequestAdapter()).SendAsync();

        Assert.NotNull(handler.Request);
        Assert.Equal("https://example.test/hidden", handler.Request!.RequestUri!.ToString());
        Assert.Equal("abc", Assert.Single(handler.Request.Headers.GetValues("x-Correlation-Id")));
    }
}
