namespace Nexus.Crypto.SDK.Models;

public class BuyRequest
{
    /// <summary>
    /// IP address field for tracking requests.
    /// </summary>
    public string? Ip { get; set; }

    /// <summary>
    /// PaymentMethod to handle payment validation.
    /// </summary>
    public required string PaymentMethodCode { get; set; }

    /// <summary>
    /// AccountCode to set the transaction initiator.
    /// </summary>
    public string? AccountCode { get; set; }

    /// <summary>
    /// Amount in FIAT.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// ISO 4217 (three letter code).
    /// </summary>
    public string? Currency { get; set; }

    /// <summary>
    /// Optional given price.
    /// </summary>
    public decimal? BuyPrice { get; set; }

    /// <summary>
    /// Callback url, required to receive notifications on status updates.
    /// </summary>
    public string? CallbackUrl { get; set; }

    /// <summary>
    /// Will auto create or use an existing Customer and Account.
    /// </summary>
    public BuyRequestAutoCreate? Customer { get; set; }

    /// <summary>
    /// Message to be sent to the Blockchain (if the Blockchain allows messages).
    /// </summary>
    public string? BlockchainMessage { get; set; }
}

public class BuyRequestAutoCreate
{
    /// <summary>
    /// Create (when missing) or use a Customer with this CustomerCode.
    /// </summary>
    public string? CustomerCode { get; set; }

    /// <summary>
    /// Specify the Crypto for the CustomerCryptoAddress.
    /// </summary>
    public string? CryptoCode { get; set; }

    /// <summary>
    /// Add this Address to the Customer when missing.
    /// </summary>
    public string? CustomerCryptoAddress { get; set; }

    /// <summary>
    /// In case of creating an account for the customer: Broker | BrokerBuyOnly
    /// </summary>
    public string? AccountType { get; set; }

    /// <summary>
    /// The customer's status in case of creating a new customer.
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// The customer's trust level in case of creating a new customer.
    /// </summary>
    public string? TrustLevel { get; set; }

    /// <summary>
    /// The customer's portfolio code in case of creating a new customer.
    /// </summary>
    public string? PortfolioCode { get; set; }

    /// <summary>
    /// The customer's currency in case of creating a new customer.
    /// </summary>
    public string? CurrencyCode { get; set; }

    /// <summary>
    /// The customer's email in case of creating a new customer.
    /// </summary>
    public string? Email { get; set; }
}

public class BuyResponse
{
    /// <summary>
    /// Transaction unique identifier.
    /// </summary>
    public string? TransactionCode { get; set; }

    /// <summary>
    /// Date and Time of creation.
    /// </summary>
    public string? Created { get; set; }

    public string? AccountCode { get; set; }
    public string? CustomerCode { get; set; }
    public decimal? NetworkFee { get; set; }
    public decimal? ServiceFee { get; set; }
    public decimal? BankFee { get; set; }
    public string? Comment { get; set; }

    /// <summary>
    /// Status of the transaction.
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// Message sent as part of the transaction in the Blockchain.
    /// </summary>
    public string? BlockchainMessage { get; set; }
}

public class BuySimulateRequest
{
    public required string AccountCode { get; set; }
    public required string PaymentMethodCode { get; set; }
    public required string Currency { get; set; }
    public decimal Amount { get; set; }
    public decimal? BuyPrice { get; set; }
    public string? BlockchainMessage { get; set; }
}

public class BuySimulateResponse
{
    public decimal CryptoBuyPriceBeforeFee { get; set; }
    public decimal CryptoBuyPriceAfterServiceFee { get; set; }
    public decimal CryptoAmount { get; set; }
    public decimal CurrencyBankFee { get; set; }
    public decimal CurrencyServiceFee { get; set; }
    public decimal CurrencyNetworkFee { get; set; }
    public decimal TotalCurrency { get; set; }
    public decimal TotalCurrencyToPay { get; set; }
    public string? CurrencyCode { get; set; }
    public string? CryptoCode { get; set; }
    public string? BlockchainMessage { get; set; }
}

public class SellRequest
{
    public string? Ip { get; set; }
    public required string AccountCode { get; set; }
    public required string PaymentMethodCode { get; set; }
    public decimal CryptoAmount { get; set; }

    /// <summary>
    /// Optional given price. 5% increase / 10% reduction allowed.
    /// </summary>
    public decimal? SellPrice { get; set; }
}

public class SellResponse
{
    public string? AccountCode { get; set; }
    public string? TransactionCode { get; set; }
    public string? CustomerCode { get; set; }
    public string? CryptoAddress { get; set; }
    public string? CurrencyCode { get; set; }
    public decimal ValueInFiatBeforeFees { get; set; }
    public decimal ValueInFiatAfterFees { get; set; }
    public decimal NetworkFee { get; set; }
    public decimal ServiceFee { get; set; }
    public decimal BankFee { get; set; }
}

public class SellSimulateRequest
{
    public string? Ip { get; set; }
    public required string AccountCode { get; set; }
    public required string PaymentMethodCode { get; set; }
    public decimal CryptoAmount { get; set; }
    public decimal? SellPrice { get; set; }
}

public class SellSimulateResponse
{
    public decimal ValueInFiatBeforeFees { get; set; }
    public decimal ValueInFiatAfterFees { get; set; }
    public decimal BankFee { get; set; }
    public decimal ServiceFee { get; set; }
    public decimal NetworkFee { get; set; }
}
