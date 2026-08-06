namespace Nexus.Crypto.SDK.Models;

public class LabelPartnerResponse
{
    public string? LabelPartnerCode { get; set; }
    public string? BusinessModel { get; set; }
    public string? BaseCurrencyCode { get; set; }
    public decimal? MaximumAllowedYieldDeviationValue { get; set; }
    public IDictionary<string, bool>? Features { get; set; }
}

public class LabelPartnerCryptoCurrency
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public bool IsActive { get; set; }
    public bool IsEnabled { get; set; }
    public LabelPartnerBlockchain? Blockchain { get; set; }

    /// <summary>
    /// Defines the flow of cryptos when selling.
    /// </summary>
    public string? HotWalletReceiveFlow { get; set; }

    public LabelPartnerTransactionLimits? TransactionLimits { get; set; }
}

public class LabelPartnerBlockchain
{
    public string? Name { get; set; }
    public string? AddressExplorerURL { get; set; }
    public string? TransactionExplorerURL { get; set; }
}

public class LabelPartnerTransactionLimits
{
    public LabelPartnerSellLimit? Sell { get; set; }
    public LabelPartnerBuyLimit? Buy { get; set; }
}

public class LabelPartnerSellLimit
{
    /// <summary>
    /// Minimum amount allowed for a sell transaction, in digital currency.
    /// </summary>
    public decimal MinimalAmount { get; set; }
}

public class LabelPartnerBuyLimit
{
    /// <summary>
    /// Minimum buy transaction amount in base currency.
    /// </summary>
    public decimal MinimalValue { get; set; }
}

public class LabelPartnerCountry
{
    public string? Code { get; set; }
    public string? Description { get; set; }

    /// <summary>
    /// Country status: None | Blocked
    /// </summary>
    public string? Status { get; set; }
}
