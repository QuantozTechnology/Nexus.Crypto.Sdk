namespace Nexus.Crypto.SDK.Models;

public class PaymentMethodResponse
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? CryptoCode { get; set; }
    public string? CurrencyCode { get; set; }

    /// <summary>
    /// Transaction type: BUY | SELL | SENDINTERNAL | SENDOUT | SWAP | RECEIVEIN | GIFT | SENDTOBUCKET | INTEREST | CLAWBACK
    /// </summary>
    public string? TransactionType { get; set; }

    public PaymentMethodFees? Fees { get; set; }
    public PaymentType? PaymentType { get; set; }
    public IDictionary<string, string>? Data { get; set; }
}

public class PaymentMethodFees
{
    public BankFees? Bank { get; set; }
    public ServiceFees? Service { get; set; }
}

public class BankFees
{
    public decimal Fixed { get; set; }
    public decimal Relative { get; set; }
}

public class ServiceFees
{
    public decimal Relative { get; set; }
}

public class PaymentType
{
    public string? Code { get; set; }
    public string? Description { get; set; }
    public string? Name { get; set; }
}

public class PaymentMethodCustomerFeeResponse
{
    /// <summary>
    /// Customer code for the fee.
    /// </summary>
    public string? CustomerCode { get; set; }

    /// <summary>
    /// Fee value as percentage (between 0 and 1).
    /// </summary>
    public decimal Value { get; set; }
}

public class UpsertPaymentMethodCustomerFeeRequest
{
    public required string CustomerCode { get; set; }

    /// <summary>
    /// Fee percentage as a decimal between 0 and 1. Example: 0.05 for 5%.
    /// </summary>
    public decimal Value { get; set; }
}

public class PaymentMethodTagFeeResponse
{
    /// <summary>
    /// Customer tag for the fee.
    /// </summary>
    public string? CustomerTag { get; set; }

    /// <summary>
    /// Fee value as percentage (between 0 and 1).
    /// </summary>
    public decimal Value { get; set; }
}

public class UpsertPaymentMethodTagFeeRequest
{
    public required string CustomerTag { get; set; }

    /// <summary>
    /// Fee percentage as a decimal between 0 and 1. Example: 0.05 for 5%.
    /// </summary>
    public decimal Value { get; set; }
}
