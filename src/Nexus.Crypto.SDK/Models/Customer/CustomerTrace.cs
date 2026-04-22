using System.Text.Json.Serialization;

namespace Nexus.Crypto.SDK.Models;

public class GetCustomerTraceSummary
{
    /// <summary>
    /// Distinct IP address of customer traces
    /// </summary>
    public string IP { get; set; }

    /// <summary>
    /// Country code of GeoLocation of the distinct IP
    /// </summary>
    public string CountryCode { get; set; }

    /// <summary>
    /// Country name of GeoLocation of the distinct IP
    /// </summary>
    public string CountryName { get; set; }

    /// <summary>
    /// ISP of the distinct IP
    /// </summary>
    public string ISP { get; set; }

    /// <summary>
    /// Number of times the distinct IP was logged for the customer
    /// </summary>
    public int Count { get; set; }
}

public class GetCustomerTrace
{
    /// <summary>
    /// IP address of customer trace
    /// </summary>
    public string IP { get; set; }

    /// <summary>
    /// Creation date of customer trace
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// Action of customer trace
    /// </summary>
    public string Action { get; set; }

    /// <summary>
    /// Entity type of customer trace
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public APIRequestEntityType? EntityType { get; set; }

    /// <summary>
    /// Location info of customer trace
    /// </summary>
    public GeoLocation GeoLocation { get; set; }
}


public class GeoLocation
{
    /// <summary>
    /// Country code of GeoLocation
    /// </summary>
    public string CountryCode { get; set; }

    /// <summary>
    /// Country name of GeoLocation
    /// </summary>
    public string CountryName { get; set; }

    /// <summary>
    /// ISP of GeoLocation
    /// </summary>
    public string ISP { get; set; }
}


public enum APIRequestEntityType
{
    Account,
    Customer,
    CustodianBuyTransaction,
    CustodianGiftTransaction,
    CustodianSellTransaction,
    CustodianSendInternalTransaction,
    CustodianSendOutTransaction,
    CustodianSwapTransaction,
    Order,
    BrokerBuyTransaction,
    BrokerSellTransaction,
    CustodianTransaction,
    BrokerMerchantTransaction,
    CustodianClawbackTransaction,
}