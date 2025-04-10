namespace Nexus.Crypto.SDK.Tests.Helpers;

public class LogicHelper
{
    public readonly INexusApiClientFactory ApiClientFactory;
    public readonly INexusAPIService ApiService;
    public readonly INexusBrokerAPIService BrokerApiService;
    public readonly MockHttpResponseHandler MockResponseHandler;

    public LogicHelper()
    {
        MockResponseHandler = new MockHttpResponseHandler();
        ApiClientFactory = new NexusApiClientFactoryMock(MockResponseHandler);

        var apiService = new NexusAPIService(ApiClientFactory);
        ApiService = apiService;
        BrokerApiService = apiService;
    }
}

public class NexusApiClientFactoryMock(MockHttpResponseHandler mockResponseHandler) : INexusApiClientFactory
{
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task<HttpClient> GetClient()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronou
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Mocks a connection to the Nexus API for the test
    /// </summary>
    /// <param name="apiVersion">For the MOCK, the apiVersion is ignored</param>
    /// <returns></returns>
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task<HttpClient> GetClient(string apiVersion)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        var httpClient = new HttpClient(mockResponseHandler, disposeHandler: false)
        {
            BaseAddress = new Uri("https://api.quantoznexus.com"),
        };
        httpClient.DefaultRequestHeaders.Add("api_version", apiVersion);

        return httpClient;
    }
}

public class MockHttpResponseHandler : DelegatingHandler
{
    private class Request(HttpRequestMessage r) : IEquatable<Request>
    {
        private string Uri { get; } = r?.RequestUri?.ToString();
        private string Content { get; } = r?.Content?.ReadAsStringAsync().Result;
        private HttpMethod Method { get; } = r?.Method;
        private string ApiVersionHeader { get; } = r?.Headers.GetValues("api_version").FirstOrDefault();

        bool IEquatable<Request>.Equals(Request other)
        {
            return Uri == other?.Uri && Content == other?.Content && Method == other?.Method &&
                   ApiVersionHeader == other?.ApiVersionHeader;
        }

        public override int GetHashCode()
        {
            // Constant because equals tests mutable member.
            // This will give poor hash performance, but will prevent bugs.
            return 0;
        }

        public override bool Equals(object obj)
        {
            return ((IEquatable<Request>)this).Equals(obj as Request);
        }
    }

    private readonly Dictionary<Request, HttpResponseMessage> _mockResponses =
        new Dictionary<Request, HttpResponseMessage>();

    public void AddMockResponse(HttpRequestMessage requestMessage, HttpResponseMessage responseMessage)
    {
        _mockResponses.Add(new Request(requestMessage), responseMessage);
    }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage,
        System.Threading.CancellationToken cancellationToken)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        var request = new Request(requestMessage);
        if (_mockResponses.TryGetValue(request, out var response))
        {
            return response;
        }
        else
        {
            throw new ArgumentException("FakeRequest not registered");
        }
    }
}