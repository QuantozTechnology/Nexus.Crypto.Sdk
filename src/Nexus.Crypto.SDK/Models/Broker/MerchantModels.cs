namespace Nexus.Crypto.SDK.Models;

public class GetMerchantTransactionResponse
{
    /// <summary>
    /// Status of merchant transaction.
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// Unique identifier of merchant.
    /// </summary>
    public string? MerchantCustomerCode { get; set; }

    /// <summary>
    /// Unique identifier of merchant transaction.
    /// </summary>
    public string? TransactionCode { get; set; }

    /// <summary>
    /// Blockchain reference required for certain crypto.
    /// </summary>
    public string? BlockchainReference { get; set; }

    /// <summary>
    /// Unique identifier of the fiat currency (ISO 4217).
    /// </summary>
    public string? CurrencyCode { get; set; }

    public decimal? CurrencyAmount { get; set; }
    public decimal? FeeAmount { get; set; }

    /// <summary>
    /// Unique identifier of the crypto.
    /// </summary>
    public string? CryptoCurrencyCode { get; set; }

    public string? PaymentReference { get; set; }
    public decimal? ExpectedCryptoAmount { get; set; }
    public decimal? ReceivedCryptoAmount { get; set; }

    /// <summary>
    /// Created date of merchant transaction.
    /// </summary>
    public string? Created { get; set; }

    /// <summary>
    /// Timestamp until merchant transaction is valid.
    /// </summary>
    public string? ValidUntil { get; set; }

    public string? MerchantCustomerEmailAddress { get; set; }
    public string? ReceiveCryptoTxId { get; set; }
    public string? CryptoPaymentAddress { get; set; }
    public string? CryptoPaymentUri { get; set; }
    public string? AccountCode { get; set; }
    public string? Comment { get; set; }
    public string? PaymentMethodCode { get; set; }
}

public class CreateMerchantTransactionRequest
{
    public required string AccountCode { get; set; }
    public required string MerchantCustomerCode { get; set; }
    public required string PaymentMethodCode { get; set; }

    /// <summary>
    /// Amount in merchant customer FIAT currency (only considered if crypto amount was not specified).
    /// </summary>
    public decimal? Amount { get; set; }

    public string? CryptoCode { get; set; }

    /// <summary>
    /// Amount in merchant account CRYPTO currency.
    /// </summary>
    public decimal? CryptoAmount { get; set; }

    public string? PaymentReference { get; set; }
    public string? MerchantCustomerEmailAddress { get; set; }
    public string? CallbackUrl { get; set; }
}

public class UpdateMerchantTransactionRequest
{
    /// <summary>
    /// Update the status of merchant transaction from "ToPayout" or "PayoutConfirming" to "SellCompleted".
    /// </summary>
    public string? Status { get; set; }
}

public class SimulateMerchantTransactionResponse
{
    public string? MerchantCustomerCode { get; set; }
    public string? CurrencyCode { get; set; }
    public decimal? CurrencyAmount { get; set; }
    public decimal? FeeAmount { get; set; }
    public string? CryptoCode { get; set; }
    public decimal? ExpectedCryptoAmount { get; set; }
    public string? PaymentMethodCode { get; set; }
}
