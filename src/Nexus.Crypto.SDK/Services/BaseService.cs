using System.Globalization;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Nexus.Crypto.SDK.Models;
using Nexus.Crypto.SDK.Models.Response;

namespace Nexus.Crypto.SDK.Services;

public class BaseService(INexusApiClientFactory nexusApiClientFactory): IHttpService
{
    public const string ApiVersion1_2 = "1.2";
    public const string ISO8601DateTimeFormat = "yyyy-MM-ddTHH:mm:ssZ";
    
    private readonly Dictionary<string, string> _headers = [];

    private readonly JsonSerializerOptions _serializerOptions = new()
    {
        Converters = { new JsonStringEnumConverter() }, PropertyNameCaseInsensitive = true
    };
    
    public void AddHeader(string key, string value)
    {
        _headers.Add(key, value);
    }

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

    private async Task<HttpClient> GetApiClient(string apiVersion)
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

        var httpResponse = await client.PostAsJsonAsync(endPoint, postObject, _serializerOptions);

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

        var httpResponse = await client.PutAsJsonAsync(endPoint, postObject, _serializerOptions);

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
    /// Returns a query string of the given dictionary, starting with ?
    /// </summary>
    /// <param name="dict"></param>
    /// <returns></returns>
    public static string ToQueryString(Dictionary<string, string> dict)
    {
        var queryStrings = new List<string>();

        foreach (var (key, value) in dict)
        {
            if (!string.IsNullOrWhiteSpace(key) && !string.IsNullOrWhiteSpace(value))
            {
                queryStrings.Add($"{Uri.EscapeDataString(ToCamelCase(key))}={Uri.EscapeDataString(value)}");
            }
        }

        if (queryStrings.Count > 0)
        {
            return "?" + string.Join("&", queryStrings);
        }

        return string.Empty;
    }

    /// <summary>
    /// Returns a query string of the given object, starting with ?
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string ToQueryString(object? obj)
    {
        if (obj == null) return string.Empty;

        var properties = obj.GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.GetValue(obj) != null)
            .Select(p =>
                $"{Uri.EscapeDataString(ToCamelCase(p.Name))}={Uri.EscapeDataString(p.GetValue(obj)?.ToString()!)}");

        if (properties.Any())
        {
            return "?" + string.Join("&", properties);
        }

        return string.Empty;
    }

    private static string ToCamelCase(string name)
    {
        if (string.IsNullOrEmpty(name) || char.IsLower(name[0]))
            return name;

        return char.ToLower(name[0], CultureInfo.InvariantCulture) + name[1..];
    }
}