namespace Nexus.Crypto.SDK.Models;

public class GetCustomerBankAccounts
{
    /// <summary>
    /// Unique identifier of the bank account.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Number of the bank account.
    /// </summary>
    public string Number { get; set; }

    /// <summary>
    /// Name of the bank account holder.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Unique identifier for this customer.
    /// </summary>
    public string CustomerCode { get; set; }

    /// <summary>
    /// Currency code related to the bank account.
    /// </summary>
    public string CurrencyCode { get; set; }

    /// <summary>
    /// Unique identifier for this bank account.
    /// </summary>
    public BankResponse Bank { get; set; }

    /// <summary>
    /// Indicates the primary account of a customer.
    /// </summary>
    public bool IsPrimary { get; set; }

    /// <summary>
    /// Date the bank account was created.
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// User that created the bank account.
    /// </summary>
    public string CreatedBy { get; set; }

    /// <summary>
    /// Date the bank account was last updated.
    /// </summary>
    public DateTime Updated { get; set; }

    /// <summary>
    /// User that updated the bank account last.
    /// </summary>
    public string UpdatedBy { get; set; }
}

public class BankResponse
{
    /// <summary>
    /// Unique identifier of the bank.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Bank's name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Bank's country code.
    /// </summary>
    public string Country { get; set; }

    /// <summary>
    /// Bank's BIC.
    /// </summary>
    public string BicCode { get; set; }

    /// <summary>
    /// Bank's city.
    /// </summary>
    public string City { get; set; }
}