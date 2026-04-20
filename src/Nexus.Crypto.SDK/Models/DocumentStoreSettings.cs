using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Nexus.Crypto.SDK.Models;

public class DocumentStoreSettingsRequest
{
    /// <summary>
    /// Document Store Name
    /// </summary>
    [JsonPropertyName("name")]
    [Required]
    [StringLength(50)]
    public required string Name { get; set; }
    /// <summary>
    /// Document Store Description
    /// </summary>
    [JsonPropertyName("description")]
    [StringLength(100)]
    public string? Description { get; set; }

    /// <summary>
    /// Document Store Type
    /// </summary>
    /// <remarks>
    /// Currently only Azure Files is supported.
    /// </remarks>
    // This JSON property name is case-sensitive. Todo: fix this issue in Nexus 
    [JsonPropertyName("DocumentStoreType")]
    [Required]
    public required string DocumentStoreType { get; set; }

    /// <summary>
    /// The parameters required for the Document Store credentials.
    /// </summary>
    /// <example>
    /// Azure Files Parameters:
    /// - "account": "string",
    /// - "key": "string"
    /// - "shareName": "string"
    /// </example>
    [JsonPropertyName("parameters")]
    [Required]
    public required Dictionary<string, string> Parameters { get; set; }

    /// <summary>
    /// The maximum file size in MB that can be uploaded to the Document Store.
    /// </summary>
    [JsonPropertyName("maxFileSizeInMB")]
    public int MaxFileSizeInMB { get; set; }

    /// <summary>
    /// The maximum number of files that can be uploaded to the Document Store via the Nexus API.
    /// </summary>
    [JsonPropertyName("maxFileCount")]
    public int MaxFileCount { get; set; }
}

public class DocumentListRequest
{
    /// <summary>
    /// List Document Store files associated with the specified Customer Code.
    /// </summary>
    [JsonPropertyName("customerCode")]
    [StringLength(40)]
    [Required]
    public required string CustomerCode { get; set; }

    /// <summary>
    /// Get specific page of Document Store items.
    /// </summary>
    /// <remarks>
    /// Page defaults to 1 if not specified.
    /// </remarks>
    [JsonPropertyName("page")]
    [Required]
    public int Page { get; set; } = 1;

    /// <summary>
    /// Maximum number of Document Store items to return.
    /// </summary>
    /// <remarks>
    /// Limit defaults to 10 if not specified.
    /// </remarks>
    [JsonPropertyName("limit")]
    [Required]
    public int Limit { get; set; } = 10;

    /// <summary>
    /// Sort documents by this field.
    /// </summary>
    /// <remarks>
    /// SortBy defaults to CreatedDate if not specified.
    /// </remarks>
    [JsonPropertyName("sortBy")]
    [Required]
    public string SortBy { get; set; } = "CreatedDate";

    /// <summary>
    /// Sort documents in this direction.
    /// Possible values:
    /// - `Asc`
    /// - `Desc`
    /// </summary>
    /// <remarks>
    /// SortDirection defaults to Desc if not specified.
    /// </remarks>
    [JsonPropertyName("sortDirection")]
    [Required]
    public string SortDirection { get; set; } = "Desc";
}

public class DocumentRequest
{
    private string _filePath = string.Empty;

    /// <summary>
    /// The path of the file in the Document Store
    /// </summary>
    /// <example>
    /// path/to/file.txt
    /// </example>
    /// <remarks>
    /// The path is automatically URL-encoded to ensure it is safe for transmission
    /// </remarks>
    [JsonPropertyName("filePath")]
    [Required]
    [StringLength(255)]
    public required string FilePath
    {
        get => _filePath;
        set => _filePath = Uri.EscapeDataString(value);
    }
}


public record DocumentStoreSettingsResponse
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    [JsonPropertyName("documentStoreType")]
    public string DocumentStoreType { get; set; }
    [JsonPropertyName("shareName")]
    public string ShareName { get; set; }
    [JsonPropertyName("maxFileSizeInMB")]
    public int MaxFileSizeInMB { get; set; }
    [JsonPropertyName("maxFileCount")]
    public int MaxFileCount { get; set; }
}

