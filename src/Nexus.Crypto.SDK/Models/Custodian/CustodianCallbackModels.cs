namespace Nexus.Crypto.SDK.Models.Custodian;

public class CustodianTransactionNotificationCallbackResponse
{
    public CustodianLabelNotificationResponse? Notification { get; set; }
    public string? LabelPartnerCode { get; set; }
    public string? TransactionCode { get; set; }

    /// <summary>
    /// Url of the callback. Maximum length is 256 characters.
    /// </summary>
    public string? Url { get; set; }

    public bool? IsSuccess { get; set; }
    public string? Finished { get; set; }
    public string? Inserted { get; set; }
    public int? DequeueCount { get; set; }
}

public class CustodianLabelNotificationResponse
{
    public string? Status { get; set; }
    public string? Type { get; set; }
    public string? CustomerReference { get; set; }
    public string? AccountCode { get; set; }
    public string? TransactionCode { get; set; }
    public string? CurrencyCode { get; set; }
    public string? CryptoCurrencyCode { get; set; }
    public string? PaymentMethodCode { get; set; }
    public string? Created { get; set; }
    public string? ValidUntil { get; set; }
    public CustodianLabelNotificationTxBuyResponse? Buy { get; set; }
    public CustodianLabelNotificationTxSellResponse? Sell { get; set; }
    public CustodianLabelNotificationTxMerchantResponse? Merchant { get; set; }
    public CustodianLabelNotificationTxSendoutResponse? Sendout { get; set; }
    public CustodianLabelNotificationTxReceiveInResponse? ReceiveIn { get; set; }
    public CustodianLabelNotificationConfirmationResponse? Confirmations { get; set; }
}

public class CustodianLabelNotificationTxBuyResponse
{
    public string? SendCryptoTxId { get; set; }
    public decimal? CryptoCurrencyAmount { get; set; }
    public decimal? CurrencyAmount { get; set; }
}

public class CustodianLabelNotificationTxSellResponse
{
    public string? ReceiveCryptoTxId { get; set; }
}

public class CustodianLabelNotificationTxMerchantResponse
{
    public decimal? CurrencyAmount { get; set; }
    public string? ReceiveCryptoTxId { get; set; }
    public string? CryptoPaymentAddress { get; set; }
    public string? CryptoPaymentUri { get; set; }
    public string? PaymentReference { get; set; }
    public decimal? ExpectedCryptoAmount { get; set; }
    public decimal? ReceivedCryptoAmount { get; set; }
    public string? MerchantCustomerEmailAddress { get; set; }
}

public class CustodianLabelNotificationTxSendoutResponse
{
    public string? TxId { get; set; }
}

public class CustodianLabelNotificationTxReceiveInResponse
{
    public string? TxId { get; set; }
    public decimal? ReceivedCryptoAmount { get; set; }
    public decimal? ReceivedFiatValue { get; set; }
}

public class CustodianLabelNotificationConfirmationResponse
{
    public int? Count { get; set; }
    public int? Required { get; set; }
}
