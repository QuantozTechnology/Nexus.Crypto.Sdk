using System.ComponentModel.DataAnnotations;

namespace Nexus.Crypto.SDK.Models;

public class CustomerBankAccountResponse
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

public class UpdateBankAccountRequest
{
    /// <summary>
    /// Bank account number of the bank account you want to update.
    /// </summary>
    [Required, StringLength(40)]
    public string Number { get; set; }

    /// <summary>
    /// Unique identifier of the customer of the bank account you want to update.
    /// </summary>
    [Required, StringLength(40)]
    public string CustomerCode { get; set; }

    /// <summary>
    /// Update the name of the bank account holder.
    /// </summary>
    [StringLength(80)]
    public string? Name { get; set; }

    /// <summary>
    /// Update the currency code related to the bank account. (ISO 4217)
    /// </summary>
    /// <example>
    /// EUR
    /// </example>
    [StringLength(3, MinimumLength = 3)]
    public string? CurrencyCode { get; set; }

    /// <summary>
    /// Update the bank account's optional related bank. If the bank does not exist yet, it will be created.
    /// </summary>
    public UpdateBankAccountBankRequest? Bank { get; set; }

    /// <summary>
    /// Update the primary account indicator of a customer. Setting this true, will set the other bank accounts of a customer to false.
    /// </summary>
    public bool? IsPrimary { get; set; }
}

public class UpdateBankAccountBankRequest
{
    /// <summary>
    /// Bank bic code of the bank account's bank.
    /// </summary>
    public string BicCode { get; set; }
}

public class CreateBankAccountRequestModel
{
    /// <summary>
    /// Bank account number of the bank account.
    /// </summary>
    [Required, StringLength(40)]
    public string Number { get; set; }

    /// <summary>
    /// Name of the bank account holder.
    /// </summary>
    [StringLength(80)]
    public string Name { get; set; }

    /// <summary>
    /// Currency code related to the bank account (ISO 4217).
    /// </summary>
    /// <example>
    /// EUR
    /// </example>
    [Required, StringLength(3, MinimumLength = 3)]
    public string CurrencyCode { get; set; }

    /// <summary>
    /// Bank account's optional related bank. If the bank does not exist yet, it will be created.
    /// </summary>
    public RelatedBank Bank { get; set; }

    /// <summary>
    /// Indicates the primary account of a customer. Setting this true, will set the other bank accounts of a customer to false.
    /// </summary>
    [Required]
    public bool IsPrimary { get; set; }
}

public class RelatedBank
{
    /// <summary>
    /// Bank bic code of the bank account's bank.
    /// </summary>
    public string BicCode { get; set; }

    /// <summary>
    /// Bank name of the bank account's bank.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Bank city of the bank account's bank.
    /// </summary>
    public string City { get; set; }

    /// <summary>
    /// Bank country code of the bank account's bank (ISO 4217).
    /// </summary>
    /// <example>NL</example>
    [StringLength(2, MinimumLength = 2)]
    public string CountryCode { get; set; }
}