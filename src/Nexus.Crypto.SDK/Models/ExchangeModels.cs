namespace Nexus.Crypto.SDK.Models;

public class ExchangeModel
{
    /// <summary>
    /// Unique identifier of an Exchange.
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// Name of the Exchange.
    /// </summary>
    public string? Name { get; set; }

    public string? PriceUpdated { get; set; }
    public string? ReserveUpdated { get; set; }
}

public class TradePairItem
{
    /// <summary>
    /// TradePair unique identifier.
    /// </summary>
    public string? Code { get; set; }

    public TradePairExchange? Exchange { get; set; }
    public TradePairCurrency? Currency { get; set; }
    public TradePairCrypto? Crypto { get; set; }
    public TradePairPrice? Price { get; set; }

    /// <summary>
    /// TradePair flags.
    /// </summary>
    public IEnumerable<string>? Supports { get; set; }

    public TradePairOverallStatus? Status { get; set; }
}

public class TradePairExchange
{
    public string? Code { get; set; }
    public string? Name { get; set; }
}

public class TradePairCurrency
{
    public string? Code { get; set; }
    public string? Name { get; set; }
}

public class TradePairCrypto
{
    public string? Code { get; set; }
    public string? Name { get; set; }
}

public class TradePairPrice
{
    public decimal? Bid { get; set; }
    public decimal? Ask { get; set; }
    public string? Updated { get; set; }
}

public class TradePairOverallStatus
{
    /// <summary>
    /// TradePair is usable to trade with.
    /// </summary>
    public bool Tradeable { get; set; }

    /// <summary>
    /// TradePair is enabled by LabelPartner.
    /// </summary>
    public bool Enabled { get; set; }

    public bool SystemEnabled { get; set; }
    public bool Updated { get; set; }
    public bool ExchangeSystemEnabled { get; set; }
    public bool ExchangeEnabled { get; set; }
}

public class CreateOrderRequest
{
    public required string ExchangeCode { get; set; }

    /// <summary>
    /// Buy | Sell
    /// </summary>
    public required string Action { get; set; }

    /// <summary>
    /// Market | Limit
    /// </summary>
    public required string Type { get; set; }

    public required string Crypto { get; set; }

    /// <summary>
    /// FIAT code (ISO 4217).
    /// </summary>
    public required string Currency { get; set; }

    /// <summary>
    /// Amount in FIAT.
    /// </summary>
    public decimal Amount { get; set; }

    public decimal? LimitPrice { get; set; }

    /// <summary>
    /// DateTime indicating the expiration of this order. Defaults to Created +24h.
    /// </summary>
    public DateTime? Expiring { get; set; }

    public IDictionary<string, string>? Data { get; set; }
}


public class CreateTransferRequest
{
    /// <summary>
    /// Transfer's source type.
    /// </summary>
    public string? SourceType { get; set; }

    /// <summary>
    /// Transfer's sink type.
    /// </summary>
    public string? SinkType { get; set; }

    public string? SourceExchangeCode { get; set; }
    public string? SinkExchangeCode { get; set; }

    /// <summary>
    /// Transfer amount in crypto.
    /// </summary>
    public decimal Amount { get; set; }

    public string? CryptoCode { get; set; }
    public string? TransferAddress { get; set; }
    public string? UserName { get; set; }
}
