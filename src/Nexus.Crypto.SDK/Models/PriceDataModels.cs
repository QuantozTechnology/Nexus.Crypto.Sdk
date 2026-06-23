namespace Nexus.Crypto.SDK.Models;

public class GetPriceData
{
    public string? CryptoCode { get; set; }
    public string? CurrencyCode { get; set; }
    public IEnumerable<PriceDataPoint>? PriceData { get; set; }
}

public class PriceDataPoint
{
    public decimal? Buy { get; set; }
    public decimal? Sell { get; set; }
    public decimal? Mid { get; set; }
    public DateTime? Timestamp { get; set; }
}
