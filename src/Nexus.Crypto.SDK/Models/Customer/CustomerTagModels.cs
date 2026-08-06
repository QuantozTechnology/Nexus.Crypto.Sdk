namespace Nexus.Crypto.SDK.Models;

/// <summary>
/// Response for a customer tag.
/// </summary>
public class CustomerTagResponse
{
    public string? Tag { get; set; }
    public string? CustomerCode { get; set; }
    public string? Created { get; set; }
}

/// <summary>
/// Request to add a tag to a customer.
/// </summary>
public class CreateCustomerTagRequest
{
    public required string Tag { get; set; }
}

/// <summary>
/// Response for a customer's IBANs or send-out addresses.
/// </summary>
public class CustomerPayoutAddressResponse
{
    public Guid Id { get; set; }
    public string? CustomerCode { get; set; }
    public string? Address { get; set; }
    public string? CurrencyCode { get; set; }
    public string? Status { get; set; }
    public string? Created { get; set; }
    public string? CreatedBy { get; set; }
}

/// <summary>
/// Request filter for listing customer tags.
/// </summary>
public class GetCustomerTagsRequest
{
    public int? Page { get; set; }
    public int? Limit { get; set; }
}

/// <summary>
/// Request filter for listing customer compliance history.
/// </summary>
public class GetCustomerComplianceHistoryRequest
{
    public DateTime? CreatedFrom { get; set; }
    public DateTime? CreatedTill { get; set; }
    public int? Page { get; set; }
    public int? Limit { get; set; }
}

/// <summary>
/// Request filter for listing customer callbacks.
/// </summary>
public class GetCustomerCallbacksRequest
{
    public string? Status { get; set; }
    public int? Page { get; set; }
    public int? Limit { get; set; }
}
