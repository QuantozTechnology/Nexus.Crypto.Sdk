#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace Nexus.Casp.Client.Models
{
    #pragma warning disable CS1591
    public partial class GetSearchResultListItemResponse : IParsable
    #pragma warning restore CS1591
    {
        /// <summary>The aliases property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::Nexus.Casp.Client.Models.GetSearchResultListItemAkaResponse>? Aliases { get; set; }
#nullable restore
#else
        public List<global::Nexus.Casp.Client.Models.GetSearchResultListItemAkaResponse> Aliases { get; set; }
#endif
        /// <summary>The dateOfBirthList property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::Nexus.Casp.Client.Models.GetSearchResultListItemDateOfBirthResponse>? DateOfBirthList { get; set; }
#nullable restore
#else
        public List<global::Nexus.Casp.Client.Models.GetSearchResultListItemDateOfBirthResponse> DateOfBirthList { get; set; }
#endif
        /// <summary>The entityType property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? EntityType { get; set; }
#nullable restore
#else
        public string EntityType { get; set; }
#endif
        /// <summary>The firstName property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? FirstName { get; set; }
#nullable restore
#else
        public string FirstName { get; set; }
#endif
        /// <summary>The id property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Id { get; set; }
#nullable restore
#else
        public string Id { get; set; }
#endif
        /// <summary>The idList property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::Nexus.Casp.Client.Models.GetSearchResultListItemIdResponse>? IdList { get; set; }
#nullable restore
#else
        public List<global::Nexus.Casp.Client.Models.GetSearchResultListItemIdResponse> IdList { get; set; }
#endif
        /// <summary>The lastName property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? LastName { get; set; }
#nullable restore
#else
        public string LastName { get; set; }
#endif
        /// <summary>The maxSimilarityScore property</summary>
        public decimal? MaxSimilarityScore { get; set; }
        /// <summary>The middleName property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? MiddleName { get; set; }
#nullable restore
#else
        public string MiddleName { get; set; }
#endif
        /// <summary>The nationalityList property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::Nexus.Casp.Client.Models.GetSearchResultListItemNationalityResponse>? NationalityList { get; set; }
#nullable restore
#else
        public List<global::Nexus.Casp.Client.Models.GetSearchResultListItemNationalityResponse> NationalityList { get; set; }
#endif
        /// <summary>The placeOfBirthList property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::Nexus.Casp.Client.Models.GetSearchResultListItemPlaceOfBirthResponse>? PlaceOfBirthList { get; set; }
#nullable restore
#else
        public List<global::Nexus.Casp.Client.Models.GetSearchResultListItemPlaceOfBirthResponse> PlaceOfBirthList { get; set; }
#endif
        /// <summary>The searchScore property</summary>
        public double? SearchScore { get; set; }
        /// <summary>The wholeName property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? WholeName { get; set; }
#nullable restore
#else
        public string WholeName { get; set; }
#endif
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::Nexus.Casp.Client.Models.GetSearchResultListItemResponse"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::Nexus.Casp.Client.Models.GetSearchResultListItemResponse CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            if(ReferenceEquals(parseNode, null)) throw new ArgumentNullException(nameof(parseNode));
            return new global::Nexus.Casp.Client.Models.GetSearchResultListItemResponse();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "aliases", n => { Aliases = n.GetCollectionOfObjectValues<global::Nexus.Casp.Client.Models.GetSearchResultListItemAkaResponse>(global::Nexus.Casp.Client.Models.GetSearchResultListItemAkaResponse.CreateFromDiscriminatorValue)?.AsList(); } },
                { "dateOfBirthList", n => { DateOfBirthList = n.GetCollectionOfObjectValues<global::Nexus.Casp.Client.Models.GetSearchResultListItemDateOfBirthResponse>(global::Nexus.Casp.Client.Models.GetSearchResultListItemDateOfBirthResponse.CreateFromDiscriminatorValue)?.AsList(); } },
                { "entityType", n => { EntityType = n.GetStringValue(); } },
                { "firstName", n => { FirstName = n.GetStringValue(); } },
                { "id", n => { Id = n.GetStringValue(); } },
                { "idList", n => { IdList = n.GetCollectionOfObjectValues<global::Nexus.Casp.Client.Models.GetSearchResultListItemIdResponse>(global::Nexus.Casp.Client.Models.GetSearchResultListItemIdResponse.CreateFromDiscriminatorValue)?.AsList(); } },
                { "lastName", n => { LastName = n.GetStringValue(); } },
                { "maxSimilarityScore", n => { MaxSimilarityScore = n.GetDecimalValue(); } },
                { "middleName", n => { MiddleName = n.GetStringValue(); } },
                { "nationalityList", n => { NationalityList = n.GetCollectionOfObjectValues<global::Nexus.Casp.Client.Models.GetSearchResultListItemNationalityResponse>(global::Nexus.Casp.Client.Models.GetSearchResultListItemNationalityResponse.CreateFromDiscriminatorValue)?.AsList(); } },
                { "placeOfBirthList", n => { PlaceOfBirthList = n.GetCollectionOfObjectValues<global::Nexus.Casp.Client.Models.GetSearchResultListItemPlaceOfBirthResponse>(global::Nexus.Casp.Client.Models.GetSearchResultListItemPlaceOfBirthResponse.CreateFromDiscriminatorValue)?.AsList(); } },
                { "searchScore", n => { SearchScore = n.GetDoubleValue(); } },
                { "wholeName", n => { WholeName = n.GetStringValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            if(ReferenceEquals(writer, null)) throw new ArgumentNullException(nameof(writer));
            writer.WriteCollectionOfObjectValues<global::Nexus.Casp.Client.Models.GetSearchResultListItemAkaResponse>("aliases", Aliases);
            writer.WriteCollectionOfObjectValues<global::Nexus.Casp.Client.Models.GetSearchResultListItemDateOfBirthResponse>("dateOfBirthList", DateOfBirthList);
            writer.WriteStringValue("entityType", EntityType);
            writer.WriteStringValue("firstName", FirstName);
            writer.WriteStringValue("id", Id);
            writer.WriteCollectionOfObjectValues<global::Nexus.Casp.Client.Models.GetSearchResultListItemIdResponse>("idList", IdList);
            writer.WriteStringValue("lastName", LastName);
            writer.WriteDecimalValue("maxSimilarityScore", MaxSimilarityScore);
            writer.WriteStringValue("middleName", MiddleName);
            writer.WriteCollectionOfObjectValues<global::Nexus.Casp.Client.Models.GetSearchResultListItemNationalityResponse>("nationalityList", NationalityList);
            writer.WriteCollectionOfObjectValues<global::Nexus.Casp.Client.Models.GetSearchResultListItemPlaceOfBirthResponse>("placeOfBirthList", PlaceOfBirthList);
            writer.WriteDoubleValue("searchScore", SearchScore);
            writer.WriteStringValue("wholeName", WholeName);
        }
    }
}
#pragma warning restore CS0618
