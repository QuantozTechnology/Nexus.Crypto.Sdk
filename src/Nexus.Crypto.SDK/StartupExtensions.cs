using Microsoft.Extensions.DependencyInjection;

namespace Nexus.Crypto.SDK;

public static class StartupExtensions
{
    public static void AddNexusCryptoSdk(this IServiceCollection services, Action<NexusApiOptions> configureOptions)
    {
        services.Configure(configureOptions);
        services.AddHttpClient();
        services.AddScoped<INexusApiClientFactory, NexusApiClientFactory>();
        services.AddScoped<NexusAPIService>();
    }

    public static void AddNexusCryptoSdk(this IServiceCollection services)
    {
        services.AddNexusCryptoSdk(_ => { });
    }
}