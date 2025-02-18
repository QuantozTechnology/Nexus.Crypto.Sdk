using System.Net;

namespace Nexus.Crypto.SDK.Models;

public class NexusApiException : Exception
{
    public HttpStatusCode StatusCode { get; set; }
    public string ResponseContent { get; set; }

    public NexusApiException(string message)
        : base(message)
    { }
}
