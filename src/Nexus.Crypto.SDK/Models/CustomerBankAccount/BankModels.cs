namespace Nexus.Crypto.SDK.Models;

public class BankModel
{
    /// <summary>
    /// Unique identifier of the bank.
    /// </summary>
    public Guid Id { get; set; }

    public string? Name { get; set; }
    public string? Country { get; set; }
    public string? BicCode { get; set; }
    public string? City { get; set; }
    public string? IbanCode { get; set; }
    public string? IssuerId { get; set; }
}

public class CreateBankRequest
{
    public required string Name { get; set; }
    public string? IbanCode { get; set; }
    public string? BicCode { get; set; }
    public required string CountryCode { get; set; }
    public string? City { get; set; }
    public string? IssuerId { get; set; }
}

public class UpdateBankRequest
{
    public string? Name { get; set; }
    public string? IbanCode { get; set; }
    public string? BicCode { get; set; }
    public string? CountryCode { get; set; }
    public string? City { get; set; }
    public string? IssuerId { get; set; }
}

public class CreateBankAccountRequest
{
    public required string Number { get; set; }
    public string? Name { get; set; }

    /// <summary>
    /// Currency code related to the bank account (ISO 4217).
    /// </summary>
    public required string CurrencyCode { get; set; }

    public RelatedBank? Bank { get; set; }

    /// <summary>
    /// Indicates the primary account of a customer.
    /// </summary>
    public bool IsPrimary { get; set; }
}


