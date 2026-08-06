namespace Nexus.Crypto.SDK.Models.Custodian;

public class GetCustodianTransactionsRequest
{
    public string? CustomerCode { get; set; }
    public string? AccountCode { get; set; }
    public string? TransactionCode { get; set; }
    public string? CryptoCode { get; set; }
    public string? CurrencyCode { get; set; }
    public string? PaymentMethodCode { get; set; }
    public TransactionTypes? Type { get; set; }
    public CustodianTransactionStatus? Status { get; set; }
    public DateTime? CreatedFrom { get; set; }
    public DateTime? CreatedTill { get; set; }
    public int? Page { get; set; }
    public int? Limit { get; set; }
    public string? SortBy { get; set; }
}
