namespace Nexus.Crypto.SDK.Models;

public class GetOrdersRequest
{
    public string? OrderCode { get; set; }
    public DateTime? CreatedFrom { get; set; }
    public DateTime? CreatedTill { get; set; }
    public DateTime? FinishedFrom { get; set; }
    public DateTime? FinishedTill { get; set; }
    public GetOrderStatus? Status { get; set; }
    public string? ExchangeCode { get; set; }
    public string? CryptoCode { get; set; }
    public string? CurrencyCode { get; set; }
    public GetOrderType? OrderType { get; set; }
    public GetOrderAction? ActionType { get; set; }
    public decimal? AmountFrom { get; set; }
    public decimal? AmountTo { get; set; }
    public bool? IsPartial { get; set; }
    public int? Page { get; set; }
    public int? Limit { get; set; }
    public string? SortBy { get; set; }
    public SortDirection? SortDirection { get; set; }
}

public enum GetOrderStatus
{
    Initiated,
    Queued,
    Open,
    ToCancel,
    FILLED,
    REJECTED,
    EXPIRED,
    CANCELLED,
}

public enum GetOrderType
{
    Market,
    Limit,
}

public enum GetOrderAction
{
    Buy,
    Sell,
}

public class ListOrder
{
    /// <summary>
    /// Order unique identifier
    /// </summary>
    /// <example>
    /// ORD-QQ-20181023175123ORZ9
    /// </example>
    public string OrderCode { get; set; }

    /// <summary>
    /// Order unique identifier on the Exchange
    /// </summary>
    /// <example>
    /// OFM5EI-5F4DY-VYA6GH
    /// </example>
    public string ExchangeTradeCode { get; set; }

    /// <summary>
    /// Exchange unique identifier
    /// </summary>
    public GetOrderTradePair TradePair { get; set; }

    /// <summary>
    /// Action type for the order
    /// </summary>
    public GetOrderAction Action { get; set; }

    /// <summary>
    /// Type of the order
    /// </summary>
    public GetOrderType Type { get; set; }

    /// <summary>
    /// Status of the order
    /// </summary>
    public GetOrderStatus Status { get; set; }

    /// <summary>
    /// Amount in FIAT
    /// </summary>
    /// <example>
    /// 100.00
    /// </example>
    public decimal Amount { get; set; }

    /// <summary>
    /// Maximum Limit Amount in FIAT
    /// </summary>
    /// <example>
    /// 100.00
    /// </example>
    public decimal? LimitPrice { get; set; }

    /// <summary>
    /// DateTime when we received this Order.
    /// </summary>
    /// <example>
    /// 2018-10-21T13:28:06.419Z
    /// </example>
    public DateTime Created { get; set; }

    /// <summary>
    /// DateTime when the Order was finished.
    /// This includes cancellations and other error statuses.
    /// </summary>
    /// <example>
    /// 2018-10-21T13:28:06.419Z
    /// </example>
    public DateTime? Finished { get; set; }

    /// <summary>
    /// Validity time of the Order.
    /// This might be later than the Finished Datetime.
    /// </summary>
    /// <example>
    /// 2018-10-21T13:28:06.419Z
    /// </example>
    public DateTime? OriginalValidTill { get; set; }

    /// <summary>
    /// Result of the order
    /// </summary>
    public GetOrderExecuted Executed { get; set; }
}

public class GetOrderExecuted
{
    /// <summary>
    /// Amount in crypto that is executed on the Exchange
    /// </summary>
    /// <example>
    /// 2
    /// </example>
    public decimal? Amount { get; set; }

    /// <summary>
    /// Price of the executed order
    /// </summary>
    /// <example>
    /// 3000
    /// </example>
    public decimal? Price { get; set; }

    /// <summary>
    /// Total value of the executed order
    /// </summary>
    /// <example>
    /// 6000
    /// </example>
    public decimal? Value { get; set; }

    /// <summary>
    /// Is true when the requested amount does not match the executed amount.
    /// Meaning that the order was partially filled.
    /// </summary>
    /// <example>
    /// false
    /// </example>
    public bool Partial { get; set; }

    /// <summary>
    /// Fee paid for the order on the exchange in FIAT currency
    /// </summary>
    /// <example>
    /// 0.0015 EUR
    /// </example>
    public decimal? Fee { get; set; }
}

public class GetOrderTradePair
{
    public string ExchangeCode { get; set; }

    /// <summary>
    /// Unique identifier of the currency (ISO 4217)
    /// </summary>
    public string CurrencyCode { get; set; }

    public string CryptoCode { get; set; }
}