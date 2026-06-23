namespace Nexus.Crypto.SDK.Models;

public class GetBrokerLimitResponse
{
    /// <summary>
    /// The parameters applied to this limit calculation.
    /// </summary>
    public LimitParameters? LimitParameters { get; set; }

    /// <summary>
    /// The reasons the limit of the customer is adjusted.
    /// </summary>
    public string[]? LimitReasons { get; set; }

    /// <summary>
    /// The remaining limits of a customer.
    /// </summary>
    public TrustlevelLimit? Remaining { get; set; }

    /// <summary>
    /// The total limits of a customer.
    /// </summary>
    public TrustlevelLimit? Total { get; set; }
}

public class LimitParameters
{
    /// <summary>
    /// The currency the limits are shown in (ISO 4217).
    /// </summary>
    public string? CurrencyCode { get; set; }

    /// <summary>
    /// The payment method the customer limit is calculated with.
    /// </summary>
    public string? PaymentMethodCode { get; set; }

    /// <summary>
    /// The crypto currency the customer limit is calculated with.
    /// </summary>
    public string? CryptoCode { get; set; }
}

public class TrustlevelLimit
{
    /// <summary>
    /// The amount in fiat a customer can buy/sell in a day.
    /// </summary>
    public decimal DailyLimit { get; set; }

    /// <summary>
    /// The amount in fiat a customer can buy/sell in a month.
    /// </summary>
    public decimal MonthlyLimit { get; set; }

    /// <summary>
    /// The amount in fiat a customer can buy/sell in a year.
    /// </summary>
    public decimal? YearlyLimit { get; set; }

    /// <summary>
    /// The amount in fiat a customer can buy/sell lifetime.
    /// </summary>
    public decimal? LifetimeLimit { get; set; }
}
