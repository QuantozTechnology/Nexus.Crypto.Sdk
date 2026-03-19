namespace Nexus.Crypto.SDK.Services;

public interface IHttpService
{
    Task<T> GetAsync<T>(string endPoint, string apiVersion);
    Task<Stream> GetStream(string endPoint, string apiVersion);
    Task<TResponse> PostAsync<TResponse>(string endPoint, MultipartFormDataContent postObject, string apiVersion);
    Task<TResponse> PostAsync<TInput, TResponse>(string endPoint, TInput? postObject, string apiVersion);
    Task<T2> PostAsync<T2>(string endPoint, string apiVersion);
    Task<TResponse> PutAsync<TInput, TResponse>(string endPoint, TInput? postObject, string apiVersion);
    Task DeleteAsync(string endPoint, string apiVersion);
}