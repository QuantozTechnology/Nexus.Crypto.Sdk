#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace Nexus.Casp.Client.Models
{
    #pragma warning disable CS1591
    public partial class GetSearchResultListItemIdResponse : IParsable
    #pragma warning restore CS1591
    {
        /// <summary>The id property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Id { get; set; }
#nullable restore
#else
        public string Id { get; set; }
#endif
        /// <summary>The idNumber property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? IdNumber { get; set; }
#nullable restore
#else
        public string IdNumber { get; set; }
#endif
        /// <summary>The idType property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? IdType { get; set; }
#nullable restore
#else
        public string IdType { get; set; }
#endif
        /// <summary>The issueDate property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? IssueDate { get; set; }
#nullable restore
#else
        public string IssueDate { get; set; }
#endif
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::Nexus.Casp.Client.Models.GetSearchResultListItemIdResponse"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::Nexus.Casp.Client.Models.GetSearchResultListItemIdResponse CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            if(ReferenceEquals(parseNode, null)) throw new ArgumentNullException(nameof(parseNode));
            return new global::Nexus.Casp.Client.Models.GetSearchResultListItemIdResponse();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "id", n => { Id = n.GetStringValue(); } },
                { "idNumber", n => { IdNumber = n.GetStringValue(); } },
                { "idType", n => { IdType = n.GetStringValue(); } },
                { "issueDate", n => { IssueDate = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            if(ReferenceEquals(writer, null)) throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("id", Id);
            writer.WriteStringValue("idNumber", IdNumber);
            writer.WriteStringValue("idType", IdType);
            writer.WriteStringValue("issueDate", IssueDate);
        }
    }
}
#pragma warning restore CS0618
