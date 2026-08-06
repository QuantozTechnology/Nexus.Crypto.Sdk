namespace Nexus.Crypto.SDK.Models;

public class GetCryptoAddressResponse
{
    public string? Created { get; set; }
    public string? CreatedBy { get; set; }

    /// <summary>
    /// Status(es): Inactive | Active | Deleted | Whitelisted | NotValidated | ToBeDeleted
    /// </summary>
    public IEnumerable<string>? Status { get; set; }

    public CryptoAddressSink? Sink { get; set; }
    public CryptoAddressSource? Source { get; set; }
    public string? CryptoCode { get; set; }
    public string? Address { get; set; }
    public string? AddressReference { get; set; }
    public string? Comment { get; set; }
    public string? ExchangeWalletCode { get; set; }
}

public class CryptoAddressSink
{
    /// <summary>
    /// Sink type: Other | Exchange | Coldstore | HotWallet
    /// </summary>
    public string? SinkType { get; set; }

    public string? SinkTradeExchangeCode { get; set; }
    public string? SinkTradeExchangeName { get; set; }
}

public class CryptoAddressSource
{
    /// <summary>
    /// Source type: Other | Exchange | Coldstore | HotWallet
    /// </summary>
    public string? SourceType { get; set; }

    public string? SourceTradeExchangeCode { get; set; }
    public string? SourceTradeExchangeName { get; set; }
}
