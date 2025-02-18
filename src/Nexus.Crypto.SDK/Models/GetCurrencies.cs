namespace Nexus.Crypto.SDK.Models;

public class GetCurrencies
{
    /// <summary>
    /// Base currency
    /// </summary>
    public BaseCurrency BaseCurrency { get; set; }

    /// <summary>
    /// List of available currencies and their rates from the base currency
    /// </summary>
    public Currency[] Currencies { get; set; }
}

public class BaseCurrency
{
    /// <summary>
    /// Currency Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// ISO 4217 (three letter code) Currency code
    /// </summary>
    /// <example>
    /// EUR
    /// </example>
    public string Code { get; set; }
}

public class Currency
{
    /// <summary>
    /// Currency Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// ISO 4217 (three letter code) Currency code
    /// </summary>
    /// <example>
    /// EUR
    /// </example>
    public string Code { get; set; }

    /// <summary>
    /// Currency status
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Currency rate from the base currency to the current currency (rate = 1 base currency / 1 current currency)
    /// </summary>
    public decimal Rate { get; set; }

    /// <summary>
    /// Date and time when the rate was last updated (ISO 8601)
    /// </summary>
    /// <example>
    /// yyyy-MM-ddTHH:mm:ssZ
    /// </example>
    public string RateUpdated { get; set; }
}