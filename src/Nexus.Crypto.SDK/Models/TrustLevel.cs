namespace Nexus.Crypto.SDK;


public class GetTrustLevel
{
    /// <summary>
    /// Name of the trust level
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Description of the trust level
    /// </summary>
    public string Description { get; set; }
    /// <summary>
    /// Is an active trust level
    /// </summary>
    public bool IsActive { get; set; }
    /// <summary>
    /// Verify the total Payout value does not exceed the total Funding value
    /// </summary>
    public bool RequireExecutedSellDoesNotExceedLifetimeBuy { get; set; }
    /// <summary>
    /// Buy limits of the trust level
    /// </summary>
    public TrustLevelDetailedLimits BuyLimits { get; set; }
    /// <summary>
    /// Sell limits of the trust level
    /// </summary>
    public TrustLevelDetailedLimits SellLimits { get; set; }
    /// <summary>
    /// Special limits of the trust level
    /// </summary>
    public TrustLevelOverallLimits OverallLimits { get; set; }
    /// <summary>
    /// Flags of the trust level
    /// </summary>
    public Dictionary<string, bool> Flags { get; set; }
}

public class TrustLevelDetailedLimits
{
    /// <summary>
    /// Daily limit
    /// </summary>
    public double DailyLimit { get; set; }
    /// <summary>
    /// Monthly limit
    /// </summary>
    public double? MonthlyLimit { get; set; }
    /// <summary>
    /// Year limit if set
    /// </summary>
    public decimal? YearLimit { get; set; }
    /// <summary>
    /// Lifetime limit if set
    /// </summary>
    public decimal? LifetimeLimit { get; set; }
}

public class TrustLevelOverallLimits
{
    /// <summary>
    /// Custodian limit (only applicable to custodian model)
    /// </summary>
    public double? CustodianLimit { get; set; }
}
