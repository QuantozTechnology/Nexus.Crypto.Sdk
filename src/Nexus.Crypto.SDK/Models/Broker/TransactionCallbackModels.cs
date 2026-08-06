namespace Nexus.Crypto.SDK.Models;

public class GetTransactionDataModel
{
    public string? Key { get; set; }
    public string? Value { get; set; }
}

public class PostTransactionDataModel
{
    public string? Value { get; set; }
}

public class UpdateBrokerTransactionRequest
{
    /// <summary>
    /// Optionally store a Payment Reference supplied by an external payment provider.
    /// </summary>
    public string? PaymentReference { get; set; }
}

public class ExecuteTransactionRequest
{
    public required string TransactionCode { get; set; }

    /// <summary>
    /// Result of the payment (e.g., PAYMENTSUCCESS).
    /// </summary>
    public required string ResultCode { get; set; }

    public bool? IsSettled { get; set; }
}

public class GetTransactionFlowResult
{
    public string? TransactionCode { get; set; }

    /// <summary>
    /// Transaction hash or operation id.
    /// </summary>
    public string? TransactionHash { get; set; }

    public IEnumerable<TransactionFlowParty>? Senders { get; set; }
    public IEnumerable<TransactionFlowParty>? Receivers { get; set; }

    /// <summary>
    /// Identifier of the token (name, contract address, assetId, etc.).
    /// </summary>
    public string? Token { get; set; }
}

public class TransactionFlowParty
{
    public string? Address { get; set; }
    public decimal Amount { get; set; }
}
