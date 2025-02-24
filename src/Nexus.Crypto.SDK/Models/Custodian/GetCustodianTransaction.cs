namespace Nexus.Crypto.SDK.Models.Custodian;

public class CustodianCancelResponse
{
    /// <summary>
    /// Cancelled transaction
    /// </summary>
    public CreateCustodianTransactionResponse Transaction { get; set; }
}

public enum TransactionTypes
{
    BUY = 1,
    SELL = 2,
    SENDINTERNAL = 3,
    SENDOUT = 4,
    SWAP = 5,
    RECEIVEIN = 6,
    GIFT = 8,
    SENDTOBUCKET = 9,
    INTEREST = 10,
    CLAWBACK = 11,
}

public enum Direction
{
    SOURCE = 1,
    DESTINATION = 2
}

public enum CustodianTransactionStatus
{
    COMPLETED = 1,
    Confirming = 2,
    Sending = 3,
    SIMULATED = 4,
    CANCELLED = 5,
    BLOCKED = 6,
    Staged = 7,
    ToCancel = 8,
    Initiated = 9
}

public class ListCustodianTransactionResponse
{
    /// <summary>
    /// Unique identifier of the customer
    /// </summary>
    public string CustomerCode { get; set; }

    /// <summary>
    /// Nexus generated unique identifier of the account
    /// </summary>
    public string AccountCode { get; set; }

    /// <summary>
    /// Unique identifier of the transaction
    /// </summary>
    public string TransactionCode { get; set; }

    /// <summary>
    /// Unique identifier of the related transaction
    /// Used for SendInternal and Swap
    /// </summary>
    public string LinkedTransactionCode { get; set; }

    /// <summary>
    /// Url to send callback notifications on status updates to.
    /// </summary>
    public string CallbackUrl { get; set; }

    /// <summary>
    /// Crypto address to which this transaction is sent out to (SENDOUT only)
    /// </summary>
    public string DestinationCryptoAddress { get; set; }

    /// <summary>
    /// Crypto code
    /// </summary>
    /// <example>
    /// BTC
    /// </example>
    public string CryptoCode { get; set; }

    /// <summary>
    /// Currency code (ISO 4217)
    /// </summary>
    /// <example>
    /// EUR
    /// </example>
    public string CurrencyCode { get; set; }

    /// <summary>
    /// Unique identifier of the payment method
    /// </summary>
    /// <example>
    /// EX_GENERIC_BTC_EUR
    /// </example>
    public string PaymentMethodCode { get; set; }

    /// <summary>
    /// Transaction type
    /// </summary>
    public TransactionTypes Type { get; set; }

    /// <summary>
    /// Transaction sub type
    /// </summary>
    public Direction? Direction { get; set; }

    /// <summary>
    /// Transaction status
    /// </summary>
    public CustodianTransactionStatus Status { get; set; }

    /// <summary>
    /// Transaction fees
    /// </summary>
    public FeeResponse Fees { get; set; }

    /// <summary>
    /// Original requested amounts
    /// </summary>
    public AmountsResponse RequestedAmounts { get; set; }

    /// <summary>
    /// Adjusted amounts executed by Nexus
    /// </summary>
    public AmountsResponse ExecutedAmounts { get; set; }

    /// <summary>
    /// Change in balance for the customer
    /// </summary>
    public BalanceMutationResponse BalanceMutation { get; set; }

    /// <summary>
    /// Date and time this transaction was created
    /// </summary>
    public string Created { get; set; }

    /// <summary>
    /// Margin made from Swap transactions in crypto
    /// </summary>
    public decimal? SwapMarginAmountCrypto { get; set; }

    /// <summary>
    /// Message sent as part of the transaction in the Blockchain
    /// </summary>
    public string BlockchainMessage { get; set; }

    /// <summary>
    /// Id of the related Transaction in the Blockchain
    /// </summary>
    public string BlockchainTransactionId { get; set; }
}

public class CreateCustodianTransactionResponse : ListCustodianTransactionResponse
{
    /// <summary>
    /// Resulting new transaction's data
    /// </summary>
    public IDictionary<string, string> Data { get; set; }
}


public class FeeResponse
{
    /// <summary>
    /// Service fee
    /// </summary>
    /// <example>
    /// 2.50
    /// </example>
    public decimal? PartnerFeeFiat { get; set; }

    /// <summary>
    /// Bank fee
    /// </summary>
    /// <example>
    /// 3.50
    /// </example>
    public decimal? BankFeeFiat { get; set; }

    /// <summary>
    /// Estimated network fee in fiat paid by the customer.
    /// In case of RECEIVE IN it is the estimated forwarding fee in fiat e.g. ETH
    /// </summary>
    /// <example>
    /// 4.50
    /// </example>
    public decimal? NetworkFeeFiat { get; set; }

    /// <summary>
    /// Estimated network fee in crypto paid by the customer.
    /// In case of RECEIVE IN it is the estimated forwarding fee in crypto e.g. ETH
    /// </summary>
    /// <example>
    /// 0.00000276
    /// </example>
    public decimal? NetworkFeeCrypto { get; set; }

    /// <summary>
    /// Fee in fiat used to get this transaction confirmed on the blockchain
    /// </summary>
    /// <example>
    /// 4.50
    /// </example>
    public decimal? ActualNetworkFeeFiat { get; set; }

    /// <summary>
    /// Fee in crypto used to get this transaction confirmed on the blockchain
    /// </summary>
    /// <example>
    /// 0.00000276
    /// </example>
    public decimal? ActualNetworkFeeCrypto { get; set; }
}

public class AmountsResponse
{
    /// <example>
    /// 0.02344567
    /// </example>
    public decimal? CryptoAmount { get; set; }

    /// <example>
    /// 50.00
    /// </example>
    public decimal? FiatValue { get; set; }

    /// <example>
    /// 4500.00000
    /// </example>
    public decimal? CryptoPrice { get; set; }
}

public class BalanceMutationResponse
{
    /// <summary>
    /// Change in crypto balance (can be negative)
    /// </summary>
    /// <example>
    /// 0.02344567
    /// </example>
    public decimal? Crypto { get; set; }

    /// <summary>
    /// Change in fiat balance (can be negative).
    /// For a BUY this absoulte amount is paid by the customer.
    /// For a SELL this amount paid out to the customer.
    /// </summary>
    /// <example>
    /// -5000.00
    /// </example>
    public decimal? Fiat { get; set; }
}
