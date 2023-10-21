using System.Text.Json;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Sparks.Api.Shared;
using Supabase.Gotrue;

namespace Sparks.Api.Features.Users;

public class AuthenticateUserCommand : ICommand<Result<Session?>>
{
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}

public sealed class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, Result<Session?>>
{
    private readonly SupabaseClient.SupabaseClient _supabaseClient;
    private readonly ILogger<AuthenticateUserCommandHandler> _logger;
    public AuthenticateUserCommandHandler(SupabaseClient.SupabaseClient supabaseClient, ILogger<AuthenticateUserCommandHandler> logger)
    {
        _supabaseClient = supabaseClient;
        _logger = logger;
    }
    
    public async Task<Result<Session?>> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        Session? session = await _supabaseClient.AuthenticateUserAccount(request.Email, request.Password);
        _logger.LogInformation("{Session}", JsonSerializer.Serialize(session));

        return Result<Session?>.Success(session);
    }
}

public class AuthenticationModule : ICarterModule
{
    private readonly IMediator _mediator;
    public AuthenticationModule(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/users/auth", async () =>
        {
            var authCommand = new AuthenticateUserCommand()
            {
                Email = "nove1398@yahoo.com",
                Password = "mypassword"
            };
            await _mediator.Send(authCommand);
            return Results.Ok();
        });
    }
}