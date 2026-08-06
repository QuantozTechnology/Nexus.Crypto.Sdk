namespace Nexus.Crypto.SDK.Models;

public class GetBucketResponse
{
    /// <summary>
    /// Unique identifier of the bucket.
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// Human readable name of the bucket.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// The customer code that the bucket is connected to.
    /// </summary>
    public string? CustomerCode { get; set; }

    /// <summary>
    /// Timestamp the bucket was created.
    /// </summary>
    public string? Created { get; set; }

    /// <summary>
    /// Timestamp the bucket was updated.
    /// </summary>
    public string? Updated { get; set; }
}

public class CreateBucketRequest
{
    /// <summary>
    /// Unique identifier of the bucket.
    /// </summary>
    public required string Code { get; set; }

    /// <summary>
    /// Human readable name of the bucket.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// The customer code that the bucket will be connected to.
    /// Leave empty to make available to all customers.
    /// </summary>
    public string? CustomerCode { get; set; }
}

public class UpdateBucketRequest
{
    /// <summary>
    /// Human readable name of the bucket.
    /// </summary>
    public required string Name { get; set; }
}
