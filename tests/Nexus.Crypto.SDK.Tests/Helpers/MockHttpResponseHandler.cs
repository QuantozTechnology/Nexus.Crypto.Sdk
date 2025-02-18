using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nexus.Crypto.SDK.Tests.Helpers;

public class LogicHelper
{
    public readonly INexusApiClientFactory ApiClientFactory;
    public readonly INexusAPIService ApiService;
    public readonly MockHttpResponseHandler MockResponseHandler;

    public LogicHelper()
    {
        MockResponseHandler = new MockHttpResponseHandler();
        ApiClientFactory = new NexusApiClientFactoryMock(MockResponseHandler);
        ApiService = new NexusAPIService(ApiClientFactory);
    }
}

public class NexusApiClientFactoryMock : INexusApiClientFactory
{
    public readonly MockHttpResponseHandler _mockResponseHandler;

    public NexusApiClientFactoryMock(MockHttpResponseHandler mockResponseHandler)
    {
        _mockResponseHandler = mockResponseHandler;
    }

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
        var httpClient = new HttpClient(_mockResponseHandler, disposeHandler: false)
        {
            BaseAddress = new Uri("https://api.quantoznexus.com"),
        };
        httpClient.DefaultRequestHeaders.Add("api_version", apiVersion);

        return httpClient;
    }
}

public class MockHttpResponseHandler : DelegatingHandler
{
    public class Request : IEquatable<Request>
    {
        public string Uri { get; set; }
        public string Content { get; set; }
        public HttpMethod Method { get; set; }
        public string ApiVersionHeader { get; set; }

        public Request(HttpRequestMessage r)
        {
            Content = r?.Content?.ReadAsStringAsync().Result;
            Uri = r?.RequestUri?.ToString();
            Method = r?.Method;
            ApiVersionHeader = r?.Headers.GetValues("api_version").FirstOrDefault();
        }

        bool IEquatable<Request>.Equals(Request other)
        {
            return Uri == other.Uri && Content == other.Content && Method == other.Method && ApiVersionHeader == other.ApiVersionHeader;
        }

        public override int GetHashCode()
        {
            // Constant because equals tests mutable member.
            // This will give poor hash performance, but will prevent bugs.
            return 0;
        }
    }

    private readonly Dictionary<Request, HttpResponseMessage> _mockResponses = new Dictionary<Request, HttpResponseMessage>();

    public void AddMockResponse(HttpRequestMessage requestMessage, HttpResponseMessage responseMessage)
    {
        _mockResponses.Add(new Request(requestMessage), responseMessage);
    }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage, System.Threading.CancellationToken cancellationToken)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        var request = new Request(requestMessage);
        if (_mockResponses.ContainsKey(request))
        {
            return _mockResponses[request];
        }
        else
        {
            throw new ArgumentException("FakeRequest not registered");
        }
    }

}
