namespace Nexus.Crypto.SDK.Services;

public class DocumentStoreService(BaseService service) : IDocumentStoreService
{
    public IDocumentStoreTypeService Types => new DocumentStoreTypeService(service);
    public IDocumentStoreSettingsService Settings => new DocumentStoreSettingsService(service);
    public IDocumentStoreRecordService Records  => new DocumentStoreRecordService(service);
}