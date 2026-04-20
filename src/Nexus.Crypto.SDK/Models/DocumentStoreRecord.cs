using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text.Json.Serialization;

namespace Nexus.Crypto.SDK.Models;

public class DocumentStoreRecordResponse
{
    [JsonPropertyName("fileName")]
    public string FileName { get; set; }

    [JsonPropertyName("filePath")]
    public string FilePath { get; set; }
    
    [JsonPropertyName("documentStoreType")]
    public DocumentStoreTypeInfo DocumentStoreType { get; set; }

    [JsonPropertyName("alias")]
    public string? Alias { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("customerCode")]
    public string CustomerCode { get; set; }

    [JsonPropertyName("itemReference")]
    public string? ItemReference { get; set; }

    [JsonPropertyName("state")]
    public string? State { get; set; }

    [JsonPropertyName("fileSizeInMB")]
    public string? FileSizeInMB { get; set; }

    [JsonPropertyName("createdOn")]
    public string CreatedOn { get; set; }

    [JsonPropertyName("updatedOn")]
    public string? UpdatedOn { get; set; }
}

public class FileUpdateRequest
{
    /// <summary>
    /// The path of the file in the Document Store
    /// </summary>
    /// <example>
    /// path/to/file.txt
    /// </example>
    [JsonPropertyName("filePath")]
    [Required]
    [StringLength(255)]
    public required string FilePath { get; set; }
    
    /// <summary>
    /// The DocumentStoreTypeCode is used to identify the type of document associated with the file.
    /// </summary>
    [JsonPropertyName("documentStoreTypeCode")]
    [StringLength(40)]
    public string DocumentStoreTypeCode { get; set; }

    /// <summary>
    /// An optional alias for the file. 
    /// </summary>

    [StringLength(50)]
    public string Alias { get; set; }

    /// <summary>
    /// An optional description of the file.
    /// </summary>
    [JsonPropertyName("description")]
    [StringLength(100)]
    public string Description { get; set; }

    /// <summary>
    /// A Customer code is used to identify the customer associated with the file.
    /// </summary>
    [JsonPropertyName("customerCode")]
    [StringLength(40)]
    public string CustomerCode { get; set; }

    /// <summary>
    /// An optional external unique identifier for the file.
    /// </summary>
    [JsonPropertyName("itemReference")]
    [StringLength(40)]
    public string ItemReference { get; set; }
}

public class FileUploadRequest
{
    /// <summary>
    /// The path of the file in the Document Store
    /// </summary>
    /// <example>
    /// path/to/file.txt
    /// </example>
    [JsonPropertyName("filePath")]
    [Required]
    [StringLength(255)]
    public required string FilePath { get; set; }

    /// <summary>
    /// The DocumentStoreTypeCode is used to identify the type of document associated with the file.
    /// </summary>
    [JsonPropertyName("documentStoreTypeCode")]
    [Required]
    [StringLength(40)]
    public required string DocumentStoreTypeCode { get; set; }

    /// <summary>
    /// An optional alias for the file. 
    /// </summary>
    [JsonPropertyName("alias")]
    [StringLength(50)]
    public string? Alias { get; set; }

    /// <summary>
    /// An optional description of the file.
    /// </summary>
    [JsonPropertyName("description")]
    [StringLength(100)]
    public string? Description { get; set; }

    /// <summary>
    /// A Customer code is used to identify the customer associated with the file.
    /// </summary>
    [JsonPropertyName("customerCode")]
    [Required]
    [StringLength(40)]
    public required string CustomerCode { get; set; }

    /// <summary>
    /// An optional external unique identifier for the file.
    /// </summary>
    [JsonPropertyName("itemReference")]
    [StringLength(40)]
    public string? ItemReference { get; set; }

    /// <summary>
    /// The content stream for the file to be added to the Document Store.
    /// </summary>
    [JsonPropertyName("fileContent")]
    [Required]
    public required Stream FileContent { get; set; }

    /// <summary>
    /// The file name to use for the uploaded file.
    /// </summary>
    [JsonPropertyName("fileName")]
    [Required]
    [StringLength(255)]
    public required string FileName { get; set; }
}

public class DocumentStoreTypeInfo
{
    /// <summary>
    /// The document type code
    /// </summary>
    [JsonPropertyName("Code")]
    public string Code { get; set; }

    /// <summary>
    /// The document type name
    /// </summary>
    [JsonPropertyName("Name")]
    public string Name { get; set; }

    /// <summary>
    /// The document type description if the document type is "Other"
    /// </summary>
    [JsonPropertyName("OtherDescription")]
    public string? OtherDescription { get; set; }
}