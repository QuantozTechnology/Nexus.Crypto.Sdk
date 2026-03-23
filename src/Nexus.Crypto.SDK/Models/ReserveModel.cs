namespace Nexus.Crypto.SDK.Models;

public class GetReserves
{
    public IEnumerable<CurrencyReserveItem> CurrencyReserves { get; set; }
    public IEnumerable<CryptoReserveItem> CryptoReserves { get; set; }
}

public class ReserveItem
{
    public string Name { get; set; }
    public ReserveType Type { get; set; }
    public string BucketCode { get; set; }
    public string ExchangeCode { get; set; }
    public decimal Total { get; set; }
    public decimal Locked { get; set; }
    public decimal Available { get; set; }
    public decimal? Unconfirmed { get; set; }
    public DateTimeOffset Updated { get; set; }

}

public class CryptoReserveItem : ReserveItem
{
    public string BlockchainCode { get; set; }
    public string TokenCode { get; set; }
    public string CryptoCode { get; set; }
}

public class CurrencyReserveItem : ReserveItem
{
    public string Code { get; set; }
}

public enum ReserveType
{
    Exchange,
    HotWallet,
    Coldstore,
    Yield
}