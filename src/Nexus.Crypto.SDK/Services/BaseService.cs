using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Nexus.Crypto.SDK.Models;
using Nexus.Crypto.SDK.Models.Response;

namespace Nexus.Crypto.SDK.Services;

public class BaseService(INexusApiClientFactory nexusApiClientFactory): IHttpService
{
    protected const string ISO8601DateTimeFormat = "yyyy-MM-ddTHH:mm:ssZ";
    protected const string ApiVersion = "1.2";
    protected readonly Dictionary<string, string> _headers = [];

    protected readonly JsonSerializerOptions _serializerOptions = new()
    {
        Converters = { new JsonStringEnumConverter() }, PropertyNameCaseInsensitive = true
    };

    private static async Task HandleErrorResponse<T>(HttpResponseMessage response)
    {
        var content = await response.Content.ReadFromJsonAsync<CustomResultHolder<T>>();

        var exception = content?.Errors is { Length: > 0 }
            ? new NexusApiException($"Request failed: {content.Errors.Aggregate((a, b) => a + ", " + b)}")
            : new NexusApiException($"Request failed: {response.ReasonPhrase} ({(int)response.StatusCode})");

        exception.StatusCode = response.StatusCode;
        exception.ResponseContent = await response.Content.ReadAsStringAsync();

        throw exception;
    }

    protected async Task<HttpClient> GetApiClient(string apiVersion)
    {
        var client = await nexusApiClientFactory.GetClient(apiVersion);

        foreach (var header in _headers)
        {
            client.DefaultRequestHeaders.Add(header.Key, header.Value);
        }

        return client;
    }

    public async Task<T> GetAsync<T>(string endPoint, string apiVersion)
    {
        var client = await GetApiClient(apiVersion);

        var httpResponse = await client.GetAsync(endPoint);

        if (!httpResponse.IsSuccessStatusCode)
        {
            await HandleErrorResponse<T>(httpResponse);
        }

        return (await httpResponse.Content.ReadFromJsonAsync<T>(options: _serializerOptions))!;
    }

    public async Task<Stream> GetStream(string endPoint, string apiVersion)
    {
        var client = await GetApiClient(apiVersion);

        var httpResponse = await client.GetAsync(endPoint);

        
        var statusCode = httpResponse.StatusCode;
        var responseObj = await httpResponse.Content.ReadAsStreamAsync();

        if (responseObj == null || (int)statusCode >= 300)
        {
            throw new NexusApiException($"Request failed: {httpResponse.ReasonPhrase} ({(int)httpResponse.StatusCode}");
        }
        
        return responseObj;
    }
    
    public async Task<TResponse> PostAsync<TResponse>(string endPoint, MultipartFormDataContent postObject, string apiVersion)
    {
        var client = await GetApiClient(apiVersion);

        var httpResponse = await client.PostAsync(endPoint, postObject);

        if (!httpResponse.IsSuccessStatusCode)
        {
            await HandleErrorResponse<TResponse>(httpResponse);
        }

        return (await httpResponse.Content.ReadFromJsonAsync<TResponse>(options: _serializerOptions))!;
    }

    public async Task<TResponse> PostAsync<TInput, TResponse>(string endPoint, TInput? postObject, string apiVersion)
    {
        var client = await GetApiClient(apiVersion);

        var httpResponse = await client.PostAsJsonAsync(endPoint, postObject);

        if (!httpResponse.IsSuccessStatusCode)
        {
            await HandleErrorResponse<TResponse>(httpResponse);
        }

        return (await httpResponse.Content.ReadFromJsonAsync<TResponse>(options: _serializerOptions))!;
    }

    public async Task<T2> PostAsync<T2>(string endPoint, string apiVersion)
    {
        return await PostAsync<object, T2>(endPoint, null, apiVersion);
    }
    
    public async Task<TResponse> PutAsync<TInput, TResponse>(string endPoint, TInput? postObject, string apiVersion)
    {
        var client = await GetApiClient(apiVersion);

        var httpResponse = await client.PutAsJsonAsync(endPoint, postObject);

        if (!httpResponse.IsSuccessStatusCode)
        {
            await HandleErrorResponse<TResponse>(httpResponse);
        }

        return (await httpResponse.Content.ReadFromJsonAsync<TResponse>(options: _serializerOptions))!;
    }
    
    public async Task DeleteAsync(string endPoint, string apiVersion)
    {
        var client = await GetApiClient(apiVersion);

        var httpResponse = await client.DeleteAsync(endPoint);

        if (!httpResponse.IsSuccessStatusCode)
        {
            await HandleErrorResponse<object>(httpResponse);
        }
    }
    
    public async Task<TResponse> DeleteAsync<TResponse>(string endPoint, string apiVersion)
    {
        var client = await GetApiClient(apiVersion);

        var httpResponse = await client.DeleteAsync(endPoint);

        if (!httpResponse.IsSuccessStatusCode)
        {
            await HandleErrorResponse<TResponse>(httpResponse);
        }
        return (await httpResponse.Content.ReadFromJsonAsync<TResponse>(options: _serializerOptions))!;
    }
    
    /// <summary>
    /// Take Dictionary of query parameters and creates the query string to paste to the URI.
    /// Prepends the '?'. When the dictionary is empty, returns an empty string;
    /// </summary>
    /// <param name="queryParams"></param>
    /// <returns></returns>
    protected static string CreateUriQuery(Dictionary<string, string> queryParams)
    {
        var query = string.Empty;

        foreach (var p in queryParams)
        {
            if (query == string.Empty)
            {
                query += "?";
            }
            else
            {
                query += "&";
            }

            query += $"{p.Key}={p.Value}";
        }

        return query;
    }

}