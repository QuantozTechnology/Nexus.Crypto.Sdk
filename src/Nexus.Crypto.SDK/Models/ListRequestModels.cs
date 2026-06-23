namespace Nexus.Crypto.SDK.Models;

/// <summary>
/// Request for listing payment methods.
/// </summary>
public class GetPaymentMethodsRequest
{
    public string? CryptoCode { get; set; }
    public string? CurrencyCode { get; set; }

    /// <summary>
    /// Transaction type: BUY | SELL | SENDINTERNAL | SENDOUT | SWAP | RECEIVEIN | GIFT | SENDTOBUCKET | INTEREST | CLAWBACK
    /// </summary>
    public string? TransactionType { get; set; }

    public int? Page { get; set; }
    public int? Limit { get; set; }
}

/// <summary>
/// Response for listing payment method fee customer overrides.
/// </summary>
public class GetPaymentMethodCustomerFeesRequest
{
    public string? CustomerCode { get; set; }
    public int? Page { get; set; }
    public int? Limit { get; set; }
}

/// <summary>
/// Request filter for listing trust levels.
/// </summary>
public class GetTrustLevelsRequest
{
    public bool? IsActive { get; set; }
    public int? Page { get; set; }
    public int? Limit { get; set; }
}

/// <summary>
/// Request filter for listing portfolios.
/// </summary>
public class GetPortfoliosRequest
{
    public string? Code { get; set; }
    public int? Page { get; set; }
    public int? Limit { get; set; }
}

/// <summary>
/// Request filter for listing buckets.
/// </summary>
public class GetBucketsRequest
{
    public string? CustomerCode { get; set; }
    public int? Page { get; set; }
    public int? Limit { get; set; }
}

/// <summary>
/// Request filter for listing label-partner countries.
/// </summary>
public class GetLabelPartnerCountriesRequest
{
    public string? Status { get; set; }
    public int? Page { get; set; }
    public int? Limit { get; set; }
}

/// <summary>
/// Request filter for listing label-partner cryptocurrencies.
/// </summary>
public class GetLabelPartnerCryptoCurrenciesRequest
{
    public bool? IsActive { get; set; }
    public bool? IsEnabled { get; set; }
    public int? Page { get; set; }
    public int? Limit { get; set; }
}

/// <summary>
/// Request filter for listing merchant transactions.
/// </summary>
public class GetMerchantTransactionsRequest
{
    public string? Status { get; set; }
    public string? MerchantCustomerCode { get; set; }
    public DateTime? CreatedFrom { get; set; }
    public DateTime? CreatedTill { get; set; }
    public int? Page { get; set; }
    public int? Limit { get; set; }
    public string? SortBy { get; set; }
    public SortDirection? SortDirection { get; set; }
}
