namespace Nexus.Crypto.SDK.Models;

/// <summary>
/// Request filter for listing customer bank accounts.
/// </summary>
public class GetBankAccountsRequest
{
    public string? Number { get; set; }
    public string? CurrencyCode { get; set; }
    public bool? IsPrimary { get; set; }
    public int? Page { get; set; }
    public int? Limit { get; set; }
    public string? SortBy { get; set; }
    public SortDirection? SortDirection { get; set; }
}

/// <summary>
/// Request filter for listing banks.
/// </summary>
public class GetBanksRequest
{
    public string? Name { get; set; }
    public string? BicCode { get; set; }
    public string? CountryCode { get; set; }
    public int? Page { get; set; }
    public int? Limit { get; set; }
}

/// <summary>
/// Request filter for listing persons.
/// </summary>
public class GetPersonsRequest
{
    public CustomerPersonType? Type { get; set; }
    public int? Page { get; set; }
    public int? Limit { get; set; }
    public string? SortBy { get; set; }
    public SortDirection? SortDirection { get; set; }
}
