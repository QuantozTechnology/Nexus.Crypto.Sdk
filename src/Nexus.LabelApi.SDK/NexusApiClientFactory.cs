using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nexus.LabelApi.SDK
{
    public class NexusApiClientFactory : INexusApiClientFactory
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly Func<Task<string>> _getAccessTokenFunc;

        public NexusApiClientFactory(
            IHttpClientFactory httpClientFactory,
            INexusApiGetAccessToken getAccessTokenFunc)
        {
            _httpClientFactory = httpClientFactory;
            _getAccessTokenFunc = getAccessTokenFunc.GetAccessToken;
        }

        // The passthrough client expects that the api_version already exists in the header
        // It only adds the authorization token to it.
        public async Task<HttpClient> GetClient()
        {
            var nexusApiClient = _httpClientFactory.CreateClient("NexusApiClient");
            var accessToken = await _getAccessTokenFunc();

            nexusApiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

            return nexusApiClient;
        }

        public async Task<HttpClient> GetClient(string apiVersion)
        {
            var client = await GetClient();

            client.DefaultRequestHeaders.Add("api_version", apiVersion);
            return client;
        }
    }
}
