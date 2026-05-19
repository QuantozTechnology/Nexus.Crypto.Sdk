namespace Nexus.Crypto.SDK.Services;

public class DocumentStoreService(BaseService service) : IDocumentStoreService
{
    private readonly IDocumentStoreTypeService _types = new DocumentStoreTypeService(service);
    private readonly IDocumentStoreSettingsService _settings = new DocumentStoreSettingsService(service);
    private readonly IDocumentStoreRecordService _records = new DocumentStoreRecordService(service);

    public IDocumentStoreTypeService Types => _types;
    public IDocumentStoreSettingsService Settings => _settings;
    public IDocumentStoreRecordService Records => _records;
}