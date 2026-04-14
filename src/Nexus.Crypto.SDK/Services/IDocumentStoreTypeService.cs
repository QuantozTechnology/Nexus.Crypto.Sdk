using Nexus.Crypto.SDK.Models;
using Nexus.Crypto.SDK.Models.Response;

namespace Nexus.Crypto.SDK.Services;

public interface IDocumentStoreTypeService
{
    /// <summary>
    /// Create a new Document Store Type
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<CustomResultHolder<DocumentStoreTypeResponse>> Create(DocumentStoreTypeCreateRequest request);

    /// <summary>
    /// Delete an existing Document Store Type by code
    /// </summary>
    /// <param name="documentStoreTypeCode"></param>
    /// <returns></returns>
    Task Delete(string documentStoreTypeCode);

    /// <summary>
    /// Retrieve a Document Store Type by code
    /// </summary>
    /// <param name="documentTypeCode"></param>
    /// <returns></returns>
    Task<CustomResultHolder<DocumentStoreTypeResponse>> GetByCode(string documentTypeCode);

    /// <summary>
    /// Retrieve Document Store Types using query parameters
    /// </summary>
    /// <param name="queryParams"></param>
    /// <returns></returns>
    Task<CustomResultHolder<PagedResult<DocumentStoreTypeResponse>>> Get(Dictionary<string, string> queryParams);

    /// <summary>
    /// Update an existing Document Store Type
    /// </summary>
    /// <param name="documentTypeCode"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<CustomResultHolder<DocumentStoreTypeResponse>> Update(string documentTypeCode, DocumentStoreTypeUpdateRequest request);
}