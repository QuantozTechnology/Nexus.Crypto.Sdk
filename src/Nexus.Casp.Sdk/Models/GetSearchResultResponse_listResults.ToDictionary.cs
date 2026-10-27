using System.Text.Json;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Serialization.Json;

namespace Nexus.Casp.Client.Models;

// Hand-written; lives outside Client/ so Kiota regeneration does not overwrite it.
public partial class GetSearchResultResponse_listResults
{
    /// <summary>
    /// Returns all list results keyed by list code (case-insensitive), converted from
    /// <see cref="AdditionalData"/>. Kiota cannot generate typed dictionaries, so the models
    /// in this folder are hand-written.
    /// </summary>
    public Dictionary<string, GetSearchResultListResponse> ToDictionary()
    {
        var result = new Dictionary<string, GetSearchResultListResponse>(StringComparer.OrdinalIgnoreCase);

        foreach (var (key, value) in AdditionalData)
        {
            var converted = Convert(value);
            if (converted is not null) result.TryAdd(key, converted);
        }

        return result;
    }

    private static GetSearchResultListResponse? Convert(object? value)
    {
        switch (value)
        {
            case null:
            case UntypedNull:
                return null;
            case GetSearchResultListResponse typed:
                return typed;
            case UntypedNode node:
                using (var writer = new JsonSerializationWriter())
                {
                    writer.WriteObjectValue(null, node);
                    using var stream = writer.GetSerializedContent();
                    using var document = JsonDocument.Parse(stream);
                    return new JsonParseNode(document.RootElement)
                        .GetObjectValue(GetSearchResultListResponse.CreateFromDiscriminatorValue);
                }
            default:
                return null;
        }
    }
}
