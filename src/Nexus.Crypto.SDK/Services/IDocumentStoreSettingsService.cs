using Nexus.Crypto.SDK.Models;
using Nexus.Crypto.SDK.Models.Response;

namespace Nexus.Crypto.SDK.Services;

public interface IDocumentStoreSettingsService
{
    /// <summary>
    /// Retrieve the Document Store settings
    /// </summary>
    /// <returns></returns>
    Task<CustomResultHolder<DocumentStoreSettingsResponse>> Get();

    /// <summary>
    /// Create a new Document Store with the provided settings
    /// </summary>
    /// <param name="documentStoreSettings"></param>
    /// <returns></returns>
    Task<CustomResultHolder<DocumentStoreSettingsResponse>> Create(
        DocumentStoreSettingsRequest documentStoreSettings);

    /// <summary>
    /// Update the existing Document Store settings
    /// </summary>
    /// <param name="documentStoreSettings"></param>
    /// <returns></returns>
    Task<CustomResultHolder> Update(DocumentStoreSettingsRequest documentStoreSettings);

    /// <summary>
    /// Delete the existing Document Store settings
    /// </summary>
    /// <returns></returns>
    Task Delete();
}