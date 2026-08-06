namespace Nexus.Crypto.SDK.Models;

public class GetCommentHistoryResponse
{
    /// <summary>
    /// The user who wrote the comment.
    /// </summary>
    public string? Author { get; set; }

    /// <summary>
    /// Description of the comment.
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// Timestamp the last comment was created.
    /// </summary>
    public string? Created { get; set; }
}

public class GetCommentsResponse
{
    /// <summary>
    /// Unique identifier of the comment.
    /// </summary>
    public string? Guid { get; set; }

    /// <summary>
    /// The user who wrote the comment.
    /// </summary>
    public string? Author { get; set; }

    /// <summary>
    /// Description of the comment.
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// Timestamp the comment was initially created.
    /// </summary>
    public string? Created { get; set; }

    /// <summary>
    /// Timestamp the comment was last updated.
    /// </summary>
    public string? Updated { get; set; }
}
