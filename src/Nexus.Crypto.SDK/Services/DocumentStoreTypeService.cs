using Nexus.Crypto.SDK.Models;
using Nexus.Crypto.SDK.Models.Response;

namespace Nexus.Crypto.SDK.Services;

public class DocumentStoreTypeService(INexusApiClientFactory nexusApiClientFactory) : BaseService(nexusApiClientFactory), IDocumentStoreTypeService
{
    private const string DocumentTypeUrl = "integrations/documentstore/type";
    public Task<CustomResultHolder<DocumentStoreTypeResponse>> Create(DocumentStoreTypeCreateRequest request)
    {
        return PostAsync<DocumentStoreTypeCreateRequest, CustomResultHolder<DocumentStoreTypeResponse>>(
            DocumentTypeUrl, request, ApiVersion);
    }

    public Task Delete(string documentTypeCode)
    {
        var url = $"{DocumentTypeUrl}/{documentTypeCode}";
        return DeleteAsync(url, ApiVersion);
    }

    public Task<CustomResultHolder<DocumentStoreTypeResponse>> GetByCode(string documentTypeCode)
    {
        var url = $"{DocumentTypeUrl}/{documentTypeCode}";
        return GetAsync<CustomResultHolder<DocumentStoreTypeResponse>>(url, ApiVersion);
    }

    public Task<CustomResultHolder<PagedResult<DocumentStoreTypeResponse>>> Get(Dictionary<string, string> queryParams)
    {
        var url = DocumentTypeUrl + CreateUriQuery(queryParams);
        return GetAsync<CustomResultHolder<PagedResult<DocumentStoreTypeResponse>>>(url, ApiVersion);
    }

    public Task<CustomResultHolder<DocumentStoreTypeResponse>> Update(string documentTypeCode, DocumentStoreTypeUpdateRequest request)
    {
        var url = $"{DocumentTypeUrl}/{documentTypeCode}";
        return PutAsync<DocumentStoreTypeUpdateRequest, CustomResultHolder<DocumentStoreTypeResponse>>(
            url, request, ApiVersion);
    }
}