using System.Collections.Generic;

namespace Nexus.LabelApi.SDK.Models
{
    public class BalanceItem_1_2
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
    public class GetCustodianBalances_1_2
    {
        public IEnumerable<BalanceItem_1_2> CurrencyBalances { get; set; }
        public IEnumerable<BalanceItem_1_2> CryptoBalances { get; set; }
    }
}
