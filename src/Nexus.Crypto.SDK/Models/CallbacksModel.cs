namespace Nexus.Crypto.SDK.Models;

public class TransactionNotificationCallbackResponse
{
    public LabelNotificationResponse Notification { get; set; }
    public string LabelPartnerCode { get; set; }
    public string TransactionCode { get; set; }
    public string Url { get; set; }
    public string Response { get; set; }
    public bool? IsSuccess { get; set; }
    public string Finished { get; set; }
    public string Inserted { get; set; }
    public int? DequeueCount { get; set; }
}

public class LabelNotificationTxBuyResponse
{
    public string SendCryptoTxId { get; set; }
    public decimal? CryptoCurrencyAmount { get; set; }
    public decimal? CurrencyAmount { get; set; }
}

public class LabelNotificationConfirmationResponse
{
    public int? Count { get; set; }
    public int? Required { get; set; }
}

public class LabelNotificationTxSendoutResponse
{
    public string TxId { get; set; }
}

public class LabelNotificationTxReceiveInResponse
{
    public string TxId { get; set; }
    public decimal? ReceivedCryptoAmount { get; set; }
}

public class LabelNotificationTxSellResponse
{
    public string ReceiveCryptoTxId { get; set; }
    public string BlockchainMessage { get; set; }
}

public class LabelNotificationTxMerchantResponse
{
    public decimal? CurrencyAmount { get; set; }
    public string ReceiveCryptoTxId { get; set; }
    public string CryptoPaymentAddress { get; set; }
    public string CryptoPaymentUri { get; set; }
    public string PaymentReference { get; set; }
    public decimal? ExpectedCryptoAmount { get; set; }
    public decimal? ReceivedCryptoAmount { get; set; }
    public string MerchantCustomerEmailAddress { get; set; }
    public string BlockchainMessage { get; set; }
}

public class LabelNotificationResponse
{
    public string Status { get; set; }
    public string Type { get; set; }
    public string CustomerReference { get; set; }
    public string AccountCode { get; set; }
    public string TransactionCode { get; set; }
    /// <summary>
    /// Unique identifier of the currency (ISO 4217)
    /// </summary>
    public string CurrencyCode { get; set; }
    public string CryptoCurrencyCode { get; set; }
    public string PaymentMethodCode { get; set; }
    public string Created { get; set; }
    public LabelNotificationTxBuyResponse Buy { get; set; }
    public LabelNotificationTxSellResponse Sell { get; set; }
    public LabelNotificationTxMerchantResponse Merchant { get; set; }
    public LabelNotificationTxSendoutResponse Sendout { get; set; }
    public LabelNotificationTxReceiveInResponse ReceiveIn { get; set; }
    public LabelNotificationConfirmationResponse Confirmations { get; set; }
    public string ValidUntil { get; set; }
}
