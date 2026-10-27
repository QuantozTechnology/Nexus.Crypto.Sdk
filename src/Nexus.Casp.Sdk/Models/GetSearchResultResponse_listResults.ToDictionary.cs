using System.Text.Json;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Serialization.Json;

namespace Nexus.Casp.Client.Models;

// Hand-written; lives outside Client/ so `kiota generate --clean-output` does not remove it.
public partial class GetSearchResultResponse_listResults
{
    /// <summary>
    /// Returns all list results keyed by list code (case-insensitive), combining the typed
    /// OFAC/EU/NL properties with any other keys captured in <see cref="AdditionalData"/>.
    /// Typed properties take precedence when a key matches in a different casing.
    /// </summary>
    public Dictionary<string, GetSearchResultListResponse> ToDictionary()
    {
        var result = new Dictionary<string, GetSearchResultListResponse>(StringComparer.OrdinalIgnoreCase);

        if (OFAC is not null) result[nameof(OFAC)] = OFAC;
        if (EU is not null) result[nameof(EU)] = EU;
        if (NL is not null) result[nameof(NL)] = NL;

        foreach (var (key, value) in AdditionalData)
        {
            if (result.ContainsKey(key)) continue;

            var converted = Convert(value);
            if (converted is not null) result[key] = converted;
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
