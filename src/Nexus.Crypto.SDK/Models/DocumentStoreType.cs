using System.ComponentModel.DataAnnotations;

namespace Nexus.Crypto.SDK.Models;


public record DocumentStoreTypeResponse
{
    /// <summary>
    /// The name of the document type.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// A unique code that identifies the document type.
    /// </summary>
    public string Code { get; set; }
    
    /// <summary>
    /// A description of the document type.
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// The timestamp indicating when the document type was created.
    /// </summary>
    public DateTime CreatedOn { get; set; }
    
    /// <summary>
    /// The timestamp indicating when the document type was last modified.
    /// </summary>
    public DateTime? UpdatedOn { get; set; }
}

public record DocumentStoreTypeCreateRequest
{
    /// <summary>
    /// A unique code that identifies the document type.
    /// </summary>
    [Required]
    [StringLength(40)]
    public string Code {get; set;}
    
    /// <summary>
    /// The name of the document type.
    /// </summary>
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    
    /// <summary>
    /// A description of the document type.
    /// </summary>
    [StringLength(255)]
    public string? Description {get; set;}
}


public class DocumentStoreTypeUpdateRequest
{
    /// <summary>
    /// The name of the document type.
    /// </summary>
    [StringLength(100)]
    public string? Name { get; set; }
    
    /// <summary>
    /// A description of the document type.
    /// </summary>
    [StringLength(255)]
    public string? Description {get; set;}
}
