using Nexus.Crypto.SDK;

namespace SdkWebSample;

public class GetToken : INexusApiGetAccessToken
{
    public Task<string?> GetAccessToken()
    {
        // This is a dummy implementation, you should replace this with your own implementation
        return Task.FromResult("token")!;
    }
}