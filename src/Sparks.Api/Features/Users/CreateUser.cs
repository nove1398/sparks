using Carter;
using MediatR;
using Sparks.Api.Database;
using Sparks.Api.Shared;
using Supabase.Gotrue;

namespace Sparks.Api.Features.Users;

public sealed class CreateUserCommand : ICommand<Result<Session?>>
{
    public GenderTypes Gender { get; init; }
    public string? Country { get; init; }
    public int? Age { get; init; }
    public int? HeightInCm { get; init; }
    public StarSignTypes? StarSign { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
}

public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<Session?>>
{
    private readonly SupabaseClient.SupabaseClient _supabaseClient;
    private readonly ILogger<CreateUserCommandHandler> _logger;
    private readonly SparksDbContext _dbContext;
    public CreateUserCommandHandler(ILogger<CreateUserCommandHandler> logger, SupabaseClient.SupabaseClient supabaseClient, SparksDbContext dbContext)
    {
        _logger = logger;
        _supabaseClient = supabaseClient;
        _dbContext = dbContext;
    }
    
    public async Task<Result<Session?>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        Session? createdSession = await _supabaseClient.CreateUserAccount(
            request.Email,
            request.Password, 
            new Dictionary<string, object>()
        {
            {"location", request.Country}
        });
        if (createdSession is null)
        {
            return Result<Session?>.Failure(new ErrorModel("Failed to register user account","Null response from server"));
        }

        if (createdSession.AccessToken is null)
        {
            return Result<Session?>.Failure(new ErrorModel("Account already created"));
        }
        
        // Create local user 
        User.Create(
            Guid.Parse(createdSession.User?.Id),
            createdSession.User.Email!,
            "Jamaica",
            request.Age,
            request.HeightInCm,
            request.Gender,
            request.StarSign);
        return Result<Session?>.Success(createdSession);
    }
}


public sealed class CreateUserModule : ICarterModule
{
    public CreateUserModule()
    {
       
    }
    
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/users", async (ISender mediator) =>
        {
            var createdSession = await mediator.Send(new CreateUserCommand()
            {
                Gender = GenderTypes.Male,
                Email = "mytestEmail@yahoo.com",
                Password = "mypassword",
                StarSign = StarSignTypes.Aquarius,
                Age = 21,
                Country = "Jamaica",
                HeightInCm = 120
            });
            return Results.Ok(createdSession);
        });
    }
}