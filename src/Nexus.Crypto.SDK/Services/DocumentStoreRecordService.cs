using Nexus.Crypto.SDK.Models.DocumentStore;
using Nexus.Crypto.SDK.Models.Response;

namespace Nexus.Crypto.SDK.Services;

public class DocumentStoreRecordService(BaseService service) : IDocumentStoreRecordService
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

        return await service.PostAsync<CustomResultHolder<DocumentStoreRecordResponse>>(DocumentStoreRecordUrl,
            formContent,
            BaseService.ApiVersion1_2);
    }

    public Task<Stream> GetDocument(string filePath)
    {
        var url = $"{DocumentStoreRecordUrl}?filePath={filePath}";
        return service.GetStream(url, BaseService.ApiVersion1_2);
    }

    public Task<CustomResultHolder<PagedResult<DocumentStoreRecordResponse>>> Get(string customerCode,
        Dictionary<string, string> queryParams)
    {
        queryParams["customerCode"] = customerCode;

        var url = DocumentStoreRecordListUrl + BaseService.CreateUriQuery(queryParams);
        return service.GetAsync<CustomResultHolder<PagedResult<DocumentStoreRecordResponse>>>(url,
            BaseService.ApiVersion1_2);
    }

    public Task<CustomResultHolder<DocumentStoreRecordResponse>> Update(FileUpdateRequest request)
    {
        return service.PutAsync<FileUpdateRequest, CustomResultHolder<DocumentStoreRecordResponse>>(
            DocumentStoreRecordUrl, request, BaseService.ApiVersion1_2);
    }

    public Task Delete(string filePath)
    {
        var url = $"{DocumentStoreRecordUrl}?filePath={filePath}";
        return service.DeleteAsync(url, BaseService.ApiVersion1_2);
    }
}