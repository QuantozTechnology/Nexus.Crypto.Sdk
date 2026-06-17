using System.ComponentModel.DataAnnotations;

namespace Nexus.Crypto.SDK.Models;

public class PersonResponse
{
    /// <summary>
    /// Unique identifier of the person.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Unique identifier of the customer linked to this person.
    /// </summary>
    public string CustomerCode { get; set; }

    /// <summary>
    /// First name of the person.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Last name of the person.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// This person is linked to a customer in one or more ways.
    /// </summary>
    public List<CustomerPersonType> Types { get; set; } = [];

    /// <summary>
    /// Date of birth of this person.
    /// </summary>
    public string? DateOfBirth { get; set; }

    /// <summary>
    /// Country of this person's nationality (ISO 4217).
    /// </summary>
    public PersonCountry? Nationality { get; set; }

    /// <summary>
    /// Country of this person's residence (ISO 4217).
    /// </summary>
    public PersonCountry? ResidenceCountry { get; set; }

    /// <summary>
    /// Person's ownership percentage of the business
    /// </summary>
    /// <example>
    /// 0.39 represents 39%
    /// </example>
    public decimal? OwnershipPercentage { get; set; }

    /// <summary>
    /// Date the person was created.
    /// </summary>
    public string CreatedOn { get; set; }

    /// <summary>
    /// User that created the person.
    /// </summary>
    public string CreatedBy { get; set; }

    /// <summary>
    /// Date the person was last updated.
    /// </summary>
    public string? UpdatedOn { get; set; }

    /// <summary>
    /// User that updated the person last.
    /// </summary>
    public string? UpdatedBy { get; set; }
}

public class CustomerPersonRequestBase
{
    /// <summary>
    /// First name of the person.
    /// </summary>
    [StringLength(100)]
    public string? FirstName { get; set; }

    /// <summary>
    /// Last name of the person.
    /// </summary>
    [StringLength(100)]
    public string? LastName { get; set; }

    /// <summary>
    /// Person's date of birth (ISO 8601). Date format needs to be: yyyy-MM-dd.
    /// </summary>
    [RegularExpression(@"^\d{4}-\d{2}-\d{2}$",
        ErrorMessage = "The value must be in the exact format: yyyy-MM-dd")]
    public string? DateOfBirth { get; set; }

    /// <summary>
    /// Country code of this person's nationality (ISO 4217).
    /// </summary>
    /// <example>NL</example>
    [StringLength(3, MinimumLength = 2)]
    public string? NationalityCountryCode { get; set; }

    /// <summary>
    /// Country code of this person's residence country (ISO 4217).
    /// </summary>
    /// <example>NL</example>
    [StringLength(3, MinimumLength = 2)]
    public string? ResidenceCountryCode { get; set; }

    /// <summary>
    /// Person's ownership percentage of the business and should be expressed as a decimal fraction.
    /// </summary>
    /// <example>
    /// 0.39 represents 39%
    /// </example>
    [Range(0.01, 1, ErrorMessage = "If value is specified, it must be between 0.01 and 1.")]
    public decimal? OwnershipPercentage { get; set; }
}

public class CreateCustomerPersonRequest : CustomerPersonRequestBase
{
    /// <summary>
    /// This person is linked to a customer in the below way:
    /// </summary>
    [Required(ErrorMessage = "The Types field is required.")]
    [MinLength(1, ErrorMessage = "At least one type must be provided.")]
    public List<CustomerPersonType> Types { get; set; } = [];
}

public class UpdateCustomerPersonRequest : CustomerPersonRequestBase
{
    /// <summary>
    /// Update the way a person is linked to a customer:
    /// </summary>
    [MinLength(1)]
    public List<CustomerPersonType>? Types { get; set; }


    /// <summary>
    /// Array of field names to explicitly clear (set to null/empty).
    /// Supported fields: FirstName, LastName, DateOfBirth, NationalityCountryCode, ResidenceCountryCode, OwnershipPercentage,
    /// and type flags via Type.Director, Type.UltimateBeneficialOwner, Type.PseudoUltimateBeneficialOwner, Type.AuthorisedRepresentative.
    /// </summary>
    /// <example>
    /// ["FirstName", "Type.UltimateBeneficialOwner", "OwnershipPercentage"]
    /// </example>
    public string[]? FieldsToClear { get; set; }
}


public enum CustomerPersonType
{
    Director,
    UltimateBeneficialOwner,
    PseudoUltimateBeneficialOwner,
    AuthorisedRepresentative,
    Other,
}

public class PersonCountry
{
    /// <summary>
    /// Code of this country (ISO 4217).
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Name of this country.
    /// </summary>
    public string Name { get; set; }
}