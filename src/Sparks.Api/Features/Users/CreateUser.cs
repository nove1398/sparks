using Carter;
using MediatR;
using Sparks.Api.Shared;
using Supabase.Gotrue;

namespace Sparks.Api.Features.Users;

internal sealed class CreateUserCommand : ICommand
{
    public GenderTypes Gender { get; init; }
}

internal sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
{
    private readonly ILogger<CreateUserCommandHandler> _logger;
    public CreateUserCommandHandler(ILogger<CreateUserCommandHandler> logger)
    {
        _logger = logger;
    }
    
    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Create user command!");
        await Task.CompletedTask;
    }
}


public sealed class CreateUserModule : ICarterModule
{
    private readonly SupabaseClient.SupabaseClient _supabaseClient;
    public CreateUserModule(SupabaseClient.SupabaseClient supabaseClient)
    {
        _supabaseClient = supabaseClient;
    }
    
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/users", async () =>
        {

           Session? createdSession = await _supabaseClient.CreateUserAccount("nove1398@yahoo.com","mypassword", new Dictionary<string, object>()
            {
                {"location", "Jamaica"}
            });
            return Results.Ok(createdSession);
        });
    }
}