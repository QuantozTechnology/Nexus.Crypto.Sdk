using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Nexus.Casp.Client;

namespace Nexus.Casp.Sdk;

/// <summary>
/// Creates <see cref="NexusClient"/> instances configured with the authentication provider from <see cref="NexusApiOptions"/>.
/// </summary>
public class NexusCaspClientFactory(
    IHttpClientFactory httpClientFactory,
    IServiceProvider serviceProvider,
    IOptions<NexusApiOptions> options)
{
    public const string HttpClientName = "NexusApiClient";
    public const string ApiVersionHeaderName = "api_version";
    public const string ApiVersion = "1.2";

    public NexusClient GetClient()
    {
        var (requestAdapter, httpClient) = BuildRequestAdapter();
        return new NexusClient(requestAdapter, httpClient);
    }

    /// <summary>
    /// Creates a request adapter configured like the one used by <see cref="GetClient"/>, for custom Kiota request builders.
    /// Prefer <see cref="NexusClient.GetRequestAdapter"/> when headers added with <see cref="NexusClient.AddHeader"/> must be shared.
    /// </summary>
    public IRequestAdapter CreateRequestAdapter() => BuildRequestAdapter().RequestAdapter;

    private (HttpClientRequestAdapter RequestAdapter, HttpClient HttpClient) BuildRequestAdapter()
    {
        KiotaDefaults.EnsureRegistered();

        var httpClient = httpClientFactory.CreateClient(HttpClientName);
        if (!httpClient.DefaultRequestHeaders.Contains(ApiVersionHeaderName))
        {
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(ApiVersionHeaderName, ApiVersion);
        }

        var authenticationProvider = CreateAuthenticationProvider();

        var requestAdapter = new HttpClientRequestAdapter(authenticationProvider, httpClient: httpClient);

        var baseUrl = httpClient.BaseAddress?.ToString() ?? options.Value.DefaultBaseAddress;
        if (string.IsNullOrEmpty(baseUrl))
        {
            throw new InvalidOperationException("No default base address found");
        }

        requestAdapter.BaseUrl = baseUrl.TrimEnd('/');

        return (requestAdapter, httpClient);
    }

    private IAuthenticationProvider CreateAuthenticationProvider()
    {
        return options.Value.AuthenticationType switch
        {
            NexusApiAuthenticationType.None => new AnonymousAuthenticationProvider(),
            NexusApiAuthenticationType.Bearer => new BaseBearerTokenAuthenticationProvider(
                ActivatorUtilities.CreateInstance<NexusApiAccessTokenProvider>(serviceProvider)),
            _ => throw new NotSupportedException(
                $"Authentication type '{options.Value.AuthenticationType}' is not supported")
        };
    }
}
