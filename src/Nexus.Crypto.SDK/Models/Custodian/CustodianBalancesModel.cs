namespace Nexus.Crypto.SDK.Models;

public class CustodianBalanceItem
{
    public string Code { get; set; }
    public string Name { get; set; }
    public decimal Locked { get; set; }
    public decimal Total { get; set; }
    public decimal Available { get; set; }
}

/// <summary>
/// Get Custodian Balance
/// </summary>
public class GetCustodianBalances
{
    public IEnumerable<CustodianBalanceItem> CryptoBalances { get; set; }
}
