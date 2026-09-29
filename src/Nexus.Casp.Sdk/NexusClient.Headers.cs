using System.Net.Http.Headers;
using Microsoft.Kiota.Abstractions;

// Extends the Kiota-generated client; kept outside Client/ so `kiota update --clean-output` preserves it.
namespace Nexus.Casp.Client;

public partial class NexusClient
{
    private readonly HttpRequestHeaders? _defaultRequestHeaders;

    internal NexusClient(IRequestAdapter requestAdapter, HttpClient httpClient) : this(requestAdapter)
    {
        _defaultRequestHeaders = httpClient.DefaultRequestHeaders;
    }

    /// <summary>
    /// Adds a header that is sent with every subsequent request made by this client instance,
    /// replacing any existing value for the same header.
    /// </summary>
    /// <example><code>nexusClient.AddHeader("x-Correlation-Id", correlationId);</code></example>
    /// <exception cref="InvalidOperationException">The client was not created by <c>NexusCaspClientFactory</c>.</exception>
    public NexusClient AddHeader(string name, string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(value);

        var headers = GetDefaultRequestHeaders();
        headers.Remove(name);
        if (!headers.TryAddWithoutValidation(name, value))
        {
            throw new ArgumentException($"Header '{name}' cannot be set as a request header.", nameof(name));
        }

        return this;
    }

    /// <summary>
    /// Removes a header previously added with <see cref="AddHeader"/>.
    /// </summary>
    public NexusClient RemoveHeader(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        GetDefaultRequestHeaders().Remove(name);
        return this;
    }

    private HttpRequestHeaders GetDefaultRequestHeaders() =>
        _defaultRequestHeaders ?? throw new InvalidOperationException(
            "Custom headers are only supported on clients created by NexusCaspClientFactory.");
}
