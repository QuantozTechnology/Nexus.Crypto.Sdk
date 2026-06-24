using Nexus.Crypto.SDK.Models.DocumentStore;
using Nexus.Crypto.SDK.Models.Response;

namespace Nexus.Crypto.SDK.Services;

public class DocumentStoreTypeService(BaseService service) : IDocumentStoreTypeService
{
    private const string DocumentTypeUrl = "integrations/documentstore/type";
    public Task<CustomResultHolder<DocumentStoreTypeResponse>> Create(DocumentStoreTypeCreateRequest request)
    {
        return service.PostAsync<DocumentStoreTypeCreateRequest, CustomResultHolder<DocumentStoreTypeResponse>>(
            DocumentTypeUrl, request, BaseService.ApiVersion1_2);
    }

    public Task Delete(string documentTypeCode)
    {
        var url = $"{DocumentTypeUrl}/{documentTypeCode}";
        return service.DeleteAsync(url, BaseService.ApiVersion1_2);
    }

    public Task<CustomResultHolder<DocumentStoreTypeResponse>> GetByCode(string documentTypeCode)
    {
        var url = $"{DocumentTypeUrl}/{documentTypeCode}";
        return service.GetAsync<CustomResultHolder<DocumentStoreTypeResponse>>(url, BaseService.ApiVersion1_2);
    }

    public Task<CustomResultHolder<PagedResult<DocumentStoreTypeResponse>>> Get(Dictionary<string, string> queryParams)
    {
        var url = DocumentTypeUrl + BaseService.ToQueryString(queryParams);
        return service.GetAsync<CustomResultHolder<PagedResult<DocumentStoreTypeResponse>>>(url, BaseService.ApiVersion1_2);
    }

    public Task<CustomResultHolder<DocumentStoreTypeResponse>> Update(string documentTypeCode, DocumentStoreTypeUpdateRequest request)
    {
        var url = $"{DocumentTypeUrl}/{documentTypeCode}";
        return service.PutAsync<DocumentStoreTypeUpdateRequest, CustomResultHolder<DocumentStoreTypeResponse>>(
            url, request, BaseService.ApiVersion1_2);
    }
}