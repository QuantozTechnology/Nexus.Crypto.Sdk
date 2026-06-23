namespace Nexus.Crypto.SDK.Models;

/// <summary>
/// Request filter for listing historic price data.
/// </summary>
public class GetPriceDataRequest
{
    /// <summary>
    /// Start of the date range (ISO 8601).
    /// </summary>
    public DateTime? From { get; set; }

    /// <summary>
    /// End of the date range (ISO 8601).
    /// </summary>
    public DateTime? To { get; set; }

    /// <summary>
    /// Interval resolution: Hour | Day | Week | Month
    /// </summary>
    public string? Resolution { get; set; }
}

/// <summary>
/// Request filter for listing reserves.
/// </summary>
public class GetReservesRequest
{
    /// <summary>
    /// Optional timestamp to retrieve reserve snapshot (ISO 8601).
    /// </summary>
    public string? ReservesTimeStamp { get; set; }
}

/// <summary>
/// Request filter for listing hot-wallet balances.
/// </summary>
public class GetHotWalletBalancesRequest
{
    public string? CryptoCode { get; set; }
    public int? Page { get; set; }
    public int? Limit { get; set; }
}

/// <summary>
/// Request filter for listing exchanges.
/// </summary>
public class GetExchangesRequest
{
    public string? ExchangeCode { get; set; }
    public int? Page { get; set; }
    public int? Limit { get; set; }
}

/// <summary>
/// Request filter for listing trade pairs.
/// </summary>
public class GetTradePairsRequest
{
    public string? ExchangeCode { get; set; }
    public string? CryptoCode { get; set; }
    public string? CurrencyCode { get; set; }
    public bool? IsEnabled { get; set; }
    public int? Page { get; set; }
    public int? Limit { get; set; }
}
