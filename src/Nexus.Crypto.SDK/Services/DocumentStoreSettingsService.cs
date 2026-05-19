using Nexus.Crypto.SDK.Models.DocumentStore;
using Nexus.Crypto.SDK.Models.Response;

namespace Nexus.Crypto.SDK.Services;

public class DocumentStoreSettingsService(BaseService service) : IDocumentStoreSettingsService
{
    private const string documentStoreUrl = "integrations/documentstore";

    /// <summary>
    /// Retrieve the Document Store settings
    /// </summary>
    /// <returns></returns>
    public Task<CustomResultHolder<DocumentStoreSettingsResponse>> Get()
    {
        return service.GetAsync<CustomResultHolder<DocumentStoreSettingsResponse>>(documentStoreUrl,
            BaseService.ApiVersion1_2);
    }

    /// <summary>
    /// Create a new Document Store with the provided settings
    /// </summary>
    /// <param name="documentStoreSettings"></param>
    /// <returns></returns>
    public Task<CustomResultHolder<DocumentStoreSettingsResponse>> Create(
        DocumentStoreSettingsRequest documentStoreSettings)
    {
        return service.PostAsync<DocumentStoreSettingsRequest, CustomResultHolder<DocumentStoreSettingsResponse>>(
            documentStoreUrl,
            documentStoreSettings,
            BaseService.ApiVersion1_2);
    }

    /// <summary>
    /// Update the existing Document Store settings
    /// </summary>
    /// <param name="documentStoreSettings"></param>
    /// <returns></returns>
    public Task<CustomResultHolder> Update(DocumentStoreSettingsRequest documentStoreSettings)
    {
        return service.PutAsync<DocumentStoreSettingsRequest, CustomResultHolder>(
            documentStoreUrl,
            documentStoreSettings,
            BaseService.ApiVersion1_2);
    }

    /// <summary>
    /// Delete the existing Document Store settings
    /// </summary>
    /// <returns></returns>
    public Task Delete()
    {
        return service.DeleteAsync(documentStoreUrl, BaseService.ApiVersion1_2);
    }
}