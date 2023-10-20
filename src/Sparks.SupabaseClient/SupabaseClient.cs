using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Supabase;
using Supabase.Gotrue;
using Client = Supabase.Client;

namespace Sparks.SupabaseClient;

public class SupabaseClient
{
    private readonly Client _client;
    private readonly ILogger<SupabaseClient> _logger;
    private static SupabaseClient? _instance;
    
    public static async Task<SupabaseClient> GetInstanceAsync(IServiceProvider serviceProvider)
    {
        // Check if the instance is already created
        if (_instance != null) return _instance;
        
        // Create a new instance and assign it to the static field
        var options = serviceProvider.GetRequiredService<IOptions<SupabaseClientOptions>>();
        var logger = serviceProvider.GetRequiredService<ILogger<SupabaseClient>>();
        _instance = new SupabaseClient(options, logger);

        // Do some asynchronous initialization here if needed
        await _instance.Init();

        // Return the singleton instance
        return _instance;
    }
    
    private SupabaseClient(IOptions<SupabaseClientOptions> clientOptions, ILogger<SupabaseClient> logger)
    {
        _logger = logger;
        var projectUrl = clientOptions.Value.ProjectUrl;
        var projectKey = clientOptions.Value.PublicKey;
        var options = new SupabaseOptions
        {
            AutoConnectRealtime = true
        };
        _client = new Client(projectUrl, projectKey, options);
    }

    public async Task Init()
    {
        _logger.LogInformation("SupabaseClient initialized");
        await _client.InitializeAsync();
    }

    public async Task<Session?> CreateUserAccount(string email, string password, Dictionary<string, object> metaData)
    {
        
        Session? newSession = await _client.Auth.SignUp(email, password, new SignUpOptions()
                                {
                                    FlowType = Constants.OAuthFlowType.PKCE,
                                    Data = metaData,
                                    RedirectTo = "/home"
                                });

        return newSession;
    }
}