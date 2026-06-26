namespace Nexus.Crypto.SDK.Models.Custodian;

public class CreateCustodianScheduleRequest
{
    /// <summary>
    /// Three letter code of the currency (ISO 4217).
    /// </summary>
    public required string CurrencyCode { get; set; }

    public required string CryptoCode { get; set; }

    /// <summary>
    /// Code of the bucket used to calculate the custodian balance.
    /// </summary>
    public required string BucketCode { get; set; }

    public required string PaymentMethodCode { get; set; }

    /// <summary>
    /// Rate algorithm: WeeklyAPY
    /// </summary>
    public required string RateType { get; set; }

    /// <summary>
    /// Rate in decimals (relative value). 0.01 equals 1%.
    /// </summary>
    public decimal DefaultRate { get; set; }

    /// <summary>
    /// When this schedule starts triggering (ISO 8601).
    /// </summary>
    public required string StartDate { get; set; }

    /// <summary>
    /// Defines the interval on which payouts happen.
    /// </summary>
    public required SchedulePeriod PayoutPeriod { get; set; }

    /// <summary>
    /// Defines the interval on which balances are snapshotted.
    /// </summary>
    public required SchedulePeriod SubIntervalPeriod { get; set; }
}

public class SchedulePeriod
{
    /// <summary>
    /// Period unit: Days | Months
    /// </summary>
    public required string Unit { get; set; }

    public int Amount { get; set; }
}

public class EditCustodianScheduleRequest
{
    /// <summary>
    /// Rate in decimals (relative value). 0.01 equals 1%. Only applies for newly created intervals.
    /// </summary>
    public decimal DefaultRate { get; set; }
}

public class EditCustodianSubIntervalRequest
{
    /// <summary>
    /// Rate in decimals (relative value). 0.01 equals 1%.
    /// </summary>
    public decimal AdjustedRate { get; set; }
}

public class GetScheduleResponse
{
    public Guid Id { get; set; }
    public string? StartDate { get; set; }
    public string? BucketCode { get; set; }
    public string? CurrencyCode { get; set; }
    public string? CryptoCode { get; set; }
    public decimal DefaultRate { get; set; }
    public string? PaymentMethodCode { get; set; }
    public SchedulePeriod? PayoutPeriod { get; set; }
    public SchedulePeriod? SubIntervalPeriod { get; set; }
}

public class GetScheduleWithIntervalsResponse : GetScheduleResponse
{
    public IEnumerable<GetScheduleIntervalResponse>? Intervals { get; set; }
}

public class GetScheduleIntervalResponse
{
    public Guid Id { get; set; }
    public string? From { get; set; }
    public string? To { get; set; }
    public int Number { get; set; }
    public IEnumerable<GetScheduleSubIntervalResponse>? SubIntervals { get; set; }
}

public class GetScheduleSubIntervalResponse
{
    public Guid Id { get; set; }
    public string? From { get; set; }
    public string? To { get; set; }
    public decimal AdjustedRate { get; set; }
    public int Number { get; set; }
}

public enum IntervalType
{
    Current,
    Next,
    Previous
}
