#!/usr/bin/env bash
# Regenerates src/Nexus.Casp.Sdk/Client from the patched OpenAPI description.
# The original spec is downloaded to a temp dir and never committed.
set -euo pipefail

root="$(cd "$(dirname "$0")/.." && pwd)"
sdk="$root/src/Nexus.Casp.Sdk"
lock="$sdk/Client/kiota-lock.json"
tmp="$(mktemp -d)"
trap 'rm -rf "$tmp"' EXIT

url="$(jq -r .descriptionLocation "$lock")"
curl -sSf "$url" -o "$tmp/openapi.json"
"$root/scripts/patch-casp-openapi.sh" "$tmp/openapi.json" "$tmp/openapi.patched.json"

cd "$sdk"
kiota generate -d "$tmp/openapi.patched.json" -o ./Client -l CSharp \
  -c "$(jq -r .clientClassName "$lock")" -n "$(jq -r .clientNamespaceName "$lock")" \
  --exclude-backward-compatible --clean-output --log-level warning

# generate records the temp file; restore the real description location
jq --arg url "$url" '.descriptionLocation = $url' Client/kiota-lock.json > "$tmp/lock.json"
cp "$tmp/lock.json" Client/kiota-lock.json
