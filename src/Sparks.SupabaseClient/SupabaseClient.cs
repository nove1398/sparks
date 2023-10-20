namespace Sparks.SupabaseClient;

public class SupabaseClient
{
    private readonly string _projectUrl;
    private readonly string _projectKey;

    public SupabaseClient(string projectUrl, string projectKey)
    {
        _projectUrl = projectUrl;
        _projectKey = projectKey;
        InitBase();
    }

    private void InitBase()
    {
        
    }
}