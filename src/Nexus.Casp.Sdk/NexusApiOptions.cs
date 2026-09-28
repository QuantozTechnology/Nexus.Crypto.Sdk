using System.Diagnostics.CodeAnalysis;

namespace Nexus.Casp.Sdk;

public class NexusApiOptions
{
    /// <summary>
    /// The authentication scheme used for requests to the Nexus API. Defaults to <see cref="NexusApiAuthenticationType.Bearer"/>.
    /// When set to <see cref="NexusApiAuthenticationType.Bearer"/>, an <see cref="INexusApiGetAccessToken"/> implementation must be registered.
    /// </summary>
    public NexusApiAuthenticationType AuthenticationType { get; set; } = NexusApiAuthenticationType.Bearer;

    /// <summary>
    /// Hosts the bearer token is allowed to be sent to. If empty, the token is sent to any (https) host.
    /// </summary>
    public IEnumerable<string> AllowedHosts { get; set; } = [];

    /// <summary>
    /// If true, the NexusApiClient will throw an exception if no access token is found.
    /// </summary>
    public bool ThrowOnMissingAccessToken { get; set; } = true;

    /// <summary>
    /// The default base address for the Nexus API.
    /// Only used if there is no base address provided in the configured `NexusApiClient` HttpClient.
    /// </summary>
    [StringSyntax(StringSyntaxAttribute.Uri)]
    public string DefaultBaseAddress { get; set; } = "https://api.casp.quantoznexus.com";
}