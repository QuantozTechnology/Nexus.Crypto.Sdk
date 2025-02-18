using System.Net.Http;
using System.Threading.Tasks;

namespace Nexus.Crypto.SDK;

public interface INexusApiClientFactory
{
    Task<HttpClient> GetClient();
    Task<HttpClient> GetClient(string apiVersion);
}
