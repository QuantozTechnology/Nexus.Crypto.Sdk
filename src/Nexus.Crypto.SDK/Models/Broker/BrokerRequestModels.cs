namespace Nexus.Crypto.SDK.Models.Broker;

public class GetBrokerTransactionsRequest
{
    public string? TransactionCode { get; set; }
    public string? CustomerCode { get; set; }
    public string? AccountCode { get; set; }
    public string? Status { get; set; }

    /// <summary>
    /// Transaction type: BUY | SELL | MERCHANT
    /// </summary>
    public string? Type { get; set; }

    public string? CryptoCurrencyCode { get; set; }
    public string? CurrencyCode { get; set; }
    public string? PaymentMethodCode { get; set; }
    public string? PortfolioCode { get; set; }
    public DateTime? CreatedFrom { get; set; }
    public DateTime? CreatedTill { get; set; }
    public DateTime? FinishedFrom { get; set; }
    public DateTime? FinishedTill { get; set; }
    public decimal? AmountFrom { get; set; }
    public decimal? AmountTo { get; set; }
    public bool? IsSettled { get; set; }
    public int? Page { get; set; }
    public int? Limit { get; set; }
    public string? SortBy { get; set; }
    public SortDirection? SortDirection { get; set; }
}

public class GetBrokerTransactionTotalsRequest
{
    public string? CustomerCode { get; set; }
    public string? Status { get; set; }
    public string? Type { get; set; }
    public string? CurrencyCode { get; set; }
    public DateTime? CreatedFrom { get; set; }
    public DateTime? CreatedTill { get; set; }
}

/// <summary>
/// Request model for cancelling an order.
/// </summary>
public class CancelOrderRequest
{
    public required string OrderCode { get; set; }
}
