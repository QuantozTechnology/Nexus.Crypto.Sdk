namespace Nexus.LabelApi.SDK.Models;

public class GetTokenPaymentsTotals
{
    public string PaymentType { get; set; }
    public decimal? TokenAmount { get; set; }
    public decimal FiatValue { get; set; }
    public int Count { get; set; }
}
