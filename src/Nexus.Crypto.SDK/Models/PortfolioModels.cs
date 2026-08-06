namespace Nexus.Crypto.SDK.Models;

public class CreatePortfolioRequest
{
    /// <summary>
    /// Unique identifier for this Portfolio.
    /// </summary>
    public required string Code { get; set; }

    /// <summary>
    /// Optional humanized name. Defaults to Code when missing.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Flag to indicate if the mails feature is enabled for this portfolio. Defaults to true.
    /// </summary>
    public bool MailsEnabled { get; set; } = true;
}
