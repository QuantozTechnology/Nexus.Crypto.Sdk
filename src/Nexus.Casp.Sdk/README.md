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

The package includes portable PDB symbols and Source Link information for
stepping into the SDK when debugging from a supported IDE.
