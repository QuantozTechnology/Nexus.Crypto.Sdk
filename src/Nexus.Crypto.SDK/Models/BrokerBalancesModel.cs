using System.Collections.Generic;

namespace Nexus.Crypto.SDK.Models;

public class BalanceItem_1_1
{
    public string BalanceCurrencyCode { get; set; }
    public double AvailableBalance { get; set; }
    public double UnconfirmedBalance { get; set; }
    public double HotConfirmedBalance { get; set; }
    public double DelayedSending { get; set; }
    public decimal ColdStoreBalance { get; set; }
}

/// <summary>
/// Get Broker Balances
/// </summary>
public class GetBrokerBalances_1_1
{
    public IEnumerable<BalanceItem_1_1> Balances { get; set; }
}
