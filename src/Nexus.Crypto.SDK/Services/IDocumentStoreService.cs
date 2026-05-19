namespace Nexus.Crypto.SDK.Services;

public interface IDocumentStoreService
{
    IDocumentStoreTypeService Types { get; }
    IDocumentStoreSettingsService Settings { get; }
    IDocumentStoreRecordService Records { get; }
}