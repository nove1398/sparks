namespace Sparks.Api.Features.Users;

public class UserPoolManagerService
{
    private readonly ILogger<UserPoolManagerService> _logger;
    
    public UserPoolManagerService(ILogger<UserPoolManagerService> logger)
    {
        _logger = logger;
    }

    public Task FindMatchForHost()
    {
        return Task.CompletedTask;
    }

    public Task InitiateRoomSession()
    {
        return Task.CompletedTask;
    }

    public Task EndRoomSession()
    {
        return Task.CompletedTask;
    }
}