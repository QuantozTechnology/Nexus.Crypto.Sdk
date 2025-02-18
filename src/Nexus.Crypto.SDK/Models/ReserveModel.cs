namespace Nexus.Crypto.SDK.Models;

public class GetReserves
{
    public IEnumerable<ReserveItem> CurrencyReserves { get; set; }
    public IEnumerable<ReserveItem> CryptoReserves { get; set; }
}

public class ReserveItem
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string ExchangeCode { get; set; }
    public decimal Total { get; set; }
    public decimal Locked { get; set; }
    public decimal Available { get; set; }
    public decimal? Unconfirmed { get; set; }
    public string Updated { get; set; }
}
