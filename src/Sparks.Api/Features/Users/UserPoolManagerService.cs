using Sparks.Api.Database;

namespace Sparks.Api.Features.Users;

public class UserPoolManagerService
{
    private readonly ILogger<UserPoolManagerService> _logger;
    private readonly SparksDbContext _dbContext;
    public UserPoolManagerService(ILogger<UserPoolManagerService> logger, SparksDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
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

    private Task FindAllRegisteredForSessions()
    {
        return Task.CompletedTask;
    }
}