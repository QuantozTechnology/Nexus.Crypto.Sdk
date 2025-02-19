using System.Text.Json.Serialization;

namespace Nexus.Crypto.SDK.Models.Broker;

public enum TransactionSummaryPeriod
{
    Day,
    Week,
    Month
}

public class GetTransaction
{
    public string Created { get; set; }
    public string TransactionCode { get; set; }
    public string TransactionType { get; set; }
    public string Status { get; set; }
    public string Type { get; set; }
    public string PortfolioCode { get; set; }

    public string CustomerCode { get; set; }
    public string AccountCode { get; set; }
    public string CryptoCurrencyCode { get; set; }
    public string CurrencyCode { get; set; }
    [JsonPropertyName("EnchangeCode")]
    public string ExchangeCode { get; set; }
    public string PaymentMethodCode { get; set; }
    public string ExchangeOrderCode { get; set; }

    public string CryptoSendTxId { get; set; }
    public string CryptoReceiveTxId { get; set; }
    public string Notified { get; set; }
    public string Traded { get; set; }
    public string Confirmed { get; set; }
    public string Finished { get; set; }
    public string Comment { get; set; }
    public double? CryptoAmount { get; set; }
    public double? CryptoExpectedAmount { get; set; }
    public double? CryptoSent { get; set; }
    public double? CryptoTraded { get; set; }
    public double? CryptoEstimatePrice { get; set; }
    public double? CryptoTradePrice { get; set; }
    public double? CryptoPrice { get; set; }
    public double? TradeValue { get; set; }
    public double? BankCommission { get; set; }
    public double? PartnerCommission { get; set; }
    public double? NetworkCommission { get; set; }
    public double? Payout { get; set; }
    public string PayComment { get; set; }
    public string BankTransferReference { get; set; }
    public bool IsSettled { get; set; }
}

public class TransactionTotals
{
    public string Status { get; set; }
    public string CurrencyCode { get; set; }
    public int TransactionCount { get; set; }
    public double? PayoutValue { get; set; }
    public double? TradeValue { get; set; }
    public double? BankCommission { get; set; }
    public double? PartnerCommission { get; set; }
    public double? NetworkCommission { get; set; }
}
