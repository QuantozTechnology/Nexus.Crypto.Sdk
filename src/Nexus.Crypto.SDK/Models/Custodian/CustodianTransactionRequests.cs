namespace Nexus.Crypto.SDK.Models.Custodian;

public class CustodianBuyRequest
{
    public required string CustomerCode { get; set; }
    public string? AccountCode { get; set; }

    /// <summary>
    /// Three letter code of the currency (ISO 4217).
    /// </summary>
    public required string CurrencyCode { get; set; }

    public required string CryptoCode { get; set; }
    public decimal FiatValue { get; set; }

    /// <summary>
    /// Optional fixed BUY price. Nexus allows 10% increase / 5% reduction.
    /// </summary>
    public decimal? RequestedPrice { get; set; }

    public required string PaymentMethodCode { get; set; }

    /// <summary>
    /// Optional bank account number for reporting purposes only.
    /// </summary>
    public string? BankAccountNumber { get; set; }

    public IDictionary<string, string>? Data { get; set; }
}

public class CustodianSellRequest
{
    public required string CustomerCode { get; set; }
    public string? AccountCode { get; set; }
    public required string CryptoCode { get; set; }
    public decimal CryptoAmount { get; set; }
    public required string CurrencyCode { get; set; }

    /// <summary>
    /// Optional fixed SELL price. Nexus allows 5% increase / 10% reduction.
    /// </summary>
    public decimal? RequestedPrice { get; set; }

    public required string PaymentMethodCode { get; set; }
    public string? BankAccountNumber { get; set; }
    public IDictionary<string, string>? Data { get; set; }
}

public class CustodianGiftRequest
{
    public required string CustomerCode { get; set; }
    public string? AccountCode { get; set; }
    public required string CurrencyCode { get; set; }
    public required string CryptoCode { get; set; }
    public decimal FiatValue { get; set; }
    public decimal? RequestedPrice { get; set; }
    public required string PaymentMethodCode { get; set; }
    public IDictionary<string, string>? Data { get; set; }
}

public class CustodianClawbackRequest
{
    public required string CustomerCode { get; set; }
    public string? AccountCode { get; set; }
    public required string CurrencyCode { get; set; }
    public required string CryptoCode { get; set; }
    public decimal CryptoAmount { get; set; }
    public decimal? RequestedPrice { get; set; }
    public IDictionary<string, string>? Data { get; set; }
}

public class CustodianSendInternalRequest
{
    public required string PaymentMethodCode { get; set; }
    public required CustodianTxSource Source { get; set; }
    public required SendInternalDestination Destination { get; set; }
    public IDictionary<string, string>? Data { get; set; }
}

public class CustodianSendOutRequest
{
    public required string PaymentMethodCode { get; set; }
    public string? CallbackUrl { get; set; }
    public string? BlockchainMessage { get; set; }
    public IDictionary<string, string>? Data { get; set; }
    public required CustodianTxSource Source { get; set; }
    public required SendOutDestination Destination { get; set; }
}

public class CustodianSendToBucketRequest
{
    public required string PaymentMethodCode { get; set; }
    public required CustodianTxSourceWithBucket Source { get; set; }
    public SendToBucketDestination? Destination { get; set; }
    public IDictionary<string, string>? Data { get; set; }
}

public class CustodianSwapRequest
{
    public required string PaymentMethodCode { get; set; }
    public required string CustomerCode { get; set; }
    public required SwapSource Source { get; set; }
    public required SwapDestination Destination { get; set; }
}

public class CustodianPaymentRequest
{
    public string? CustomerCode { get; set; }
    public string? AccountCode { get; set; }

    /// <summary>
    /// Optionally re-use a previously used receive address.
    /// </summary>
    public string? ReceiveAddress { get; set; }

    public required string CurrencyCode { get; set; }
    public required string CryptoCode { get; set; }
    public decimal? CryptoAmount { get; set; }
    public decimal? RequestedPrice { get; set; }
    public required string PaymentMethodCode { get; set; }
    public string? CallbackUrl { get; set; }
    public IDictionary<string, string>? Data { get; set; }
}

// Source/Destination helpers

public class CustodianTxSource
{
    public required string CustomerCode { get; set; }
    public string? AccountCode { get; set; }
    public required string CryptoCode { get; set; }
    public required string CurrencyCode { get; set; }
    public decimal CryptoAmount { get; set; }
}

public class CustodianTxSourceWithBucket : CustodianTxSource
{
    /// <summary>
    /// Code of the source bucket. Leave empty to use main bucket.
    /// </summary>
    public string? BucketCode { get; set; }
}

public class SendInternalDestination
{
    public required string CustomerCode { get; set; }
    public string? AccountCode { get; set; }
}

public class SendOutDestination
{
    public required string Address { get; set; }
}

public class SendToBucketDestination
{
    /// <summary>
    /// Code of the destination bucket. Leave empty to use main bucket.
    /// </summary>
    public string? BucketCode { get; set; }

    public string? AccountCode { get; set; }
}

public class SwapSource
{
    public string? AccountCode { get; set; }
    public required string CryptoCode { get; set; }
    public decimal CryptoAmount { get; set; }
    public decimal? RequestedPrice { get; set; }
    public IDictionary<string, string>? Data { get; set; }
}

public class SwapDestination
{
    public string? AccountCode { get; set; }
    public required string CryptoCode { get; set; }
    public decimal? RequestedPrice { get; set; }
    public IDictionary<string, string>? Data { get; set; }
}

// Response wrappers

public class CustodianBuyResponse
{
    public CreateCustodianTransactionResponse? Transaction { get; set; }
}

public class CustodianSellResponse
{
    public CreateCustodianTransactionResponse? Transaction { get; set; }
}

public class CustodianGiftResponse
{
    public CreateCustodianTransactionResponse? Transaction { get; set; }
}

public class CustodianClawbackResponse
{
    public CreateCustodianTransactionResponse? Transaction { get; set; }
}

public class CustodianSendOutResponse
{
    public CreateCustodianTransactionResponse? Transaction { get; set; }
}

public class CustodianSendInternalResponse
{
    public CreateCustodianTransactionResponse? SourceTransaction { get; set; }
    public CreateCustodianTransactionResponse? DestinationTransaction { get; set; }
}

public class CustodianSwapResponse
{
    public CreateCustodianTransactionResponse? SourceTransaction { get; set; }
    public CreateCustodianTransactionResponse? DestinationTransaction { get; set; }
}

public class CustodianSwapSimulationResponse
{
    public IEnumerable<object>? CryptoBalances { get; set; }
    public CreateCustodianTransactionResponse? SourceTransaction { get; set; }
    public CreateCustodianTransactionResponse? DestinationTransaction { get; set; }
}

public class CustodianPaymentRequestResponse
{
    public CreateCustodianTransactionResponse? Transaction { get; set; }

    /// <summary>
    /// A payment string that can be represented as a QR code for the sender.
    /// </summary>
    public string? PaymentUri { get; set; }
}
