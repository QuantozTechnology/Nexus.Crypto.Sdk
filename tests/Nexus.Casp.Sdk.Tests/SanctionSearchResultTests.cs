using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Serialization.Json;
using Nexus.Casp.Client.Models;
using Xunit;

namespace Nexus.Casp.Sdk.Tests;

public class SanctionSearchResultTests
{
    private static string Json(string ofacKey, string euKey) => $$"""
        {
          "listResults": {
            "{{ofacKey}}": {
              "created": "2026-01-01T00:00:00Z",
              "version": "v1",
              "searchResults": [
                {
                  "id": "1",
                  "wholeName": "John Doe",
                  "searchScore": 0.9,
                  "aliases": [ { "aliasId": "a1", "wholeName": "Johnny" } ]
                }
              ]
            },
            "{{euKey}}": { "version": "v2", "searchResults": [] }
          }
        }
        """;

    private static GetSearchResultResponse Parse(string json)
    {
        using var document = System.Text.Json.JsonDocument.Parse(json);
        return new JsonParseNode(document.RootElement.Clone())
            .GetObjectValue(GetSearchResultResponse.CreateFromDiscriminatorValue)!;
    }

    [Fact]
    public void UppercaseKeys_AreReturnedByToDictionary()
    {
        var dict = Parse(Json("OFAC", "EU")).ListResults!.ToDictionary();

        var ofac = dict["OFAC"];
        Assert.Equal("v1", ofac.Version);
        var item = Assert.Single(ofac.SearchResults!);
        Assert.Equal("John Doe", item.WholeName);
        Assert.Equal("Johnny", Assert.Single(item.Aliases!).WholeName);
        Assert.Equal("v2", dict["EU"].Version);
        Assert.False(dict.ContainsKey("NL"));
    }

    [Fact]
    public void LowercaseKeys_AreReturnedByToDictionary()
    {
        var response = Parse(Json("ofac", "eu"));

        var dict = response.ListResults!.ToDictionary();

        Assert.Equal(2, dict.Count);
        var ofac = dict["OFAC"];
        Assert.Equal("v1", ofac.Version);
        Assert.Equal("John Doe", Assert.Single(ofac.SearchResults!).WholeName);
        Assert.Equal("Johnny", Assert.Single(ofac.SearchResults![0].Aliases!).WholeName);
        Assert.Equal("v2", dict["eu"].Version);
    }

    [Fact]
    public void MixedCasing_ReturnsAllEntries()
    {
        var response = Parse(Json("OFAC", "eu"));
        var dict = response.ListResults!.ToDictionary();

        Assert.Equal(2, dict.Count);
        Assert.Equal("v1", dict["ofac"].Version);
        Assert.Equal("v2", dict["EU"].Version);
    }

    [Fact]
    public void EmptyListResults_ReturnsEmptyDictionary()
    {
        var response = Parse("""{ "listResults": {} }""");

        Assert.Empty(response.ListResults!.ToDictionary());
    }

    [Fact]
    public void Sdk_ContainsTypedSearchModels()
    {
        var assembly = typeof(GetSearchResultResponse).Assembly;

        Assert.NotNull(assembly.GetType("Nexus.Casp.Client.Models.GetSearchResultListResponse"));
        Assert.NotNull(assembly.GetType("Nexus.Casp.Client.Models.GetSearchResultListItemResponse"));
    }
}
