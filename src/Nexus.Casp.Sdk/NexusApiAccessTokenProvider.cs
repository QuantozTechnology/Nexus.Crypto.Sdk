using Microsoft.Extensions.Options;
using Microsoft.Kiota.Abstractions.Authentication;

namespace Nexus.Casp.Sdk;

/// <summary>
/// Kiota <see cref="IAccessTokenProvider"/> that retrieves the access token through <see cref="INexusApiGetAccessToken"/>.
/// </summary>
public class NexusApiAccessTokenProvider : IAccessTokenProvider
{
    private readonly INexusApiGetAccessToken _getAccessToken;
    private readonly NexusApiOptions _options;

    public NexusApiAccessTokenProvider(INexusApiGetAccessToken getAccessToken, IOptions<NexusApiOptions> options)
    {
        _getAccessToken = getAccessToken;
        _options = options.Value;
        AllowedHostsValidator = new AllowedHostsValidator(_options.AllowedHosts);
    }

    public AllowedHostsValidator AllowedHostsValidator { get; }

    public async Task<string> GetAuthorizationTokenAsync(
        Uri uri,
        Dictionary<string, object>? additionalAuthenticationContext = null,
        CancellationToken cancellationToken = default)
    {
        if (!AllowedHostsValidator.IsUrlHostValid(uri))
        {
            return string.Empty;
        }

        if (!uri.Scheme.Equals(Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase) && !uri.IsLoopback)
        {
            throw new ArgumentException("Only https is supported when sending an access token", nameof(uri));
        }

        var accessToken = await _getAccessToken.GetAccessToken();

        if (string.IsNullOrEmpty(accessToken))
        {
            if (_options.ThrowOnMissingAccessToken)
            {
                throw new InvalidOperationException("No access token found");
            }

            return string.Empty;
        }

        return accessToken;
    }
}
