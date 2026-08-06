namespace Nexus.Crypto.SDK.Models.Custodian;

/// <summary>
/// Request filter for listing custodian interest schedule intervals.
/// </summary>
public class GetScheduleIntervalsRequest
{
    public IntervalType? Type { get; set; }
    public int? Page { get; set; }
    public int? Limit { get; set; }
}

/// <summary>
/// Request filter for listing interest schedules.
/// </summary>
public class GetSchedulesRequest
{
    public string? BucketCode { get; set; }
    public string? CryptoCode { get; set; }
    public string? CurrencyCode { get; set; }
    public int? Page { get; set; }
    public int? Limit { get; set; }
}

/// <summary>
/// Response for a custodian swap simulation.
/// </summary>
public class CustodianSwapSimulateRequest
{
    public required string PaymentMethodCode { get; set; }
    public required SwapSource Source { get; set; }
    public required SwapDestination Destination { get; set; }
}
