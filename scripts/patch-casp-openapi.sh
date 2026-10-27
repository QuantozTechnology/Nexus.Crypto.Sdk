#!/usr/bin/env bash
# Patches GetSearchResultResponse.listResults so Kiota generates typed models
# (Kiota ignores additionalProperties schemas, microsoft/kiota#62).
# Usage: patch-casp-openapi.sh <input.json> <output.json>
# Fails if the schema/property is missing or no longer has the expected shape.
set -euo pipefail

in="$1"
out="$2"
ref='#/components/schemas/GetSearchResultListResponse'

jq -e --arg ref "$ref" '
  .components.schemas.GetSearchResultResponse.properties.listResults as $p
  | ($p != null)
    and (($p.type | if type == "array" then index("object") != null else . == "object" end))
    and ($p.additionalProperties["$ref"] == $ref)
    and (.components.schemas.GetSearchResultListResponse != null)
    and (.components.schemas.GetSearchResultListItemResponse != null)
' "$in" > /dev/null || {
  echo "ERROR: components.schemas.GetSearchResultResponse.properties.listResults does not have the expected shape; update scripts/patch-casp-openapi.sh" >&2
  exit 1
}

jq --arg ref "$ref" '
  .components.schemas.GetSearchResultResponse.properties.listResults |=
    (.properties = {OFAC: {"$ref": $ref}, EU: {"$ref": $ref}, NL: {"$ref": $ref}})
' "$in" > "$out"

jq -e --arg ref "$ref" '
  .components.schemas.GetSearchResultResponse.properties.listResults
  | (.properties | keys == ["EU","NL","OFAC"]) and (.additionalProperties["$ref"] == $ref)
' "$out" > /dev/null || { echo "ERROR: patch verification failed" >&2; exit 1; }
