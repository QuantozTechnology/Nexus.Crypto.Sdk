using Nexus.Crypto.SDK.Models;
using Nexus.Crypto.SDK.Models.Response;

namespace Nexus.Crypto.SDK.Services;

public interface IDocumentStoreRecordService
{
    Task<CustomResultHolder<DocumentStoreRecordResponse>> Create(FileUploadRequest request);
    Task<Stream> GetDocument(string filePath);
    Task<CustomResultHolder<PagedResult<DocumentStoreRecordResponse>>> Get(string customerCode, Dictionary<string, string> queryParams);
    Task<CustomResultHolder<DocumentStoreRecordResponse>> Update(FileUpdateRequest request);
    Task Delete(string filePath);
}