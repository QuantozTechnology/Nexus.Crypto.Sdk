namespace Nexus.Crypto.SDK.Models;

public class HotWalletBalance
{
    /// <summary>
    /// Crypto identifier.
    /// </summary>
    public string? CryptoCode { get; set; }

    /// <summary>
    /// Crypto name.
    /// </summary>
    public string? CryptoName { get; set; }

    public decimal Available { get; set; }
    public decimal Unconfirmed { get; set; }
    public decimal ConfirmedReserve { get; set; }
    public decimal DelayedSending { get; set; }
}
