namespace Nexus.Crypto.SDK.Models;

public class CreateNotificationRequest
{
    public required CreateNotificationType Type { get; set; }
    public required CreateNotificationLevel Level { get; set; }
    public required string Title { get; set; }
    public required string Message { get; set; }
}

public enum CreateNotificationType
{
    Operational,
    Technical
}

public enum CreateNotificationLevel
{
    Info,
    Warning,
    Error
}
