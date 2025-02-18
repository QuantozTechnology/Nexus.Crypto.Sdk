using System.Collections.Generic;

namespace Nexus.LabelApi.SDK.Models;

public class GetTokenPayment
{
    /// <summary>
    /// Unique Nexus generated code for identifying the payment.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// For Optional data
    /// </summary>
    public IDictionary<string, string> Data { get; set; }

    /// <summary>
    /// Unique hash of the transaction envelope.
    /// </summary>
    public string Hash { get; set; }

    /// <summary>
    /// The sender of the simple payment or payout
    /// </summary>
    public AccountInfo SenderAccount { get; set; }

    /// <summary>
    /// The receiver of the simple payment or funding
    /// </summary>
    public AccountInfo ReceiverAccount { get; set; }

    /// <summary>
    /// Amount of tokens this payment is in.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Token code of the payment
    /// </summary>
    public string TokenCode { get; set; }

    /// <summary>
    /// The date this payment was created.
    /// </summary>
    public string Created { get; set; }

    /// <summary>
    /// Status of the payment.
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// Type of the payment.
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Memo field of the transaction envelope for the payment
    /// </summary>
    public string Memo { get; set; }
}

public class AccountInfo
{
    /// <summary>
    /// Address of the account
    /// </summary>
    public string AccountAddress { get; set; }

    /// <summary>
    /// Unique Nexus identifier of the account
    /// </summary>
    public string AccountCode { get; set; }

    /// <summary>
    /// Unique Nexus identifier of the customer
    /// </summary>
    public string CustomerCode { get; set; }
}
