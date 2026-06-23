namespace Nexus.Crypto.SDK.Models;

public class GetBalanceMutationsRequest
{
    public string? CryptoCode { get; set; }
    public string? TransactionCode { get; set; }

    /// <summary>
    /// Mutation type: BUY | SELL | GIFT | DEPOSIT | WITHDRAW | FEE | CORRECTION | RECEIVEIN | SENDOUT
    /// </summary>
    public string? Type { get; set; }

    public DateTime? CreatedFrom { get; set; }
    public DateTime? CreatedTill { get; set; }
    public int? Page { get; set; }
    public int? Limit { get; set; }
    public string? SortBy { get; set; }
    public SortDirection? SortDirection { get; set; }
}

public class GetMailsRequest
{
    public string? CustomerCode { get; set; }
    public string? AccountCode { get; set; }
    public string? TransactionCode { get; set; }
    public string? Type { get; set; }
    public string? Status { get; set; }
    public DateTime? CreatedFrom { get; set; }
    public DateTime? CreatedTill { get; set; }
    public int? Page { get; set; }
    public int? Limit { get; set; }
    public string? SortBy { get; set; }
    public SortDirection? SortDirection { get; set; }
}

public class UpdatePortfolioRequest
{
    /// <summary>
    /// New human-readable name for this portfolio.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Flag to indicate if the mails feature is enabled for this portfolio.
    /// </summary>
    public bool? MailsEnabled { get; set; }
}

public class GetCryptoAddressesRequest
{
    public string? CryptoCode { get; set; }

    /// <summary>
    /// Address status: Inactive | Active | Deleted | Whitelisted | NotValidated | ToBeDeleted
    /// </summary>
    public string? Status { get; set; }

    public string? SinkType { get; set; }
    public string? SourceType { get; set; }
    public int? Page { get; set; }
    public int? Limit { get; set; }
}
