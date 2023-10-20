using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Sparks.SupabaseClient;

public static class DependencyInjection
{
    public static void AddSupaBaseClient(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddOptions();
        serviceCollection.Configure<SupabaseClientOptions>(configuration.GetSection(SupabaseClientOptions.Name));
        serviceCollection.AddSingleton(provider => SupabaseClient.GetInstanceAsync(provider).GetAwaiter().GetResult());
    }
}