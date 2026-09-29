namespace Nexus.Casp.Sdk;

/// <summary>
/// The authentication scheme used by the <see cref="Nexus.Casp.Client.NexusClient"/>.
/// </summary>
public enum NexusApiAuthenticationType
{
    /// <summary>
    /// No authentication is applied to the requests (anonymous).
    /// </summary>
    None,

    /// <summary>
    /// A bearer token, obtained through <see cref="INexusApiGetAccessToken"/>, is added to the Authorization header.
    /// </summary>
    Bearer
}
