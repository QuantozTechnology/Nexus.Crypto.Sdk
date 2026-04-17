using Nexus.Crypto.SDK.Models;
using Nexus.Crypto.SDK.Models.Response;

namespace Nexus.Crypto.SDK.Services;

public class DocumentStoreRecordService(INexusApiClientFactory nexusApiClientFactory) : BaseService(nexusApiClientFactory), IDocumentStoreRecordService
{
    private const string DocumentStoreRecordUrl = "integrations/documentstore/file";
    private const string DocumentStoreRecordListUrl = "integrations/documentstore/list";
    public async Task<CustomResultHolder<DocumentStoreRecordResponse>> Create(FileUploadRequest request)
    {
        using var formContent = new MultipartFormDataContent();
        
        // Add file content as stream content to avoid loading the entire file into memory
        using var fileContent = new StreamContent(request.FileContent);
        formContent.Add(fileContent, "file", request.FileName);

        formContent.Add(new StringContent(request.FilePath), "filePath");
        formContent.Add(new StringContent(request.CustomerCode), "customerCode");
        formContent.Add(new StringContent(request.DocumentStoreTypeCode), "documentStoreTypeCode");

        if (!string.IsNullOrEmpty(request.Alias))
            formContent.Add(new StringContent(request.Alias), "alias");

        if (!string.IsNullOrEmpty(request.Description))
            formContent.Add(new StringContent(request.Description), "description");

        if (!string.IsNullOrEmpty(request.ItemReference))
            formContent.Add(new StringContent(request.ItemReference), "itemReference");

        return await PostAsync<CustomResultHolder<DocumentStoreRecordResponse>>(DocumentStoreRecordUrl, formContent, ApiVersion);
    }

    public Task<Stream> GetDocument(string filePath)
    {
        var url = $"{DocumentStoreRecordUrl}?filePath={filePath}";
        return GetStream(url, ApiVersion);
    }

    public Task<CustomResultHolder<PagedResult<DocumentStoreRecordResponse>>> Get(string customerCode, Dictionary<string, string> queryParams)
    {
        queryParams["customerCode"] = customerCode;

        var url = DocumentStoreRecordListUrl + CreateUriQuery(queryParams);
        return GetAsync<CustomResultHolder<PagedResult<DocumentStoreRecordResponse>>>(url, ApiVersion);
    }

    public Task<CustomResultHolder<DocumentStoreRecordResponse>> Update(FileUpdateRequest request)
    {
        return PutAsync<FileUpdateRequest, CustomResultHolder<DocumentStoreRecordResponse>>(
            DocumentStoreRecordUrl, request, ApiVersion);
    }

    public Task Delete(string filePath)
    {
        var url = $"{DocumentStoreRecordUrl}?filePath={filePath}";
        return DeleteAsync(url, ApiVersion);
    }
}