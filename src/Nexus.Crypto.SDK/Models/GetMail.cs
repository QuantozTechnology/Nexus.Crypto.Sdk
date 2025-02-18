using System;
using System.Collections.Generic;
using System.Text;

namespace Nexus.Crypto.SDK.Models;

public class GetMail
{
    /// <summary>
    /// The code of this mail
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// The time this mail was created
    /// </summary>
    public string Created { get; set; }

    /// <summary>
    /// The time this mail was sent
    /// </summary>
    public string Sent { get; set; }

    /// <summary>
    /// The current status of the mail
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// The type of this mail
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// The references to other entities (customer, account, transaction) for this mail
    /// </summary>
    public MailEntityCodes References { get; set; }

    /// <summary>
    /// The contents of the mail
    /// </summary>
    public MailContent Content { get; set; }

    /// <summary>
    /// The information about the recipient(s) of the mail
    /// </summary>
    public MailRecipient Recipient { get; set; }
}

public class MailEntityCodes
{
    /// <summary>
    /// The code of the account that this mail is a reference to
    /// </summary>
    public string AccountCode { get; set; }

    /// <summary>
    /// The code of the customer that this mail is a reference to
    /// </summary>
    public string CustomerCode { get; set; }

    /// <summary>
    /// The code of the transaction that this mail is a reference to
    /// </summary>
    public string TransactionCode { get; set; }
}

public class MailContent
{
    /// <summary>
    /// The subject of this mail
    /// </summary>
    public string Subject { get; set; }

    /// <summary>
    /// The content of this mail as html
    /// </summary>
    public string Html { get; set; }

    /// <summary>
    /// The content of this mail as plaintext
    /// </summary>
    public string Text { get; set; }
}

public class MailRecipient
{
    /// <summary>
    /// The email address this mail is sent to
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// The CC email address this mail is sent to
    /// </summary>
    /// <example>
    /// user1@example.com,user2@example.com
    /// </example>
    public string CC { get; set; }

    /// <summary>
    /// The BCC email address this mail is sent to
    /// </summary>
    /// <example>
    /// user1@example.com,user2@example.com
    /// </example>
    public string BCC { get; set; }
}
