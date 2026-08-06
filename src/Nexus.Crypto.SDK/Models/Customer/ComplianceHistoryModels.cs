namespace Nexus.Crypto.SDK.Models;

public class GetCustomerComplianceHistoryResponse
{
    public string? CustomerCode { get; set; }

    /// <summary>
    /// The date and time the customer's compliance history record was created.
    /// </summary>
    public string? Created { get; set; }

    /// <summary>
    /// Status of customer: New | Active | Deleted | UnderReview | Blocked
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// Risk level of this customer.
    /// </summary>
    public string? Trustlevel { get; set; }

    /// <summary>
    /// Currency this Customer is allowed to transact in (ISO 4217).
    /// </summary>
    public string? CurrencyCode { get; set; }

    public bool IsBusiness { get; set; }

    /// <summary>
    /// Risk of a customer: None | Low | Medium | High | VeryHigh
    /// </summary>
    public string? RiskQualification { get; set; }

    /// <summary>
    /// Last date the customer was reviewed (ISO 8601).
    /// </summary>
    public string? LatestReview { get; set; }

    public bool IsReviewRecommended { get; set; }

    /// <summary>
    /// The optional reason why the compliance history record was created.
    /// </summary>
    public string? Reason { get; set; }

    /// <summary>
    /// By whom was the compliance history record created.
    /// </summary>
    public string? InitiatedBy { get; set; }
}
