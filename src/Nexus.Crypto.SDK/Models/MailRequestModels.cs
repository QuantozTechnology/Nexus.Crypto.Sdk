namespace Nexus.Crypto.SDK.Models;

public class PostMailRequest
{
    /// <summary>
    /// The type of this mail (e.g., NewAccountRequested, TransactionBuyFinish, etc.)
    /// </summary>
    public required string Type { get; set; }

    /// <summary>
    /// References to other entities. At least one must be provided.
    /// </summary>
    public required MailEntityCodes References { get; set; }

    public MailContent? Content { get; set; }
    public required PostMailRecipient Recipient { get; set; }
}

public class PostMailRecipient
{
    public required string Email { get; set; }
    public string? Cc { get; set; }
    public string? Bcc { get; set; }
}

public class PutMailRequest
{
    public MailContent? Content { get; set; }
    public MailRecipient? Recipient { get; set; }
}
