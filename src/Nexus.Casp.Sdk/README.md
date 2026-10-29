# Nexus.Casp.Sdk

The Quantoz Nexus CASP API client for .NET, including dependency injection and
authentication support.

Install the package:

```sh
dotnet add package Nexus.Casp.Sdk
```

Register the client in your service collection:

```csharp
services.AddNexusCaspSdk();
```

The SDK uses bearer authentication by default. Provide an implementation of
`INexusApiGetAccessToken` to supply access tokens, and register it with your
application's dependency injection container. `NexusClient` can then be
injected into services that call the API.

To send an extra header with every subsequent request from the injected
client (scoped per DI scope, e.g. per HTTP request), use `AddHeader`:

```csharp
nexusClient.AddHeader("x-Correlation-Id", correlationId);
```

Calling `AddHeader` again with the same name replaces the value;
`RemoveHeader` removes it.

The package includes portable PDB symbols and Source Link information for
stepping into the SDK when debugging from a supported IDE.

## Calling endpoints that are not in the generated client

When you are working on an endpoint that is not (yet) part of the generated
client and want to try it out with Kiota, you can call it with a hand-written
request builder that reuses the adapter of an existing `NexusClient`:

```csharp
public class MyEndpointRequestBuilder(IRequestAdapter adapter)
    : BaseRequestBuilder(adapter, "{+baseurl}/my/new/endpoint", new Dictionary<string, object>
    {
        ["baseurl"] = adapter.BaseUrl!,
    })
{
    public Task PostAsync(Stream jsonBody, CancellationToken ct = default)
    {
        var info = new RequestInformation(Method.POST, UrlTemplate, PathParameters);
        info.SetStreamContent(jsonBody, "application/json");
        return RequestAdapter.SendNoContentAsync(info, cancellationToken: ct);
    }
}

var builder = new MyEndpointRequestBuilder(nexusClient.GetRequestAdapter());
```

Requests sent through `GetRequestAdapter()` share the base URL, authentication,
handlers and the headers added with `AddHeader`. Prefer it over
`NexusCaspClientFactory.CreateRequestAdapter()` when you need headers shared
with an existing client; `CreateRequestAdapter()` returns a new adapter on its own `HttpClient`.

## Publishing

The CASP release workflow publishes `Nexus.Casp.Sdk` to both GitHub Packages and
nuget.org. NuGet publishing uses [trusted publishing](https://learn.microsoft.com/en-us/nuget/nuget-org/trusted-publishing)
to exchange a GitHub OIDC token for a short-lived API key.

Before running a release:

1. Sign in to nuget.org and add a **Trusted Publishing** policy for the owner of
   `Nexus.Casp.Sdk`, configured as follows:
   - Push scope: **Push new packages and package versions**
   - Glob Patterns and Packages: `Nexus.Casp.Sdk`
   - Repository owner: `QuantozTechnology`
   - Repository: `Nexus.Crypto.Sdk`
   - Workflow file: `casp-release.yml` (file name only)
   - Environment: leave empty; this workflow does not use a GitHub environment.
2. Set the repository Actions secret `NUGET_USER` to the authorized nuget.org
   account's username (profile name, not an email address).

No long-lived `NUGET_API_KEY` secret is required for CASP releases. GitHub Packages
continues to use the workflow's `GITHUB_TOKEN`. Both package pushes skip duplicate
versions so a partially published release can be retried. Other SDK publishing
is unchanged.
