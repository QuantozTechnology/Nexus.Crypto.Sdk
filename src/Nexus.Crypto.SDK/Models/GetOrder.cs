namespace Nexus.Crypto.SDK.Models;

public class GetOrderTradePair
{
    public string ExchangeCode { get; set; }
    public string CryptoCode { get; set; } // "TokenCode"
}

public class GetOrder
{
    public GetOrderTradePair TradePair { get; set; }
    public string Status { get; set; }
    public DateTime? Finished { get; set; }
}

public enum GetOrderStatus
{

}