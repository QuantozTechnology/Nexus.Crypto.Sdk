namespace Nexus.Crypto.SDK.Models;

public class GetCustomersRequest
{
    public string? CustomerCode { get; set; }
    public string? Email { get; set; }
    public string? Status { get; set; }
    public string? TrustLevel { get; set; }
    public string? PortfolioCode { get; set; }
    public string? Tag { get; set; }
    public bool? IsBusiness { get; set; }
    public string? CountryCode { get; set; }
    public int? Page { get; set; }
    public int? Limit { get; set; }
    public string? SortBy { get; set; }
    public SortDirection? SortDirection { get; set; }
}

public class AddCommentRequest
{
    /// <summary>
    /// Text content of the comment.
    /// </summary>
    public required string Text { get; set; }
}

public class UpdateCommentRequest
{
    /// <summary>
    /// Updated text content of the comment.
    /// </summary>
    public required string Text { get; set; }
}

public class UpsertCustomerDataRequest
{
    /// <summary>
    /// Key/value pairs to store against a customer.
    /// </summary>
    public required IDictionary<string, string> Data { get; set; }
}

public class GetCustomerLimitRequest
{
    public required string CustomerCode { get; set; }
    public required string PaymentMethodCode { get; set; }
    public required string CryptoCode { get; set; }
}
