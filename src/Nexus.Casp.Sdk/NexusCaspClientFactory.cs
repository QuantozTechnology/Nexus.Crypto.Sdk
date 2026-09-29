using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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

    public NexusClient GetClient()
    {
        var httpClient = httpClientFactory.CreateClient(HttpClientName);
        var authenticationProvider = CreateAuthenticationProvider();

        var requestAdapter = new HttpClientRequestAdapter(authenticationProvider, httpClient: httpClient);

        var baseUrl = httpClient.BaseAddress?.ToString() ?? options.Value.DefaultBaseAddress;
        if (string.IsNullOrEmpty(baseUrl))
        {
            throw new InvalidOperationException("No default base address found");
        }

        requestAdapter.BaseUrl = baseUrl.TrimEnd('/');

        return new NexusClient(requestAdapter, httpClient);
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
