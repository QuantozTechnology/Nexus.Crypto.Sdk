namespace Nexus.Crypto.SDK.Models;

/// <summary>
/// Response for a notification (portal alert).
/// </summary>
public class GetNotificationResponse
{
    public Guid Id { get; set; }

    /// <summary>
    /// Notification type: Operational | Technical
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Severity: Info | Warning | Error
    /// </summary>
    public string? Level { get; set; }

    public string? Title { get; set; }
    public string? Message { get; set; }
    public string? Created { get; set; }
    public string? CreatedBy { get; set; }
    public string? Dismissed { get; set; }
}

/// <summary>
/// Request filter for listing notifications.
/// </summary>
public class GetNotificationsRequest
{
    public string? Type { get; set; }
    public string? Level { get; set; }
    public bool? IsDismissed { get; set; }
    public int? Page { get; set; }
    public int? Limit { get; set; }
}
