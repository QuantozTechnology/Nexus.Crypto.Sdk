using Nexus.Crypto.SDK.Models;
using Nexus.Crypto.SDK.Models.Response;

namespace Nexus.Crypto.SDK.Services;

public class DocumentStoreSettingsService(INexusApiClientFactory nexusApiClientFactory): BaseService(nexusApiClientFactory), IDocumentStoreSettingsService
{
    private const string documentStoreUrl = "integrations/documentstore";
    /// <summary>
    /// Retrieve the Document Store settings
    /// </summary>
    /// <returns></returns>
    public Task<CustomResultHolder<DocumentStoreSettingsResponse>> Get()
    {
        return GetAsync<CustomResultHolder<DocumentStoreSettingsResponse>>(documentStoreUrl, ApiVersion);
    }

    /// <summary>
    /// Create a new Document Store with the provided settings
    /// </summary>
    /// <param name="documentStoreSettings"></param>
    /// <returns></returns>
    public Task<CustomResultHolder<DocumentStoreSettingsResponse>> Create(DocumentStoreSettingsRequest documentStoreSettings)
    {
        return PostAsync<DocumentStoreSettingsRequest, CustomResultHolder<DocumentStoreSettingsResponse>>(
            documentStoreUrl,
            documentStoreSettings,
            ApiVersion);
    }

    /// <summary>
    /// Update the existing Document Store settings
    /// </summary>
    /// <param name="documentStoreSettings"></param>
    /// <returns></returns>
    public Task<CustomResultHolder> Update(DocumentStoreSettingsRequest documentStoreSettings)
    {
        return PutAsync<DocumentStoreSettingsRequest, CustomResultHolder>(
            documentStoreUrl,
            documentStoreSettings,
            ApiVersion);
    }

    /// <summary>
    /// Delete the existing Document Store settings
    /// </summary>
    /// <returns></returns>
    public Task Delete()
    {
        return DeleteAsync(documentStoreUrl, ApiVersion);
    }
}