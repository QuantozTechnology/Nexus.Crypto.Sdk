namespace Nexus.Crypto.SDK.Models.Account;

/// <summary>
/// Response for a single crypto balance on a custodian account.
/// </summary>
public class GetAccountBalanceResponse
{
    /// <summary>
    /// Crypto identifier.
    /// </summary>
    public string? CryptoCode { get; set; }

    public decimal Total { get; set; }
    public decimal Available { get; set; }
    public decimal Locked { get; set; }
    public decimal Unconfirmed { get; set; }
}

/// <summary>
/// Request filter for listing accounts.
/// </summary>
public class GetAccountsRequest
{
    public string? CustomerCode { get; set; }
    public string? AccountCode { get; set; }
    public string? AccountType { get; set; }
    public string? AccountStatus { get; set; }
    public string? CryptoCode { get; set; }
    public string? BucketCode { get; set; }
    public int? Page { get; set; }
    public int? Limit { get; set; }
    public string? SortBy { get; set; }
    public SortDirection? SortDirection { get; set; }
}

/// <summary>
/// Request/response model for account data key-value pairs.
/// </summary>
public class AccountDataEntry
{
    public string? Key { get; set; }
    public string? Value { get; set; }
}

public class UpsertAccountDataRequest
{
    public required IDictionary<string, string> Data { get; set; }
}
