using Carter;
using MediatR;
using Sparks.Api.Shared;
using Supabase.Gotrue;

namespace Sparks.Api.Features.Users;

public sealed class CreateUserCommand : ICommand<Result<Session?>>
{
    public GenderTypes Gender { get; init; }
}

public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<Session?>>
{
    private readonly SupabaseClient.SupabaseClient _supabaseClient;
    private readonly ILogger<CreateUserCommandHandler> _logger;
    public CreateUserCommandHandler(ILogger<CreateUserCommandHandler> logger, SupabaseClient.SupabaseClient supabaseClient)
    {
        _logger = logger;
        _supabaseClient = supabaseClient;
    }
    
    public async Task<Result<Session?>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        Session? createdSession = await _supabaseClient.CreateUserAccount("nove1398@yahoo.com","mypassword", new Dictionary<string, object>()
        {
            {"location", "Jamaica"}
        });
        // Create local user 
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
                Gender = GenderTypes.Male
            });
            return Results.Ok(createdSession);
        });
    }
}